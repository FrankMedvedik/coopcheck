using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Properties;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;
using log4net;
using Newtonsoft.Json;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanVoucherImportSvc
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static async Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(
            List<VoucherImportWrapper> vouchers)
        {
            var ilist = new List<VoucherImportWrapper>();

            try
            {
                foreach (var v in vouchers)
                {
                    v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                        v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
                }

                var inputAddresses = vouchers.Select(VoucherImportWrapperConverter.ToInputStreetAddress).ToList();
                var dataCleanEvents = await ValidateAddresses(inputAddresses);

                foreach (var e in dataCleanEvents)
                {
                    var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
                        vouchers.First(x => x.RecordID == e.RecordID));
                    // we want to join the row to get the data we did not send to the cleaner
                    ilist.Add(i);
                }
            }
            catch (Exception e)
            {
                log.Error(string.Format("cleaning the vouchers failed - {0} - {1} ", e.Message,
                    e.InnerException?.Message));
                throw;
            }
            return new ObservableCollection<VoucherImportWrapper>(ilist);
        }

        public static async Task<List<DataCleanEvent>> ValidateAddresses(List<InputStreetAddress> newVouchers)
        {
            HttpResponseMessage response;
            var retVal = new List<DataCleanEvent>();

#if DEBUG
            var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
            //var credentials = new NetworkCredential("fmedv", "candycup22");
            using (
                var client =
                    new HttpClient(new HttpClientHandler {Credentials = credentials}))
#else
            using (
                var client =
                    new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler()
                    {
                        UseDefaultCredentials = true
                    }))
#endif

            {
                //client.BaseAddress = new Uri("http://localhost:22253/");
                client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // HTTP POST
                    response =
                        await
                            client.PostAsJsonAsync("api/DataCleanEvent/CleanAddresses",
                                newVouchers);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = response.Content.ReadAsStringAsync().Result;
                        var dceList = JsonConvert.DeserializeObject<List<DataCleanEvent>>(jsonContent);
                        //foreach (DataCleanEvent e in dceList)
                        //    Console.WriteLine("\tID: " + e.ID + ", " + e.DataCleanDate + ", " + e.Output.OkComplete);
                        retVal = dceList;
                    }
                }
                catch (Exception e)
                {
                    log.Error(string.Format("cleaning the vouchers failed - {0} - {1} ", e.Message,
                        e.InnerException?.Message));
                    throw;
                }
                return retVal;
            }
        }
    }
}