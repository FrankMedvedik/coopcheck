using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Properties;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using DataClean.Services;
using Newtonsoft.Json;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanVoucherImportSvc
    {
        //public async static Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(List<VoucherImportWrapper> vouchers)
        //{
        //    var l = await Task<ObservableCollection<VoucherImportWrapper>>.Factory.StartNew(() =>
        //    {
        //        var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
        //        foreach (var v in vouchers)
        //        {
        //            v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
        //                v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
        //        }
        //        var inputAddresses = vouchers.Select(v => VoucherImportWrapperConverter.ToInputStreetAddress(v)).ToList();
        //        var dataCleanEvents = DataCleanSvc.TestValidateAddresses(inputAddresses);


        //        //var dataCleanEvents = DataCleanSvc.ValidateAddresses(inputAddresses).Result;
        //        var ilist = new List<VoucherImportWrapper>();
        //        foreach (var e in dataCleanEvents)
        //        {
        //            var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
        //                vouchers.First(x => x.RecordID == e.RecordID));
        //            // we want to join the row to get the data we did not send to the cleaner
        //            ilist.Add(i);
        //        }
        //        return new ObservableCollection<VoucherImportWrapper>(ilist);
        //    });
        //    return l;
        //}



        public static async Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(
            List<VoucherImportWrapper> vouchers)
        {
            var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
            foreach (var v in vouchers)
            {
                v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                    v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
            }

            var inputAddresses = vouchers.Select(VoucherImportWrapperConverter.ToInputStreetAddress).ToList();
            var dataCleanEvents = await TestValidateAddresses(inputAddresses);

            var ilist = new List<VoucherImportWrapper>();
            foreach (var e in dataCleanEvents)
            {
                var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
                    vouchers.First(x => x.RecordID == e.RecordID));
                // we want to join the row to get the data we did not send to the cleaner
                ilist.Add(i);
            }
            return new ObservableCollection<VoucherImportWrapper>(ilist);
        }

        public static async Task<List<DataCleanEvent>> TestValidateAddresses(List<InputStreetAddress> newVouchers)
        {
            HttpResponseMessage response;
            List<DataCleanEvent> retVal = new List<DataCleanEvent>();

#if DEBUG
            var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
            using (
                var client =
                    new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler { Credentials = credentials }))
#else
            using (
                var client =
                    new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler()
                    {
                        UseDefaultCredentials = true
                    })
#endif        

            {
                //client.BaseAddress = new Uri("http://localhost:37432/");
                client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // HTTP POST
                    response =
                        await
                            client.PostAsJsonAsync<List<InputStreetAddress>>("api/DataCleanEvent/CleanAddresses",
                                newVouchers);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = response.Content.ReadAsStringAsync().Result;
                        var dceList = JsonConvert.DeserializeObject<List<DataCleanEvent>>(jsonContent);
                        //foreach (DataCleanEvent e in dceList)
                        //    Console.WriteLine("\tID: " + e.ID + ", " + e.DataCleanDate + ", " + e.Output.OkComplete);
                        retVal = dceList;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                return retVal;
            }

        }
    }
}