using System;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Services
{
    public static class OutstandingBalancePrintSvc
    {
        public static async Task<StatusInfo> PrintOutstandingBalanceReportAsync(OutstandingBalanceStats stats)
        {
            return await Task<StatusInfo>.Factory.StartNew(() =>
            {
                var status = new StatusInfo();
                try
                {
                    var app = new Microsoft.Office.Interop.Word.Application();
                    status = PrintOutstandingBalanceReport(app, stats);
                    app.Quit();
                }
                catch (Exception e)
                {
                    status.StatusMessage = String.Format("printing failed");
                    status.ErrorMessage = e.Message;
                }
                return status;
            });
        }
    

    public static StatusInfo PrintOutstandingBalanceReport(Microsoft.Office.Interop.Word.Application app, OutstandingBalanceStats b)
    {
        var template = System.AppDomain.CurrentDomain.BaseDirectory + @Properties.Settings.Default.OutstandingBalanceReportTemplate;
        try
        {

            var doc = new Microsoft.Office.Interop.Word.Document();
            doc = app.Documents.Add(Template: template);

            #region mailmerge
            foreach (Microsoft.Office.Interop.Word.Field f in doc.Fields)
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
                    app.Selection.TypeText(b.ItemCount  ?? "");
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
            return new StatusInfo()
            {
                StatusMessage =
                String.Format("Printed")
            };
        }
        catch (Exception e)
        {
            return new StatusInfo()
            {
                StatusMessage = String.Format("printing failed"),
                ErrorMessage = e.Message
            };
        }

        // app.PrintOut(doc);
        //   doc.SaveAs(string.Format("check.{0}.{1}.doc", b.Num, c.Id));
        // doc = new Microsoft.Office.Interop.Word.Document();
        //doc = app.Documents.Add(Template: @Properties.Settings.Default.CheckTemplate);
        return new StatusInfo()
        {
            StatusMessage = String.Format("printed")
        };
    }
}


}