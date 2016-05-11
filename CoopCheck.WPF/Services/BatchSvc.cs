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
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + " before records added");
                    //foreach (var v in vouchers)
                    //    {
                    //        ctx.check_tran.Add(new check_tran
                    //        {
                    //            address_1 = v.AddressLine1,
                    //            address_2 = v.AddressLine2,
                    //            batch_num = sb.Num,
                    //            company = v.Company,
                    //            country = v.Country,
                    //            email = v.EmailAddress,
                    //            first_name = v.First,
                    //            last_name = v.Last,
                    //            middle_name = v.Middle,
                    //            municipality = v.Municipality,
                    //            name_prefix = v.NamePrefix,
                    //            name_suffix = v.Suffix,
                    //            phone_number = v.PhoneNumber,
                    //            postal_code = v.PostalCode,
                    //            region = v.Region,
                    //            title = v.Title,
                    //            tran_amount = v.Amount
                    //        });
                    //        currentVoucher = v;
                    //    }
                    //    Console.WriteLine(DateTime.Now.ToLongTimeString() + " after add");
                    //    ctx.SaveChanges();
                    //    Console.WriteLine(DateTime.Now.ToLongTimeString() + " after save");
                    //}
            foreach (var v in vouchers)
            {
                sb.Vouchers.Add(VoucherImportConverter.ToVoucherEdit(v));
                //sb.Vouchers.AddVoucher(v.Amount.GetValueOrDefault(), v.RecordID, v.NamePrefix, v.First,
                //    v.Middle, v.Last, v.Suffix,
                //    v.Title, v.Company, v.AddressLine1, v.AddressLine2,
                //    v.Municipality, v.Region, v.PostalCode, v.Country, v.PhoneNumber, v.EmailAddress);
            }
                sb = sb.Save();
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " batch saved");
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