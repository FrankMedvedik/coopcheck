using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc 
    {
        public static string Payee(string company, string fulllName)
        {
            if (company.Length != 0) return company;
            return fulllName;
        }

        public static async Task<StatusInfo> SwiftFulfillAsync(int accountId, int batchNum)
        {
            StatusInfo i = new StatusInfo();
            i.StatusMessage = "Batch sumbitted to Swift Pay";
            try
            {
               // await Task.Factory.StartNew(() => BatchSwiftFulfillCommand.BatchSwiftFulfill(batchNum));
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
            StatusInfo i = new StatusInfo();
            i.StatusMessage = "Batch sumbitted to Swift Pay";
            return i;
            //  return await Task<string>.Factory.StartNew(() => PrintChecks(accountId, batchNum, startingCheckNum));
        }

        private static string PrintChecks(int accountId, int batchNum, int startingCheckNum)
        {
            string retVal = string.Empty; 
            var app = new Microsoft.Office.Interop.Word.Application();
            var doc = new Microsoft.Office.Interop.Word.Document();
            doc = app.Documents.Add(Template: @Properties.Settings.Default.CheckTemplate);
            app.Visible = true;
            int checkNum = startingCheckNum;
            var b = BatchEdit.GetBatchEdit(batchNum);
            foreach (var c in b.Vouchers)
            {
                try
                {
                    foreach (Microsoft.Office.Interop.Word.Field f in doc.Fields)
                    {
                        if (f.Code.Text.Contains("thank_you_1"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.ThankYou1 ?? "");

                        }
                        else if (f.Code.Text.Contains("thank_you_2"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.ThankYou2 ?? "");
                        }

                        else if (f.Code.Text.Contains("marketing_research_message"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.MarketingResearchMessage ?? "");
                        }


                        else if (f.Code.Text.Contains("job_num"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.JobNum.ToString());

                        }

                        else if (f.Code.Text.Contains("batch_num"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.Num.ToString());
                        }

                        else if (f.Code.Text.Contains("check_date"))
                        {
                            f.Select();
                            app.Selection.TypeText(DateTime.Now.ToString("MMMM dd, yyyy"));
                        }

                        else if (f.Code.Text.Contains("check_num"))
                        {
                            f.Select();

                            app.Selection.TypeText(checkNum.ToString());
                        }
                        else if (f.Code.Text.Contains("tran_amount"))
                        {
                            f.Select();

                            app.Selection.TypeText(c.Amount.GetValueOrDefault().ToString());
                        }

                        else if (f.Code.Text.Contains("tran_amount"))
                        {
                            f.Select();
                            app.Selection.TypeText(c.Amount.GetValueOrDefault().ToString());
                        }

                        else if (f.Code.Text.Contains("tran_amount_text"))
                        {
                            f.Select();
                            app.Selection.TypeText(NumberConverter.NumberToCurrencyText(c.Amount.GetValueOrDefault(0),
                                MidpointRounding.AwayFromZero));
                        }

                        else if (f.Code.Text.Contains("full_name"))
                        {
                            f.Select();
                            app.Selection.TypeText(Payee(c.Company, c.FullName));
                        }
                        else if (f.Code.Text.Contains("address_1"))
                        {
                            f.Select();
                            app.Selection.TypeText(c.AddressLine1);
                        }
                        else if (f.Code.Text.Contains("address_2"))
                        {
                            f.Select();
                            if (c.AddressLine2.Length != 0)
                                app.Selection.TypeText(c.AddressLine2 ?? "");


                        }
                        else if (f.Code.Text.Contains("address_3"))
                        {
                            f.Select();
                            app.Selection.TypeText(String.Join(" ", c.Municipality, c.Region, c.PostalCode, c.Country));
                        }

                        var list = WriteCheckCommand.Execute(batchNum, accountId, checkNum);

                        retVal = string.Empty;
                        CommitCheckCommand.Execute(batchNum, checkNum);
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        doc.Close();
                        app.Quit();
                    }
                    catch (Exception x)
                    {

                    }
                        return String.Format("Error {0}, printing checks failed : Batch {1} Payee {2} Transaction Id {3}",
                                e.Message, batchNum, Payee(c.Company, c.FullName), c.Id);
                }
                checkNum++;
                }

 //               doc.PrintOut();
                doc.SaveAs2(FileName: Properties.Settings.Default.CheckDirectory + "checks." + accountId + "." + batchNum.ToString() + ".doc");
                doc.Close();
                app.Quit();
                retVal = String.Format("{0} checks printed: First check # {1} Last Check #: {2)", b.Vouchers.Count, startingCheckNum, checkNum);

            return retVal;
        }

        public static Task<int> NextCheckNum(int accountId)
        {
            return Task<int>.Factory.StartNew(() => NextCheckNumCommand.Execute(accountId));
        }


        public static async Task<List<vwPayment>> GetPayments(PaymentFinderCriteria crc)
        {
            IQueryable<vwPayment> query;
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                query =
                    ctx.vwPayments.Where(
                        x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);
                if (!String.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = query.Where(x => x.check_num == crc.CheckNumber);

                }
                if (!String.IsNullOrWhiteSpace(crc.Email))
                {
                    query = query.Where(x => x.email.Contains(crc.Email));

                }

                if (!String.IsNullOrWhiteSpace(crc.PhoneNumber))
                {
                    query = query.Where(x => x.phone_number.Contains(crc.PhoneNumber));

                }

                if (!String.IsNullOrEmpty(crc.LastName))
                {
                    query = query.Where(x => x.last_name.Contains(crc.LastName));
                }

                if (!String.IsNullOrEmpty(crc.FirstName))
                {
                    query = query.Where(x => x.first_name.Contains(crc.FirstName));
                }

                if (!String.IsNullOrEmpty(crc.JobNumber))
                {
                    int n;
                    if (int.TryParse(crc.JobNumber, out n))
                        query = query.Where(x => x.job_num == n);
                }

                //string ob = String.IsNullOrEmpty(b.last_name) ? b.company : b.last_name;

                v = await (from b in query
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwPayment>> GetPayments(int jobNum)
        {
            List<vwPayment> v; 
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                               where b.job_num == jobNum
                               select b).ToListAsync();
            }
            return v;
        }
    }
}