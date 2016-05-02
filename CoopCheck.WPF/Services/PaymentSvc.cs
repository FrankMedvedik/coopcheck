using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Voucher.Pay;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Properties;
using Microsoft.Office.Interop.Word;
using Task = System.Threading.Tasks.Task;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc
    {
        public static int MAX_PAYMENT_COUNT = 10000;


        //public static async Task<StatusInfo> SwiftFulfillAsync(int accountId, int batchNum)
        //{
        //    var i = new StatusInfo();
        //    try
        //    {
        //        //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
        //        //System.Threading.Thread.Sleep(5000);
        //        //var credentials = new NetworkCredential("fmedvedik@reckner.com", "(manos)3k");
        //        //var client = new HttpClient(new HttpClientHandler { Credentials = credentials });
        //        //{ 
        //        using (var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
        //        {
        //            //client.BaseAddress = new Uri("http://localhost:37432/");
        //            client.BaseAddress = new Uri(Settings.Default.SwiftPaySite);
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            //// HTTP GET
        //            //HttpResponseMessage response = await client.GetAsync(String.Format("api/Swiftpayment?batchNum={0}",batchNum));
        //            //if (response.IsSuccessStatusCode)
        //            //{
        //            //    var a = await response.Content.ReadAsAsync<List<vwPayment>>();
        //            //    foreach (var r in a)
        //            //        Console.WriteLine("{0}\t${1}\t{2}", r.last_name, r.check_num, r.tran_amount);
        //            //}

        //            // HTTP POST
        //            var response = await client.PostAsync(String.Format("api/swiftpayment/swiftpay?accountId={0}&batchNum={1}",accountId,batchNum), null);
        //            if (response.IsSuccessStatusCode)
        //                i.StatusMessage = "Swiftpay processing started";
        //                i.IsBusy = false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        i.StatusMessage = String.Format("Swiftpay processing error for batch {0}",batchNum);
        //        i.ErrorMessage = e.Message;
        //    }
        //    return i;
        //}

        public static async Task<StatusInfo> SwiftFulfillAsync(int accountId, int batchNum)
        {
            var i = new StatusInfo();
            try
            {
                //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
                //System.Threading.Thread.Sleep(5000);
                await Task.Factory.StartNew(() => BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum));
                i.StatusMessage = "Swiftpay processing complete";
                i.IsBusy = false;
            }
            catch (Exception e)
            {
                i.StatusMessage = "Swift Pay processing error for batch {0}";
                i.ErrorMessage = e.Message;
            }
            return i;
        }

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
                var status = PaymentPrintSvc.PrintCheck(app, b, c, checkNum++);
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

        public static Task<StatusInfo> UnprintChecksAsync(int accountId, int batchNum, int startingCheckNum)
        {
            throw new NotImplementedException();
        }

        public static Task<StatusInfo> CancelSwiftFulfillAsync(int accountId, int batchNum)
        {
            throw new NotImplementedException();
        }
    }
}