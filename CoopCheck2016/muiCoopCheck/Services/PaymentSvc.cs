using System;
using System.Threading;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Content.Voucher.Pay;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc 
    {
        public static int MAX_PAYMENT_COUNT = 10000;

   
        public static async Task<StatusInfo> SwiftFulfillAsync(int accountId, int batchNum)
        {
            StatusInfo i = new StatusInfo();
            try
            {
                //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
                //System.Threading.Thread.Sleep(5000);
                await Task.Factory.StartNew(() => BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum));
                
            }
            catch (Exception e)
            {
                i.StatusMessage = "Swift Pay processing error";
                i.ErrorMessage = e.Message;
            }
            return i;
        }

       public static async Task<PrintCheckProgress> PrintChecksAsync(int accountId, int batchNum, int startingCheckNum, CancellationToken ctx, Progress<PrintCheckProgress> progress)
       {
           //return await Task<StatusInfo>.Factory.StartNew(() =>
           //{
           //    StatusInfo i = new StatusInfo();
           //    i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
           //    System.Threading.Thread.Sleep(10000);
           //    return i;
           //});
          return await Task<PrintCheckProgress>.Factory.StartNew(() => PrintChecks(accountId, batchNum, startingCheckNum, ctx,  progress));
       }

        private static PrintCheckProgress PrintChecks(int accountId, int batchNum, int startingCheckNum, CancellationToken ctx, IProgress<PrintCheckProgress> progress)
        {
            //return 
            var retVal = new PrintCheckProgress();

            var b = BatchEdit.GetBatchEdit(batchNum);
            int checkNum = startingCheckNum;
            int currentCheckPct = 0;
                

            // WriteCheckBatchCommand works like a begin transaction and 
            // marks the whole batch as in progress... the check number is defined here but the print flag is set at the end. 
            // see commit afterwards

            WriteCheckBatchCommand.Execute(batchNum, accountId, startingCheckNum);

            var app = new Microsoft.Office.Interop.Word.Application();
            foreach (var c in b.Vouchers)
            {
                var status = PaymentPrintSvc.PrintCheck(app,b, c, checkNum++);
                    status.IsBusy = true;
                ++currentCheckPct;
                var z = currentCheckPct / b.Vouchers.Count * 100;
                retVal.ProgressPercentage = ((decimal) currentCheckPct / (decimal) b.Vouchers.Count * 100);
                retVal.CurrentCheckNum = checkNum;
                    retVal.Status = status;
                progress?.Report(retVal); // calls SynchronizationContext.Post
                if (retVal.Status.ErrorMessage != null)
                {
                    app.Quit(false);
                    return retVal;
                }
                if(ctx.IsCancellationRequested)
                    app.Quit(false);
                ctx.ThrowIfCancellationRequested();
            }
            app.Quit(false);
            return new PrintCheckProgress
            {
                 ProgressPercentage =  ((decimal)currentCheckPct / (decimal)b.Vouchers.Count * 100),
            Status = new StatusInfo()
                {
                    StatusMessage =
                        String.Format("Batch {0}  Printed", batchNum)
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