using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaymentReportCriteriaViewModel : ViewModelBase
    {
        public SolidColorBrush AccentBrush { get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); } }
        /// <summary>
        /// Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
        /// 
        public PaymentReportCriteriaViewModel()
        {
            ResetState();
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.PaymentReportCriteriaCheckNumberChanged)
                {
                    SetCheckNumVisibility();
                }
            });
            
        }

        private void SetCheckNumVisibility()
        {
            if (string.IsNullOrEmpty(PaymentReportCriteria.CheckNumber))
            {
                EnableCheckNum = true;
                EnableMiscFields = true;
            }
            else
            {
                EnableCheckNum = true;
                EnableMiscFields = false;
            }
        }

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();

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
            PaymentReportCriteria.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            PaymentReportCriteria.EndDate = DateTime.Today.AddDays(1);
            Accounts = new ObservableCollection<bank_account>(await BankAccountSvc.GetAccounts());
            PaymentReportCriteria.Account = (from l in Accounts where l.IsDefault.GetValueOrDefault(false) == true select l).First();
            ShowGridData = false;
            EnableMiscFields = true;
            EnableCheckNum = true;
        }

        public bool ShowAllAccountsOption
        {
            get { return _showAllAccountsOption; }
            set { _showAllAccountsOption = value;
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

        private Boolean _enableCheckNum;

        public Boolean EnableCheckNum
        {
            get { return _enableCheckNum; }
            set
            {
                _enableCheckNum = value;
                NotifyPropertyChanged();
            }
        }
        private bool _enableMiscFields;
        public Boolean EnableMiscFields
        {
            get { return _enableMiscFields; }
            set
            {
                _enableMiscFields = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<bank_account> _accounts;
        private bool _showAllAccountsOption = false;


        public ObservableCollection<bank_account> Accounts
        {
            get
            {
                return _accounts;
            }
            set
            {
                _accounts = value;
                if (_accounts != null) 
                    if (ShowAllAccountsOption) /*&& !(Accounts.Any(a => a.account_id == BankAccountSvc.AllAccountsOption.account_id)))*/
                        _accounts.Add(BankAccountSvc.AllAccountsOption);
                NotifyPropertyChanged("");

            }
        }


        #endregion
    }
}