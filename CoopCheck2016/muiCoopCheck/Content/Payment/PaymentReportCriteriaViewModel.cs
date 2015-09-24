using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaymentReportCriteriaViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
        /// 
        public PaymentReportCriteriaViewModel()
        {
            ResetState();

        }

        private PaymentReportCriteria _paymentReportCriteria;

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria= value;
                NotifyPropertyChanged();

            }
        }

        #region DisplayState

        public async void ResetState()
        {

            PaymentReportCriteria = new PaymentReportCriteria();
            PaymentReportCriteria.StartDate = DateTime.Today.Add(new TimeSpan(-30, 0, 0, 0));
            PaymentReportCriteria.EndDate = DateTime.Today;
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
            PaymentReportCriteria.Account = (from l in Accounts where l.IsDefault.GetValueOrDefault(false) == true select l).First();
            ShowGridData = false;
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
        
        private ObservableCollection<bank_account> _accounts;
        public ObservableCollection<bank_account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;

                NotifyPropertyChanged("");
            }
        }


        #endregion
    }
}