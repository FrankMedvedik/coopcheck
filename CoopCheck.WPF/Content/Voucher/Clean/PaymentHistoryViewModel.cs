using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class PaymentHistoryViewModel : ViewModelBase, IPaymentHistoryViewModel
    {
        private ObservableCollection<Paymnt> _payments = new ObservableCollection<Paymnt>();

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

        public ObservableCollection<Paymnt> Payments
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
                var pay = await _rptSvc.GetPayeePayments(LastName, FirstName);
                Payments = new ObservableCollection<Paymnt>(pay.Cast<Paymnt>().ToList());
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
            _payments = new ObservableCollection<Paymnt>();
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

        private Paymnt _selectedPayment;
        private string _lastName;
        private string _firstName;
        private readonly IRptSvc _rptSvc;

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