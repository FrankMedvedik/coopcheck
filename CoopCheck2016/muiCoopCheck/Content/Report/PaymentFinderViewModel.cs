using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

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
            }
        }


        #region DisplayState
        private StatusInfo _status; 

        public void ResetState()
        {

            PaymentFinderCriteria = new PaymentFinderCriteria();
            PaymentFinderCriteria.StartDate = DateTime.Today.Add(new TimeSpan(-365, 0, 0, 0));
            PaymentFinderCriteria.EndDate = DateTime.Today;
            ShowGridData = false;

        }
        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
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

        public  async void GetPayments()
        {
            var c =  new ObservableCollection<vwPayment>(await PaymentSvc.GetPayments(PaymentFinderCriteria));
            Payments = c;
        }
    }
}