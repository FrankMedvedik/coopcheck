using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Content.Voucher.Pay;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Properties;
using Microsoft.Office.Interop.Word;

namespace CoopCheck.WPF.Services
{
    public static class HonorariaPaymentSvc
    {
        public static int MAX_HonorariaPayment_COUNT = 10000;

        public static async Task<StatusInfo> SwiftFulfillAsync(int batchNum)
        {
            var i = new StatusInfo();
            try
            {
                //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
                //System.Threading.Thread.Sleep(5000);
#if DEBUG

                var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
                using (var client = new HttpClient(new HttpClientHandler { Credentials = credentials }))
                {
#else
                using (var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
                {
#endif
                    client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP POST
                    var response = await client.PostAsync(String.Format("api/swiftHonorariaPayment/swiftpay?batchNum={0}", batchNum), null);
                    if (response.IsSuccessStatusCode)
                        i.StatusMessage = "Swiftpay processing started. An email will be sent to you when it completes";
                    i.IsBusy = false;
                }
            }
            catch (Exception e)
            {
                i.StatusMessage = String.Format("Swiftpay processing error for batch {0}", batchNum);
                i.ErrorMessage = e.Message;
            }
            return i;
        }
        public static async Task<StatusInfo> SwiftVoidAsync(int batchNum)
        {
            var i = new StatusInfo();
            try
            {
                //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
                //System.Threading.Thread.Sleep(5000);
                //  client.BaseAddress = new Uri("http://localhost:37432/");
#if DEBUG

                var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
                using (var client = new HttpClient(new HttpClientHandler { Credentials = credentials }))
                {
#else
                using (var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
                {
#endif
                  
                    client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // HTTP POST
                    var response = await client.PostAsync(String.Format("api/swiftHonorariaPayment/swiftvoid?batchNum={0}", batchNum), null);
                    if (response.IsSuccessStatusCode)
                        i.StatusMessage = "Swiftpay processing started";
                    i.IsBusy = false;
                }
            }
            catch (Exception e)
            {
                i.StatusMessage = String.Format("Swiftpay processing error for batch {0}", batchNum);
                i.ErrorMessage = e.Message;
            }
            return i;
        }

        //public static async Task<StatusInfo> SwiftFulfillAsync( int batchNum)
        //{
        //    var i = new StatusInfo();
        //    try
        //    {
        //        //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
        //        //System.Threading.Thread.Sleep(5000);
        //        await Task.Factory.StartNew(() => BatchSwiftFulfillCommand.Execute(batchNum));
        //        i.StatusMessage = "Swiftpay processing complete";
        //        i.IsBusy = false;
        //    }
        //    catch (Exception e)
        //    {
        //        i.StatusMessage = "Swift Pay processing error for batch {0}";
        //        i.ErrorMessage = e.Message;
        //    }
        //    return i;
        //}

        public static async Task<PrintCheckProgress> PrintChecksAsync(int accountId, int batchNum, int startingCheckNum,
            CancellationToken ctx, Progress<PrintCheckProgress> progress)
        {
            //return await Task<StatusInfo>.Factory.StartNew(() =>
            //{
            //    StatusInfo i = new StatusInfo();
            //    i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
            //    System.Threading.Thread.Sleep(10000);
            //    return i;
            //});
            return
                await
                    Task<PrintCheckProgress>.Factory.StartNew(
                        () => PrintChecks(accountId, batchNum, startingCheckNum, ctx, progress));
        }

        private static PrintCheckProgress PrintChecks(int accountId, int batchNum, int startingCheckNum,
            CancellationToken ctx, IProgress<PrintCheckProgress> progress)
        {
            //return 
            var retVal = new PrintCheckProgress();

            var b = BatchEdit.GetBatchEdit(batchNum);
            var checkNum = startingCheckNum;
            var currentCheckPct = 0;


            // WriteCheckBatchCommand works like a begin transaction and 
            // marks the whole batch as in progress... the check number is defined here but the print flag is set at the end. 
            // see commit afterwards

            WriteCheckBatchCommand.Execute(batchNum, accountId, startingCheckNum);

            var app = new Application();
            foreach (var c in b.Vouchers)
            {
                var status = HonorariaPaymentPrintSvc.PrintCheck(app, b, c, checkNum++);
                status.IsBusy = true;
                ++currentCheckPct;
                var z = currentCheckPct/b.Vouchers.Count*100;
                retVal.ProgressPercentage = (currentCheckPct/(decimal) b.Vouchers.Count*100);
                retVal.CurrentCheckNum = checkNum;
                retVal.Status = status;
                progress?.Report(retVal); // calls SynchronizationContext.Post
                if (retVal.Status.ErrorMessage != null)
                {
                    app.Quit(false);
                    return retVal;
                }
                if (ctx.IsCancellationRequested)
                    app.Quit(false);
                ctx.ThrowIfCancellationRequested();
            }
            app.Quit(false);
            return new PrintCheckProgress
            {
                ProgressPercentage = (currentCheckPct/(decimal) b.Vouchers.Count*100),
                Status = new StatusInfo
                {
                    StatusMessage =
                        string.Format("Batch {0}  Printed", batchNum)
                },
                CurrentCheckNum = --checkNum
            };
        }
    
    }
}