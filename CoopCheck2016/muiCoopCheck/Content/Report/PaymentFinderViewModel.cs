using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaymentFinderViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
        /// 
        public PaymentFinderViewModel()
        {
            ResetState();

        }

        private PaymentFinderCriteria _paymentFinderCriteria;

        public PaymentFinderCriteria PaymentFinderCriteria
        {
            get { return _paymentFinderCriteria; }
            set
            {
                _paymentFinderCriteria = value;
                NotifyPropertyChanged();

            }
        }


        private ObservableCollection<vwPayment> _payments = new ObservableCollection<vwPayment>();
        public ObservableCollection<vwPayment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                string em = null;
                if (Payments.Count == PaymentSvc.MAX_PAYMENT_COUNT)
                        em = String.Format("Showing only the first {0} payments add additional criteria to limit your search",
                            PaymentSvc.MAX_PAYMENT_COUNT);
                 Status = new StatusInfo()
                {
                    ErrorMessage = em,
                    StatusMessage = String.Format("{0} Payments found", Payments.Count)
                };
            }
        }


        #region DisplayState
        private StatusInfo _status;

        public void ResetState()
        {

            PaymentFinderCriteria = new PaymentFinderCriteria();
            PaymentFinderCriteria.StartDate = DateTime.Today.Add(new TimeSpan(-30, 0, 0, 0));
            PaymentFinderCriteria.EndDate = DateTime.Today;
            Payments = new ObservableCollection<vwPayment>();
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

        private vwPayment _selectedPayment;
        public vwPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public async void GetPayments()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };
            try
            {
                var c = new ObservableCollection<vwPayment>(await RptSvc.GetPayments(PaymentFinderCriteria));
                Payments = c;

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