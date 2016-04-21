using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class BatchSvc
    {
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
            catch (Exception e)
            {
                sb.JobNum = -1;
            }
            sb.Amount = vouchers.Select(x => x.Amount).Sum().GetValueOrDefault(0);
            sb.Date = DateTime.Today.ToShortDateString();
    
           
            try
            {
                sb = sb.Save();
                foreach(var v in vouchers)
                {
                    currentVoucher = v;
                    sb.Vouchers.Add(VoucherImportConverter.ToVoucherEdit(v));
                  
                }
                sb = sb.Save();
            }
            catch (Exception e)
            {
                BatchEdit.DeleteBatchEdit(sb.Num);
                throw (new Exception(string.Format("batch import failed  {0} - {1}",
                    "Voucher = " + (currentVoucher?.EmailAddress) ?? "", e.Message)));
            }
            return sb.Num;
        }
    }
}