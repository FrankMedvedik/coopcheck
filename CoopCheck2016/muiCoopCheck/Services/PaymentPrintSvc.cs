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
    public static class PaymentPrintSvc 
    {
        public static string Payee(string company, string fulllName)
        {
            if (company.Length != 0) return company;
            return fulllName;
        }




        public static StatusInfo PrintCheck(Microsoft.Office.Interop.Word.Application app, BatchEdit b, VoucherEdit c,  int checkNum)
        {
            var template = System.AppDomain.CurrentDomain.BaseDirectory +  @Properties.Settings.Default.CheckTemplate;
            try
            {

                var doc = new Microsoft.Office.Interop.Word.Document();
                doc = app.Documents.Add(Template: template);
                
                #region mailmerge
                foreach (Microsoft.Office.Interop.Word.Field f in doc.Fields)
                    {
                        if (f.Code.Text.Contains("thank_you_1"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.ThankYou1 ?? "");

                        }
                        else if (f.Code.Text.Contains("study_topic"))
                        {
                            f.Select();
                            app.Selection.TypeText(b.StudyTopic ?? "");
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
                            app.Selection.TypeText(Convert.ToDateTime(b.PayDate).ToString("MMMM dd, yyyy"));
                        }

                        else if (f.Code.Text.Contains("check_num"))
                        {
                            f.Select();

                            app.Selection.TypeText(checkNum.ToString());
                        }
               
                        else if (f.Code.Text.Contains("tran_amount"))
                        {
                            f.Select();
                            app.Selection.TypeText(string.Format("{0:C}",c.Amount.GetValueOrDefault(0)));
                        }

                        else if (f.Code.Text.Contains("tran_amt_text"))
                        {
                            f.Select();
                            var v = NumberConverter.NumberToCurrencyText(c.Amount.GetValueOrDefault(0));
                            app.Selection.TypeText(v);
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
                            var v = (c.AddressLine2.Trim().Length != 0) ? c.AddressLine2 : "   ";
                            app.Selection.TypeText(v);

                        }
                        else if (f.Code.Text.Contains("address_3"))
                        {
                            f.Select();
                            app.Selection.TypeText(String.Join(" ", c.Municipality, c.Region, c.PostalCode, c.Country));
                        }

                }
                #endregion

                doc.PrintOut();
                doc.Close(false);
                
            }
            catch (Exception e)
                {
                 return   new StatusInfo()
                    {
                       StatusMessage = String.Format("printing checks failed stopped at Batch {0} Payee {1} Transaction Id {2}",
                       b.Num, Payee(c.Company, c.FullName), c.Id),
                       ErrorMessage = e.Message
                    };
                
        }

            // app.PrintOut(doc);
         //   doc.SaveAs(string.Format("check.{0}.{1}.doc", b.Num, c.Id));
           // doc = new Microsoft.Office.Interop.Word.Document();
           //doc = app.Documents.Add(Template: @Properties.Settings.Default.CheckTemplate);
            return new StatusInfo() 
                    {
                            StatusMessage = String.Format("printed - Batch {0} Payee {1} Transaction Id {2}",
                                   b.Num, Payee(c.Company, c.FullName), c.Id),
            };
      }
    }
}