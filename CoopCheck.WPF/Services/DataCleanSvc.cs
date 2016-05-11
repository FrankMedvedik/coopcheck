using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.Properties;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using Newtonsoft.Json;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanSvc
    {
        public static  List<DataCleanEvent> ValidateAddresses(List<InputStreetAddress> newVouchers)
        {
            return  AddressCleaner.Instance.DataCleanEventFactory.ValidateAddresses(newVouchers);
        }


        //public static async Task<List<DataCleanEvent>> SvrValidateAddressesAsync(List<InputStreetAddress> newVouchers)
        //{
        //    List<DataCleanEvent> retVal = new List<DataCleanEvent>();
        //    var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
        //    var client = new HttpClient(new HttpClientHandler { Credentials = credentials });
        //    //using (var client = new HttpClient(new HttpClientHandler() {UseDefaultCredentials = true}))
        //    //{
        //       // client.BaseAddress = new Uri("http://localhost:37432/");
        //        client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //}
        //    try
        //    {
        //        // HTTP POST
        //        var response = await client.PostAsJsonAsync<List<InputStreetAddress>>("api/DataCleanEvent/CleanAddresses", newVouchers);
        //        var dceList = await response.Content.ReadAsAsync<List<DataCleanEvent>>();
        //    }
        //    catch (Exception e)
        //    {

        //        Console.WriteLine(e.Message);
        //    }


        //if (response.IsSuccessStatusCode)
        //    {
        //    string jsonContent = response.Content.ReadAsStringAsync().Result;
        //    var dceList = JsonConvert.DeserializeObject<List<DataCleanEvent>>(jsonContent);
        //        return dceList;
        //    }
        //}
        //return retVal;
    //}
}
}