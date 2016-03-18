using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using DataClean;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanSvc
    {

        public static async Task<List<DataCleanEvent>> ValidateAddressesAsync(List<InputStreetAddress> newVouchers,
            DataCleanCriteria criteria)
        {
            List<DataCleanEvent> outRows = new List<DataCleanEvent>();

            try
            {
                outRows = await Task<List<DataCleanEvent>>.Factory.StartNew(() =>
                {
                    var c = ConfigurationManager.AppSettings;
                    var d = new DataCleanEventFactory(new DataCleaner(c),new DataClean.Repository.Mgr.DataCleanRespository(), criteria);
                    var a =d.ValidateAddresses(newVouchers);
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