using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Models;
using Microsoft.Office.Interop.Word;

namespace CoopCheck.Domain.Services
{
    public  class OutstandingBalancePrintSvc
    {
        private IDictionary<string, string> _settings;

        public OutstandingBalancePrintSvc(IDictionary<string, string> settings)
        {
            _settings = settings;
        }
        public  async Task<StatusInfo> PrintOutstandingBalanceReportAsync(OutstandingBalanceStats stats)
        {
            return await Task<StatusInfo>.Factory.StartNew(() =>
            {
                var status = new StatusInfo();
                try
                {
                    var app = new Application();
                    status = PrintOutstandingBalanceReport(app, stats);
                    app.Quit();
                }
                catch (Exception e)
                {
                    status.StatusMessage = "printing failed";
                    status.ErrorMessage = e.Message;
                }
                return status;
            });
        }


        public  StatusInfo PrintOutstandingBalanceReport(Application app, OutstandingBalanceStats b)
        {
            var template = AppDomain.CurrentDomain.BaseDirectory + _settings["OutstandingBalanceReportTemplate"];
            try
            {
                var doc = new Document();
                doc = app.Documents.Add(template);

                #region mailmerge

                foreach (Field f in doc.Fields)
                {
                    if (f.Code.Text.Contains("AccountName"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.AccountName ?? "");
                    }
                    else if (f.Code.Text.Contains("OutstandingBalance"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.OutstandingBalance ?? "");
                    }
                    else if (f.Code.Text.Contains("AsOfDate"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.AsOfDate.ToLongDateString() ?? "");
                    }

                    else if (f.Code.Text.Contains("ItemCount"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.ItemCount ?? "");
                    }


                    else if (f.Code.Text.Contains("PreparedBy"))
                    {
                        f.Select();
                        app.Selection.TypeText(b.PreparedBy);
                    }
                }

                #endregion

                doc.PrintOut();
                doc.Close(false);
                return new StatusInfo
                {
                    StatusMessage =
                        "Printed"
                };
            }
            catch (Exception e)
            {
                return new StatusInfo
                {
                    StatusMessage = "printing failed",
                    ErrorMessage = e.Message
                };
            }
          }
    }
}