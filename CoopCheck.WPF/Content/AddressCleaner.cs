using System;
using System.Configuration;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content
{
        public sealed class AddressCleaner : ViewModelBase
        {
            private static volatile AddressCleaner _instance;
            private static object syncRoot = new Object();

            private AddressCleaner()
            {
            var c = ConfigurationManager.AppSettings;
            var dataCleanCriteria = new DataCleanCriteria
            {
                AutoFixAddressLine1 = false,
                AutoFixCity = false,
                AutoFixPostalCode = true,
                AutoFixState = false,
                ForceValidation = false
            };
            _dataCleanEventFactory = new DataCleanEventFactory(new DataCleaner(c), new DataCleanRespository(), dataCleanCriteria);
        }

            public static AddressCleaner Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (_instance == null)
                                _instance = new AddressCleaner();
                        }
                    }

                    return _instance;
                }
            }

            public  DataCleanEventFactory DataCleanEventFactory
        {
                get { return _instance._dataCleanEventFactory; }
                set
                {
                    _instance._dataCleanEventFactory = value;
                    NotifyPropertyChanged();
                }
            }

        private DataCleanEventFactory _dataCleanEventFactory;
   
    }
}
