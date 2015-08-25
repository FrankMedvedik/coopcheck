using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using muiCoopCheck.Models;
using muiCoopCheck.Services;

namespace muiCoopCheck.Pages.Payments
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaymentReportViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
        /// 
        public PaymentReportViewModel()
        {
            ResetState();
            StatusMsg = "";
            _paymentReportCriteria = new PaymentReportCriteria();
            _paymentReportCriteria.StartDate = DateTime.Today.Add(new TimeSpan(-30, 0, 0, 0));
            _paymentReportCriteria.EndDate = DateTime.Today;

        }

        private PaymentReportCriteria _paymentReportCriteria;

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value; 
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
        private bool _canProceed;
        private string _errorMsg;
        private string _statusMsg;

        private void ResetState()
        {
            StatusMsg = "";
            ErrorMsg = "";
            CanProceed = false;
            ShowGridData = false;
        }
        public string StatusMsg
        {
            get { return _statusMsg; }
            set
            {
                _statusMsg = value;
                NotifyPropertyChanged();
            }
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
                NotifyPropertyChanged();
            }

        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
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

        #region Checks

        private vwPayment _selectedCheck;
        public vwPayment SelectedCheck
        {
            get { return _selectedCheck; }
            set
            {
                _selectedCheck = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public  void GetPayments()
        {
            var c =  new ObservableCollection<vwPayment>(PaymentSvc.GetPayments(PaymentReportCriteria).ToList());
            Payments = c;
        }
    }
}