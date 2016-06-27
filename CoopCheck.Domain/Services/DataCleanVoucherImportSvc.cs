using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Converters;
using CoopCheck.Domain.Contracts.Wrappers;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Services;
using Newtonsoft.Json;
using DataCleanEventConverter = CoopCheck.Domain.Converters.DataCleanEventConverter;
using VoucherImportWrapperConverter = CoopCheck.Domain.Converters.VoucherImportWrapperConverter;

namespace CoopCheck.Domain.Services
{
    public class DataCleanVoucherImportSvc : IDataCleanVoucherImportSvc
    {
        private Dictionary<string, string> _settings;
        private NetworkCredential _credential;

        public DataCleanVoucherImportSvc(Dictionary<String, String> settings , NetworkCredential credential)
        {
            _settings = settings;
            _credential = credential;
        }
        public async Task<ObservableCollection<VoucherImportWrapper>> CleanVouchers(
            List<VoucherImportWrapper> vouchers)
        {
            var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
            foreach (var v in vouchers)
            {
                v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
                    v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
            }

            List<IInputStreetAddress> inputAddresses = new List<IInputStreetAddress>(vouchers.Select(VoucherImportWrapperConverter.ToInputStreetAddress).ToList());
            var dataCleanEvents = await ValidateAddresses(inputAddresses);

            var ilist = new List<VoucherImportWrapper>();
            foreach (var e in dataCleanEvents)
            {
                VoucherImportWrapper i = (VoucherImportWrapper) DataCleanEventConverter.ToVoucherImportWrapper(e,
                    vouchers.First(x => x.RecordID == e.Input.RecordID));
                // we want to join the row to get the data we did not send to the cleaner
                ilist.Add(i);
            }
            return new ObservableCollection<VoucherImportWrapper>(ilist);
        }

        public  async Task<List<IDataCleanEvent>> ValidateAddresses(List<IInputStreetAddress> newVouchers)
        {
            HttpResponseMessage response;
            List<IDataCleanEvent> retVal = new List<IDataCleanEvent>();

            using (
                var client =
                    new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler { Credentials = _credential }))
            {
                //client.BaseAddress = new Uri("http://localhost:37432/");
                client.BaseAddress = new Uri(_settings["SwiftPaySite"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // HTTP POST
                    response =
                        await
                            client.PostAsJsonAsync<List<IInputStreetAddress>>("api/DataCleanEvent/CleanAddresses",
                                newVouchers);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = response.Content.ReadAsStringAsync().Result;
                        var dceList = JsonConvert.DeserializeObject<List<IDataCleanEvent>>(jsonContent);
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