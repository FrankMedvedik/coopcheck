using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Void
{
    /// <summary>
    public class VoidedPaymentReportViewModel : ViewModelBase
    {
        private ObservableCollection<VoidedPaymnt> _payments = new ObservableCollection<VoidedPaymnt>();

        public VoidedPaymentReportViewModel()
        {
            ResetState();
        }

        public ObservableCollection<VoidedPaymnt> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                Status = new StatusInfo
                {
                    StatusMessage = string.Format("{0} Payments found", Payments.Count)
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
                Payments = await Task<ObservableCollection<VoidedPaymnt>>.Factory.StartNew(() =>
                {
                    var task = RptSvc.GetVoidedPayments(p);
                    return new ObservableCollection<VoidedPaymnt>(task.Result);
                });
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
            _payments = new ObservableCollection<VoidedPaymnt>();
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

        private VoidedPaymnt _selectedPayment;

        public VoidedPaymnt SelectedPayment
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