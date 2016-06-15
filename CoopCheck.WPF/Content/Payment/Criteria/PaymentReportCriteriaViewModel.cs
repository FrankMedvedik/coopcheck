using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using CoopCheck.Reports.Services;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class PaymentReportCriteriaViewModel : ViewModelBase
    {
        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();

        /// <summary>
        ///     Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
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

        public SolidColorBrush AccentBrush
        {
            get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); }
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
            }
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

        #region DisplayState

        public async void ResetState()
        {
            PaymentReportCriteria = new PaymentReportCriteria();
            PaymentReportCriteria.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            PaymentReportCriteria.EndDate = DateTime.Today.AddDays(1);
            Accounts = new ObservableCollection<Models.BankAccount>(await BankAccountSvc.GetAccounts());
            PaymentReportCriteria.Account =
                (from l in Accounts where l.IsDefault.GetValueOrDefault(false) select l).First();
            ShowGridData = false;
            EnableMiscFields = true;
            EnableCheckNum = true;
        }

        public bool ShowAllAccountsOption
        {
            get { return _showAllAccountsOption; }
            set
            {
                _showAllAccountsOption = value;
                NotifyPropertyChanged();
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

        private bool _enableCheckNum;

        public bool EnableCheckNum
        {
            get { return _enableCheckNum; }
            set
            {
                _enableCheckNum = value;
                NotifyPropertyChanged();
            }
        }

        private bool _enableMiscFields;

        public bool EnableMiscFields
        {
            get { return _enableMiscFields; }
            set
            {
                _enableMiscFields = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<Models.BankAccount> _accounts;
        private bool _showAllAccountsOption;


        public ObservableCollection<Models.BankAccount> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                if (_accounts != null)
                    if (ShowAllAccountsOption)
                        /*&& !(Accounts.Any(a => a.account_id == BankAccountSvc.AllAccountsOption.account_id)))*/
                        _accounts.Add(BankAccountSvc.AllAccountsOption);
                NotifyPropertyChanged("");
            }
        }

        #endregion
    }
}