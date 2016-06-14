using System;
using System.Collections.Generic;
using System.Linq;
using DataClean.Contracts.Interfaces;
using DataClean.Models;
using DataClean.Repository;

namespace DataClean.DataCleaner
{
    public class DataCleanEventFactory : IDataCleanEventFactory
    {
        private IDataCleaner _dataCleaner;
        private IDataCleanRepository _dataCleanRepository;
        private IDataCleanCriteria _criteria;
        

        public DataCleanEventFactory(IDataCleaner dataCleaner, IDataCleanRepository dataCleanRepository, IDataCleanCriteria criteria)
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

        public  IDataCleanEvent ValidateAddress(IInputStreetAddress input)
        {

            IDataCleanEvent e;
            IOutputStreetAddress output;
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

        public IDataCleanEvent GetExistingEvent(int id)
        {
                return _dataCleanRepository.GetEvent(id);
        }
        public List<IDataCleanEvent> ValidateAddresses(List<IInputStreetAddress> inputAddressList)
        {
            List<IInputStreetAddress> toBeCleaned = new List<IInputStreetAddress>();
            List<IDataCleanEvent> results = new List<IDataCleanEvent>();
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

            Console.WriteLine("Number of vouchers to clean " + toBeCleaned.Count);
            Console.WriteLine("Number of vouchers pulled from repository " + results.Count);


            if (toBeCleaned.Any())
            {
                var outArray = _dataCleaner.VerifyAndCleanAddress(toBeCleaned.ToArray());
                List<IDataCleanEvent> newEvents = CombineOutputsAndInputs(toBeCleaned, outArray);
                foreach (var e in newEvents)
                {
                        _dataCleanRepository.SaveEvent(e);
                }
                results.AddRange(newEvents);
            }
            return ApplyAutomaticFixes(results);
        }

        private IDataCleanEvent MergeInandOutReq(IInputStreetAddress inAddress, IDataCleanEvent outEvent)
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

        private List<IDataCleanEvent> ApplyAutomaticFixes(List<IDataCleanEvent> results)
        {
            AutoFixer A = new AutoFixer(_criteria) {DataCleanEvents = results};
            A.ApplyFixes();
            return A.DataCleanEvents;
        }

        private List<IDataCleanEvent> CombineOutputsAndInputs(List<IInputStreetAddress> toBeCleaned,IOutputStreetAddress[] outArray)
        {

            var convertedList = new List<IDataCleanEvent>();

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

 