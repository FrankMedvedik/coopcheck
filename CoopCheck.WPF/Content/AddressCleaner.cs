using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content
{
        public sealed class AddressCleaner : ViewModelBase
        {
            private static volatile AddressCleaner instance;
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
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                                instance = new AddressCleaner();
                        }
                    }

                    return instance;
                }
            }

            public  DataCleanEventFactory DataCleanEventFactory
        {
                get { return instance._dataCleanEventFactory; }
                set
                {
                    instance._dataCleanEventFactory = value;
                    NotifyPropertyChanged();
                }
            }

        private DataCleanEventFactory _dataCleanEventFactory;
   
    }
}
