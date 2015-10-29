using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public partial class BankClearTransactionsView : UserControl
    {
        private BankClearTransactionViewModel _vm;

        public BankClearTransactionsView()
        {
            InitializeComponent();
            _vm = new BankClearTransactionViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                _vm.BankClearTransactions = message.Content.BankClearTransactions;

            });
        }

        public List<BankClearTransaction> BankClearTransactions
        {
            get { return _vm.BankClearTransactions.ToList(); }
            set { _vm.BankClearTransactions = new ObservableCollection<BankClearTransaction>(value); }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BankClearTransactionsProperty =
            DependencyProperty.Register("BankClearTransactions", typeof ( List<BankClearTransaction>), typeof (BankClearTransactionsView),
                new PropertyMetadata(new List<BankClearTransaction>()));
        
        private void DeleteBankClearTransaction_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddBankClearTransaction_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
