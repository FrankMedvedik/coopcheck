using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using log4net;

namespace CoopCheck.WPF.Services
{
    public static class BatchSvc
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static Task<BatchEdit> GetBatchEditAsync(int batchNum)
        {
            return Task<BatchEdit>.Factory.StartNew(() => BatchEdit.GetBatchEdit(batchNum));
        }

        public static Task<BatchEdit> GetNewBatchEditAsync()
        {
            return Task<BatchEdit>.Factory.StartNew(() => BatchEdit.NewBatchEdit());
        }

        public static Task DeleteBatchEditAsync(int batchNum)
        {
            return Task.Factory.StartNew(() => BatchEdit.DeleteBatchEdit(batchNum));
        }

        public static Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }

        public static int ImportVouchers(List<VoucherImport> vouchers)
        {
            var sb = BatchEdit.NewBatchEdit();
            VoucherImport currentVoucher = null;
            try
            {
                sb.JobNum = int.Parse(vouchers.Select(x => x.JobNumber).First());
            }
            catch (Exception )
            {
                sb.JobNum = -1;
            }
            sb.Amount = vouchers.Select(x => x.Amount).Sum().GetValueOrDefault(0);
            sb.Date = DateTime.Today.ToShortDateString();


            try
            {
                sb = sb.Save();
                //using (var ctx = new CoopCheckEntities())
                //{
                log.Info(DateTime.Now.ToLongTimeString() + " before records added");
    
            foreach (var v in vouchers)
            {
                sb.Vouchers.Add(VoucherImportConverter.ToVoucherEdit(v));
                    //    if (!sb.Vouchers.IsValid)
                    //    {
                    //        sb.Vouchers.
                    //    }
                    //            thow new ApplicationException()
                }
                sb = sb.Save();
                log.Info(string.Format("{0} - batch saved", sb.Num));
            }
            catch (Exception e)
            {
                log.Error(string.Format("batch save failed  {0} - {1}", "Voucher = " + (currentVoucher?.EmailAddress) ?? "", e.Message));
                BatchEdit.DeleteBatchEdit(sb.Num);
                throw (new Exception(string.Format("batch save failed  {0} - {1}", "Voucher = " + (currentVoucher?.EmailAddress) ?? "", e.Message)));
            }
            return sb.Num;
        }
    }
}