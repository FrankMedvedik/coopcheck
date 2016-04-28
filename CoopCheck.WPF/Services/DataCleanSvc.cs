using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanSvc
    {
        public static async Task<List<DataCleanEvent>> ValidateAddressesAsync(List<InputStreetAddress> newVouchers,
            DataCleanCriteria criteria)
        {
            var outRows = new List<DataCleanEvent>();

            try
            {
                outRows = await Task<List<DataCleanEvent>>.Factory.StartNew(() =>
                {
                    var c = ConfigurationManager.AppSettings;
                    var d = new DataCleanEventFactory(new DataCleaner(c), new DataCleanRespository(), criteria);
                    var a = d.ValidateAddresses(newVouchers);
                    return a;
                });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return outRows;
        }
    }
}