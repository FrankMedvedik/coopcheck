using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class PaymentHistoryViewModel : ViewModelBase, IPaymentHistoryViewModel
    {
        private ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();

        public PaymentHistoryViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
            ResetState();

            Messenger.Default.Register<NotificationMessage<PaymentReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.FindPayeePayments)
                {
                    LastName = message.Content.LastName;
                    FirstName = message.Content.FirstName;
                    GetPayments();
                }
            });
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
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

        public ObservableCollection<Payment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }


        public async void GetPayments()
        {
            try
            {
                Payments = new ObservableCollection<Payment>(await _rptSvc.GetPayeePayments(LastName, FirstName));
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

        #region DisplayState

        private StatusInfo _status;

        public void ResetState()
        {
            _payments = new ObservableCollection<Payment>();
            ShowGridData = false;
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

        #endregion

        #region collection and selected

        private Payment _selectedPayment;
        private string _lastName;
        private string _firstName;
        private readonly IRptSvc _rptSvc;

        public Payment SelectedPayment
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