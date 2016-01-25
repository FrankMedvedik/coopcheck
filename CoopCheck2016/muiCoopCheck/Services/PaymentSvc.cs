using System;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc 
    {
        public static int MAX_PAYMENT_COUNT = 100;

   
        public static async Task<StatusInfo> SwiftFulfillAsync(int accountId, int batchNum)
        {
            StatusInfo i = new StatusInfo()
            {
                StatusMessage = String.Format("Batch {0} Submitted for swiftpay ", batchNum)//,ShowMessageBox = true
            };
            // test code 
            //i.StatusMessage = "            StatusInfo i = new StatusInfo();
            //i.StatusMessage = "LETS PRETEND THE BATCH IS GONE TO BE PAID NOW... ";
            //System.Threading.Thread.Sleep(5000);
            //return i;
            try
            {
             
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
            //StatusInfo i = new StatusInfo();
            //i.StatusMessage = "LETS PRETEND THE CHECKS ARE PRINTED NOW... ";
            //System.Threading.Thread.Sleep(5000);
            //return i;
             return await Task<StatusInfo>.Factory.StartNew(() => PrintChecks(accountId, batchNum, startingCheckNum));
        }

        private static StatusInfo PrintChecks(int accountId, int batchNum, int startingCheckNum)
        {
            //return 
            var status = new StatusInfo();

            var b = BatchEdit.GetBatchEdit(batchNum);
            int checkNum = startingCheckNum + b.Vouchers.Count-1;

            // the write check should happen AFTER THE PHYSICAL CHECKS ARE PRINTED 

            WriteCheckBatchCommand.Execute(batchNum, accountId, startingCheckNum);
            //foreach (var c in b.Vouchers)
            //{
            //    //status = PaymentPrintSvc.PrintCheck(b, c, checkNum);
            //    //if (status.ErrorMessage == null)
            //    //{
            //        WriteCheckCommand.Execute(batchNum, accountId, checkNum);
            //        //status.IsBusy = true;
            //        //status.ShowMessageBox = false;
            //        //Messenger.Default.Send(new NotificationMessage<StatusInfo>(status, Notifications.StatusInfoChanged));
            //    //}
            //    //else
            //    //{
            //    //    // commit all the checks up to the one that errored out
            //    //    // send ERROR message to popup 
            //    //    CommitCheckCommand.Execute(batchNum, --checkNum);
            //    //    status.ShowMessageBox = true;
            //    //    return status;
            //    //}
            //}
            // commit the check operation to the database
            // send completion message to popup with counts and account
            CommitCheckCommand.Execute(batchNum, checkNum);
            return new StatusInfo() 
            {
                    StatusMessage =
                    String.Format("Batch {0} First Check # {1} Last Check # {2} Printed", batchNum,
                        startingCheckNum, checkNum)//,ShowMessageBox = true
            };
      }

   }
}