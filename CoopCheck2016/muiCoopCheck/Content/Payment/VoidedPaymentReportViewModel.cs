﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    public class VoidedPaymentReportViewModel : ViewModelBase
    {
        public VoidedPaymentReportViewModel()
        {
            ResetState();

        }


        private ObservableCollection<vwVoidedPayment> _payments = new ObservableCollection<vwVoidedPayment>();
        public ObservableCollection<vwVoidedPayment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                string em = null;

                if (Payments.Count == PaymentSvc.MAX_PAYMENT_COUNT)
                    em = String.Format("Showing only the first {0} payments add additional criteria to limit your search", PaymentSvc.MAX_PAYMENT_COUNT);
                Status = new StatusInfo()
                {
                    ErrorMessage = em,
                    StatusMessage = String.Format("{0} Payments found", Payments.Count)
                };
                ShowGridData = true;
            }
        }


        #region DisplayState
        private StatusInfo _status;

        public void ResetState()
        {

            _payments = new ObservableCollection<vwVoidedPayment>();
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

        private Boolean _showGridData;

        public Boolean ShowGridData
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

        private vwVoidedPayment _selectedPayment;
        public vwVoidedPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public async void GetPayments(PaymentReportCriteria p)
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };
            try
            {
                Payments = await Task<ObservableCollection<vwVoidedPayment>>.Factory.StartNew(() =>
                {
                    var task = RptSvc.GetVoidedPayments(p);
                    return new ObservableCollection<vwVoidedPayment>(task.Result);
                });

            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading payments",
                    ErrorMessage = e.Message
                };

            }
        }
    }
}