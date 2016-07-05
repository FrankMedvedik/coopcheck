using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Interfaces;
using DataClean.Models;
using DataClean.Repository;
using log4net;
using log4net.Repository.Hierarchy;

namespace DataClean.DataCleaner
{
    public class DataCleanEventFactory
    {
        private DataCleaner _dataCleaner;
        private Repository.Mgr.DataCleanRespository _dataCleanRepository;
        private DataCleanCriteria _criteria;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(DataCleanEventFactory));
        public DataCleanEventFactory(DataCleaner dataCleaner, Repository.Mgr.DataCleanRespository dataCleanRepository, DataCleanCriteria criteria)
        {
            _dataCleaner = dataCleaner;
            _dataCleanRepository = dataCleanRepository;

            // if we get bad data clean criteruia use the defaults (no fixes and no force) 
            if (criteria != null) _criteria = criteria;
            _criteria = new DataCleanCriteria()
            {
                AutoFixAddressLine1 = false,
                AutoFixCity = false,
                AutoFixPostalCode = true,
                AutoFixState = false,
                ForceValidation = false
            };

        }

        public  DataCleanEvent ValidateAddress(InputStreetAddress input)
        {

            DataCleanEvent e;
            OutputStreetAddress output;
            if (_criteria.ForceValidation == false)
            {
                e = _dataCleanRepository.GetEvent(input.ID);
                if (e != null) return e;
            }

            var b = _dataCleaner.VerifyAndCleanAddress(input, out output);
            e = new DataCleanEvent() {Input = input, DataCleanDate = DateTime.Now, Output = output};
            _dataCleanRepository.SaveEvent(e);
            return e;
        }

        public DataCleanEvent GetExistingEvent(int id)
        {
                return _dataCleanRepository.GetEvent(id);
        }
        public List<DataCleanEvent> ValidateAddresses(List<InputStreetAddress> inputAddressList)
        {
            List<InputStreetAddress> toBeCleaned = new List<InputStreetAddress>();
            List<DataCleanEvent> results = new List<DataCleanEvent>();
            foreach (var i in inputAddressList)
            {
                if (_criteria.ForceValidation == false)
                {
                    var e = _dataCleanRepository.GetEvent(i.ID);
                    if (e != null) results.Add(MergeInandOutReq(i, e));
                    else toBeCleaned.Add(i);
                }
                else
                {
                    toBeCleaned.Add(i);
                }
            }

            DataCleanStat d = new DataCleanStat()
            {
                CacheCnt = results.Count,
                CleanCnt = toBeCleaned.Count,
                CleanDate = DateTime.Now
            };

            _dataCleanRepository.SaveStats(d);

            Logger.Info("Number of vouchers to clean " + toBeCleaned.Count);
            Logger.Info("Number of vouchers pulled from repository " + results.Count);


            if (toBeCleaned.Any())
            {
                var outArray = _dataCleaner.VerifyAndCleanAddress(toBeCleaned.ToArray());
                List<DataCleanEvent> newEvents = CombineOutputsAndInputs(toBeCleaned, outArray);
                foreach (var e in newEvents)
                {
                        _dataCleanRepository.SaveEvent(e);
                }
                results.AddRange(newEvents);
            }
            return ApplyAutomaticFixes(results);
        }

        private DataCleanEvent MergeInandOutReq(InputStreetAddress inAddress, DataCleanEvent outEvent)
        {
            var e = new DataCleanEvent
            {
                DataCleanDate = outEvent.DataCleanDate,
                Input = inAddress,
                Output = outEvent.Output
            };
            e.Output.RecordID = e.Input.RecordID;
            return e;
        }

        private List<DataCleanEvent> ApplyAutomaticFixes(List<DataCleanEvent> results)
        {
            AutoFixer A = new AutoFixer(_criteria) {DataCleanEvents = results};
            A.ApplyFixes();
            return A.DataCleanEvents;
        }

        private List<DataCleanEvent> CombineOutputsAndInputs(List<InputStreetAddress> toBeCleaned,OutputStreetAddress[] outArray)
        {

            var convertedList = new List<DataCleanEvent>();

            foreach (var i in toBeCleaned)
            {
                var e = new DataCleanEvent();
                //find related output assign to local var
                var oc = outArray.First(obj => obj.RecordID == i.RecordID);
                if (oc != null)
                {
                    e.DataCleanDate = DateTime.Now;
                    e.Input = i;
                    e.Output = oc;
                    convertedList.Add(e);
                }
            }
            return convertedList;
        }
    }
}

 