using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoopCheck.WPF.Content;
using DataClean.Models;
using Newtonsoft.Json;

namespace CoopCheck.WPF.Services
{
    public static class DataCleanSvc
    {
        //public static  List<DataCleanEvent> ValidateAddresses(List<InputStreetAddress> newVouchers)
        //{
        //    return  AddressCleaner.Instance.DataCleanEventFactory.ValidateAddresses(newVouchers);
        //}

        public static async Task<List<DataCleanEvent>> ValidateAddresses(List<InputStreetAddress> newVouchers)
        {
            HttpResponseMessage response;
            List<DataCleanEvent> retVal = new List<DataCleanEvent>();
            //var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
            //var client = new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler { Credentials = credentials });
            using (
                var client =
                    new HttpClient(new HttpClientHandler()
                    {
                        UseDefaultCredentials = true
                    }))
            {
                client.BaseAddress = new Uri("http://localhost:37432/");
                //client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // HTTP POST
                    response = await client.PostAsJsonAsync<List<InputStreetAddress>>("api/DataCleanEvent/CleanAddresses",newVouchers);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = response.Content.ReadAsStringAsync().Result;
                        var dceList = JsonConvert.DeserializeObject<List<DataCleanEvent>>(jsonContent);
                        foreach (DataCleanEvent e in dceList)
                            Console.WriteLine("\tID: " + e.ID + ", " + e.DataCleanDate + ", " + e.Output.OkComplete);
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