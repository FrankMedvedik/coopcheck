using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Reports.Contracts.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Void
{
    /// <summary>
    public class VoidedPaymentReportViewModel : ViewModelBase, IVoidedPaymentReportViewModel
    {
        private ObservableCollection<VoidedPaymnt> _payments = new ObservableCollection<VoidedPaymnt>();

        public VoidedPaymentReportViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
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

                var vs = await _rptSvc.GetVoidedPayments(p);
                var payments = new List<VoidedPaymnt>();
                foreach (var v in vs)
                {
                    payments.Add((VoidedPaymnt) v);
                }
               Payments = new ObservableCollection<VoidedPaymnt>(payments);

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
        private IRptSvc _rptSvc;

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