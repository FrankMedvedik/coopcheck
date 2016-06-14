using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount.Open
{
    public class OpenPaymentReportViewModel : ViewModelBase
    {
        private string _headingText;

        private ObservableCollection<Paymnt> _payments = new ObservableCollection<Paymnt>();

        public OpenPaymentReportViewModel()
        {
            ResetState();
        }

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }

        public int PaymentsCnt
        {
            get { return Payments.Count(); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Payments.Sum(x => x.tran_amount); }
        }

        public ObservableCollection<Paymnt> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
                Status = new StatusInfo
                {
                    StatusMessage = $"{PaymentsCnt} Payments found, Total Dollars {PaymentsTotalDollars:c}"
                };
                ShowGridData = true;
            }
        }

        public async void GetPayments(PaymentReportCriteria p)
        {
            Status = new StatusInfo
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };
            try
            {
                ShowGridData = false;
                Payments = await Task<ObservableCollection<Paymnt>>.Factory.StartNew(() =>
                {
                    var task = RptSvc.GetOpenPayments(p);
                    var v = new ObservableCollection<Paymnt>(task.Result);
                    return v;
                });
                HeadingText = string.Format("{0} Account Open Payment Report for Payments as of {1:ddd, MMM d, yyyy}",
                    p.Account.account_name, p.EndDate);
            }
            catch (Exception e)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error loading payments",
                    ErrorMessage = e.Message
                };
            }
        }


        public async void PrintReport(PaymentReportCriteria c)
        {
            var s = new OutstandingBalanceStats
            {
                AccountName = c.Account.account_name,
                AsOfDate = c.EndDate,
                ItemCount = PaymentsCnt.ToString(),
                OutstandingBalance = string.Format("{0:c}", PaymentsTotalDollars),
                PreparedBy = UserAuth.Instance.UserName
            };

            await OutstandingBalancePrintSvc.PrintOutstandingBalanceReportAsync(s);
        }

        #region DisplayState

        private StatusInfo _status;

        public void ResetState()
        {
            _payments = new ObservableCollection<Paymnt>();
            ShowGridData = false;
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        private bool _showGridData;

        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region collection and selected

        private Paymnt _selectedPayment;

        public Paymnt SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}