using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using CoopCheck.Reports.Contracts.Interfaces;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Criteria
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class PaymentReportCriteriaViewModel : ViewModelBase, IPaymentReportCriteriaViewModel
    {
        private readonly IBankAccountSvc _bankAccountSvc;
        private IPaymentReportCriteria _paymentReportCriteria;

        /// <summary>
        ///     Initializes a new instance of the CheckReportViewModel class.
        /// </summary>
        public PaymentReportCriteriaViewModel(IBankAccountSvc bankAccountSvc,
            IPaymentReportCriteria paymentReportCriteria)
        {
            _bankAccountSvc = bankAccountSvc;
            _paymentReportCriteria = paymentReportCriteria;

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
            get { return (PaymentReportCriteria) _paymentReportCriteria; }
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
            var accounts = await _bankAccountSvc.GetAccounts();
            var ocAccounts = new ObservableCollection<Models.BankAccount>();
            foreach (var a in accounts)
            {
                ocAccounts.Add((Models.BankAccount) a);
            }
            Accounts = ocAccounts;

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
                        _accounts.Add((Models.BankAccount) _bankAccountSvc.AllAccountsOption);
                NotifyPropertyChanged("");
            }
        }

        #endregion
    }
}