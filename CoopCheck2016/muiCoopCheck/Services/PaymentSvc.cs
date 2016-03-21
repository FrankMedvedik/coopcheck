using System;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc 
    {
        public static int MAX_PAYMENT_COUNT = 3000;

   
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

       public static async Task<StatusInfo> PrintChecksAsync(int accountId, int batchNum, int startingCheckNum)
       {
           //return await Task<StatusInfo>.Factory.StartNew(() =>
           //{
           //    StatusInfo i = new StatusInfo();
           //    i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
           //    System.Threading.Thread.Sleep(10000);
           //    return i;
           //});
          return await Task<StatusInfo>.Factory.StartNew(() => PrintChecks(accountId, batchNum, startingCheckNum));
       }

        private static StatusInfo PrintChecks(int accountId, int batchNum, int startingCheckNum)
        {
            //return 
            var status = new StatusInfo();

            var b = BatchEdit.GetBatchEdit(batchNum);
            int checkNum = startingCheckNum;

            // WriteCheckBatchCommand works like a begin transaction and 
            // marks the whole batch as in progress... the check number is defined here but the print flag is set at the end. 
            // see commit afterwards

            WriteCheckBatchCommand.Execute(batchNum, accountId, startingCheckNum);

            var app = new Microsoft.Office.Interop.Word.Application();
            foreach (var c in b.Vouchers)
            {
                status = PaymentPrintSvc.PrintCheck(app,b, c, checkNum++);
                if (status.ErrorMessage == null)
                {
                    status.IsBusy = true;
                    Messenger.Default.Send(new NotificationMessage<StatusInfo>(status, Notifications.StatusInfoChanged));
                }
                else
                {
                    app.Quit(false);
                    return status;
                }
            }
            app.Quit(false);
            return new StatusInfo() 
            {
                    StatusMessage =
                    String.Format("Batch {0}  Printed", batchNum)
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