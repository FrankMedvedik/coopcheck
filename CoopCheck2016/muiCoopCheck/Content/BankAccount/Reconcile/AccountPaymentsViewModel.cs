using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.DAL;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public class AccountPaymentsViewModel : ViewModelBase
    {
        public async Task GetPayments()
        {
            AllPayments = await RptSvc.GetPaymentReconcileReport(PaymentReportCriteria);
        }

        private ObservableCollection<KeyValuePair<String, String>> _stats =
            new ObservableCollection<KeyValuePair<string, string>>();

        public List<vwPayment> MatchedPayments
        {
            get { return _matchedPayments; }
            set
            {
                _matchedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public List<vwPayment> OpenPayments
        {
            get { return _openPayments; }
            set
            {
                _openPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<String, String>> Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                NotifyPropertyChanged();
            }
        }

        public List<CheckInfoDto> ChecksToClear
        {
            get { return _checksToClear; }
            set
            {
                _checksToClear = value;
                NotifyPropertyChanged();
            }
        }

        private List<CheckInfoDto> _checksToClear = new List<CheckInfoDto>();
        private List<vwPayment> _allPayments = new List<vwPayment>();
        private List<vwPayment> _matchedPayments = new List<vwPayment>();
        private List<vwPayment> _openPayments = new List<vwPayment>();
        private PaymentReportCriteria _paymentReportCriteria;

        public List<vwPayment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
            }
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
            }
        }



        public async void LoadStats()
        {
            await Task.Factory.StartNew(() =>
            {
                List<KeyValuePair<String, String>> s =
                    new List<KeyValuePair<string, string>>();
                var matchedTransactionAmt = MatchedPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
                var matchedTransactionCnt = MatchedPayments.Count;
                //var openTransactionAmt = OpenPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
                //var openTransactionCnt = OpenPayments.Count;
                //var allTransactionAmt = AllPayments.Sum(p => p.tran_amount).GetValueOrDefault(0);
                //var allTransactionCnt = AllPayments.Count;
                //s.Add(new KeyValuePair<string, string>("Total Payments Cnt", String.Format("{0:n0}", allTransactionCnt)));
                //s.Add(new KeyValuePair<string, string>("Total Payments", String.Format("{0:c}", allTransactionAmt)));
                s.Add(new KeyValuePair<string, string>("Matched Payments Cnt",
                    String.Format("{0:n0}", matchedTransactionCnt)));
                s.Add(new KeyValuePair<string, string>("Matched Payments", String.Format("{0:c}", matchedTransactionAmt)));
                //s.Add(new KeyValuePair<string, string>("Open Payments Cnt", String.Format("{0:n0}", openTransactionCnt)));
                //s.Add(new KeyValuePair<string, string>("Open Payments", String.Format("{0:c}", openTransactionAmt)));
                foreach(var si in s)
                    Stats.Add(si);
            });
        }

        //    public async void SaveReconciliationRptToExcel()
        //    {
        //        Status = new StatusInfo()
        //        {
        //            StatusMessage = "exporting vouchers to Excel",
        //            IsBusy = true
        //        };

        //        await Task.Factory.StartNew(() =>
        //        {
        //            string dtFmt = String.Format("{0:g}", DateTime.Now).Replace('/', '.');
        //            dtFmt = dtFmt.Replace(':', '.');
        //            dtFmt = dtFmt.Replace(' ', '.');

        //            try
        //            {
        //                if (Stats.Count > 0)
        //                {
        //                    ExportToExcel<KeyValuePair<String, String>>, Stats> s = new ExportToExcel<KeyValuePair<String, String>>, Stats>
        //                    {
        //                        ExcelSourceWorkbook = ExcelFileInfo.ExcelFilePath,
        //                        ExcelWorksheetName = String.Format("Errors.{0}", dtFmt),
        //                        dataToPrint = BadVoucherExports
        //                    };
        //                    s.GenerateReport();

        //                    if (GoodVoucherExports.Count > 0)
        //                    {
        //                        s.ExcelWorksheetName = String.Format("Processed.{0}", dtFmt);
        //                        s.dataToPrint = GoodVoucherExports;
        //                        s.GenerateReport();
        //                        CanExport = false;
        //                    }
        //                    ErrorBatchInfoMessage = String.Format("Vouchers Exported to {0}",
        //                        Path.GetFileName(ExcelFileInfo.ExcelFilePath));
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Status = new StatusInfo()
        //                {
        //                    StatusMessage = "export to excel failed",
        //                    ErrorMessage = e.Message //,ShowMessageBox = true
        //                };
        //            }
        //        });
        //    }
        //
    }
}