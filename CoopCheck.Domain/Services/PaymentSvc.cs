using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;

using CoopCheck.Library;
using Microsoft.Office.Interop.Word;


namespace CoopCheck.Domain.Services
{
    public class PaymentSvc
    {
        private IDictionary<string, string> _settings;
        private ICredentials _credentials;
        public PaymentSvc(IDictionary<string,string> settings, ICredentials credentials, PaymentPrintSvc paymentPrintSvc )
        {
            _settings = settings;
            _credentials = credentials;
            _paymentPrintSvc = paymentPrintSvc;
        }

        public readonly int MAX_PAYMENT_COUNT = 10000;
        private PaymentPrintSvc _paymentPrintSvc;

        public  async Task<StatusInfo> SwiftFulfillAsync(int batchNum)
        {
            var i = new StatusInfo();
            try
            {
                using (var client = new HttpClient(new HttpClientHandler { Credentials = _credentials }))
                {
                    client.BaseAddress = new Uri(_settings["SwiftPaySite"]);
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
        public async Task<StatusInfo> SwiftVoidAsync(int batchNum)
        {
            var i = new StatusInfo();
            try
            {
                using (var client = new HttpClient(new HttpClientHandler { Credentials = _credentials }))
                {

                    client.BaseAddress = new Uri(_settings["SwiftPaySite"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // HTTP POST
                    var response = await client.PostAsync(String.Format("api/swiftPayment/swiftvoid?batchNum={0}", batchNum), null);
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

        
        public async Task<PrintCheckProgress> PrintChecksAsync(int accountId, int batchNum, int startingCheckNum,
            CancellationToken ctx, Progress<PrintCheckProgress> progress)
        {
            return
                await
                    Task<PrintCheckProgress>.Factory.StartNew(
                        () => PrintChecks(accountId, batchNum, startingCheckNum, ctx, progress), ctx);
        }

        private  PrintCheckProgress PrintChecks(int accountId, int batchNum, int startingCheckNum,
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
                var status = _paymentPrintSvc.PrintCheck(app, b, c, checkNum++);
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

        public  Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }
    }
}