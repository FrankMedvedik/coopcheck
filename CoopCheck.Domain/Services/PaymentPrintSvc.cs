using System;
using System.Collections.Generic;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Library;
using CoopCheck.Library.Contracts.Interfaces;
using Microsoft.Office.Interop.Word;
using Reckner.WPF.Converters;

namespace CoopCheck.Domain.Services
{
    public class PaymentPrintSvc  
    {
        private IDictionary<string, string> _settings;

        public PaymentPrintSvc(IDictionary<string, string> settings)
        {
            _settings = settings;
        }
        public string Payee(string company, string fulllName)
        {
            if (company.Length != 0) return company;
            return fulllName;
        }

        public StatusInfo PrintCheck(Application app, BatchEdit b, IVoucherEdit c, int checkNum)
        {
            var template = AppDomain.CurrentDomain.BaseDirectory + _settings["CheckTemplate"];
            try
            {
                var doc = new Document();
                doc = app.Documents.Add(template);

                #region mailmerge

                foreach (Field f in doc.Fields)
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
                        app.Selection.TypeText(Convert.ToDateTime(DateTime.Now).ToString("MMMM dd, yyyy"));
                    }

                    else if (f.Code.Text.Contains("check_num"))
                    {
                        f.Select();

                        app.Selection.TypeText(checkNum.ToString());
                    }

                    else if (f.Code.Text.Contains("tran_amount"))
                    {
                        f.Select();
                        app.Selection.TypeText(string.Format("{0:C}", c.Amount.GetValueOrDefault(0)));
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
                        app.Selection.TypeText(string.Join(" ", c.Municipality, c.Region, c.PostalCode, c.Country));
                    }
                }

                #endregion

                doc.PrintOut();
                doc.Close(false);
                //System.Threading.Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                return new StatusInfo
                {
                    StatusMessage = string.Format("checks printing stopped at check {0} payee {1} ",
                        checkNum, Payee(c.Company, c.FullName)),
                    ErrorMessage = e.Message
                };
            }
            return new StatusInfo
            {
                StatusMessage = string.Format("printed - check {0} payee {1}", checkNum, Payee(c.Company, c.FullName))
            };
        }
    }
}