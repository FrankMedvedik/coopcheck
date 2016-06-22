using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.Converters;
using DataClean.Personator;
using DataClean.Repository.Mgr;
using log4net;

namespace DataClean.DataCleaner 
{
    public class DataCleaner : IDataCleaner
    {
        private const int MaxArraySize = 100;
        private static readonly ILog Logger =
            LogManager.GetLogger(typeof (DataCleaner));

        private Request _req;
        private Response _resp;
        private ServicemdContactVerifySOAPClient _action;
        private int _arraysize;
        private ParseResultDictionary _msgDict;
        DataCleanRespository _db;



        public DataCleaner(NameValueCollection c)
        {
            Initialize(c);
            ForceValidation = false;
            _db = new DataCleanRespository(); 
            _msgDict = new ParseResultDictionary(_db.GetMelissaReference());
        }

        public bool ForceValidation { get; set; }

        public DataCleaner(NameValueCollection c, List<IParseResult> melissaCodes)
        {
            Initialize(c);
            _msgDict = new ParseResultDictionary(melissaCodes);
        }


        private void Initialize(NameValueCollection c)
        {
            //String ClientID =c["PersonatorClientID"];

            String ClientID = "98867798";
            String PersonatorActions = c["PersonatorActions"];
            String PersonatorOptions = c["PersonatorOptions"];

            if (ClientID == null)
            {
                var t = "ClientID Not Specified in app config file";
                Logger.Error(t);
                throw (new Exception(t));
            }

            if (PersonatorActions == null)
            {
                var u = "http://wiki.melissadata.com/index.php?title=Personator%3ABest_Practices";
                var t = String.Format("No actions specified in the app config file see: {0} ", u);
                Logger.Error(t);
                throw (new Exception(t));
            }

            Logger.Info(String.Format("Melissa ClientId {0}", ClientID));
            Logger.Info(String.Format("Melissa Personator Actions - {0}", PersonatorActions));
            Logger.Info(String.Format("Melissa Personator Options - {0}", PersonatorOptions));


            _req = new Request();
            _req.Actions = PersonatorActions;
            _req.Options = PersonatorOptions;
            _resp = new Response();
            _action = new ServicemdContactVerifySOAPClient(); //"BasicHttpBinding_IService");
            _req = new Request {CustomerID = ClientID, TransmissionReference = "JRA Personator SOAP Web Service "};
            
        }


        public Boolean VerifyAndCleanAddress(IInputStreetAddress inA, out IOutputStreetAddress outA)
        {
            /* use settings from config file and process 1 record */
            var iArray = new[] {inA};
            var oArray = VerifyAndCleanAddress(iArray);
            outA = oArray[0];
            return  oArray[0].OkComplete;
        }

        public IOutputStreetAddress[] VerifyAndCleanAddress(IInputStreetAddress[] inputAddressArray)
        {
            var o = new List<IOutputStreetAddress>();
            
            int i = 0;
            int arrayOffset = 0;

            if (inputAddressArray.Length <= MaxArraySize)
                return VerifyAndCleanAddressBatch(inputAddressArray).ToArray();
            i = MaxArraySize;
            while (o.Count  < inputAddressArray.Length)
            {
                var l = inputAddressArray.Skip(arrayOffset).Take(i);
                o.AddRange(VerifyAndCleanAddressBatch(l.ToArray()));
                arrayOffset = o.Count() -1 ;
                i = ((inputAddressArray.Length - arrayOffset) < MaxArraySize)
                    ? inputAddressArray.Length - arrayOffset
                    : MaxArraySize;
            }
            return o.ToArray();
        }

        protected IOutputStreetAddress[] VerifyAndCleanAddressBatch(IInputStreetAddress[] inputAddressArray)
        {
            if (inputAddressArray.Length > MaxArraySize)
                throw new Exception(String.Format("Too Many Items in Request maximum number is {0}", MaxArraySize));
            _arraysize = inputAddressArray.Length;
            var rra = new RequestRecord[_arraysize];
            var x = 0;
            foreach (var i in inputAddressArray)
                rra[x++] = new RequestRecord(i);
            _req.Records = rra;

            try
            {
                // the transmission results tell us if we got far enough to process records. if it is blank the answer is yes 
                // if we got transmission results we have a broke - connection and or configuration 
                _resp = _action.doContactVerify(_req);

                if (_resp.TransmissionResults.Trim() == "")
                {
                    var o = new OutputStreetAddress[_resp.Records.Length];
                    int i = 0;
                    foreach (var r in _resp.Records)
                    {
                        o[i++] = ResponseToOutputStreetAddressConverter.ProcessResponseRecord(r, _msgDict);
                    }
                    return o;
                }
                var t = GetTransmissionErrors();
                string exText = null;
                foreach (var a in t)
                    exText += a.ToString() + Environment.NewLine;
                throw new Exception(exText);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }

        }

      
        private IParseResult[] GetTransmissionErrors()
        {
            return _msgDict.LookupCodeList(_resp.TransmissionResults.Split(','));
        }

  
    }
}