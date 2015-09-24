using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Report
{
    public class PaymentRptViewModel : ViewModelBase
    {
        public PaymentRptViewModel()
        {
            ResetState();
        }

         public void ResetState()
        {
            ShowGridData = false;

        }


        private bool _canRefresh = true;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
                GetPayments();
            }
        }

        #endregion

        private ObservableCollection<vwPayment> _payments = new ObservableCollection<vwPayment>();

        private StatusInfo _status;
        private bool _showGridData;
        private PaymentReportCriteria _paymentReportCriteria;

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
        public ObservableCollection<vwPayment> Payments
        {
            get
            {
                return _payments;
            }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }

        public void RefreshAll ()
        {
            if (CanRefresh)
            {
                GetPayments();
            }
        }

        public async void GetPayments()
        {
            ShowGridData = false;
            if ((PaymentReportCriteria != null))
            {
                try
                {
                   Payments = await Task<ObservableCollection<vwPayment>>.Factory.StartNew( () =>
                    {
                        var p = RptSvc.GetPayments(PaymentReportCriteria);
                        return new ObservableCollection<vwPayment>(p.Result);
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

        public bool ShowGridData    
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }
    }
}