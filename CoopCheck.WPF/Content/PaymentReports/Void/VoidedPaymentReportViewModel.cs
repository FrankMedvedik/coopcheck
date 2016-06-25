using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Void
{
    /// <summary>
    public class VoidedPaymentReportViewModel : ViewModelBase, IVoidedPaymentReportViewModel
    {
        private ObservableCollection<VoidedPayment> _payments = new ObservableCollection<VoidedPayment>();

        public VoidedPaymentReportViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
            ResetState();
        }

        public ObservableCollection<VoidedPayment> Payments
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
                var payments = new List<VoidedPayment>();
                foreach (var v in vs)
                {
                    payments.Add((VoidedPayment) v);
                }
               Payments = new ObservableCollection<VoidedPayment>(payments);

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
            _payments = new ObservableCollection<VoidedPayment>();
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

        private VoidedPayment _selectedPayment;
        private IRptSvc _rptSvc;

        public VoidedPayment SelectedPayment
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