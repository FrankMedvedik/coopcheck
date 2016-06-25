using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.Domain.Contracts.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public partial class BankTransactionsView : UserControl
    {
        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BankClearTransactionsProperty =
            DependencyProperty.Register("BankClearTransactions", typeof(List<BankClearTransaction>),
                typeof(BankTransactionsView),
                new PropertyMetadata(new List<BankClearTransaction>()));

        private readonly BankTransactionViewModel _vm;

        public BankTransactionsView()
        {
            InitializeComponent();
            _vm = new BankTransactionViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this,
                message => { _vm.BankClearTransactions = message.Content.UnmatchedBankClearTransactions; });
        }

        public List<BankClearTransaction> BankClearTransactions
        {
            get { return _vm.BankClearTransactions.ToList(); }
            set { _vm.BankClearTransactions = new ObservableCollection<BankClearTransaction>(value); }
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            dgBankTransaction.SelectAllCells();
            dgBankTransaction.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgBankTransaction);
            var result = (string) Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats.);
            dgBankTransaction.UnselectAllCells();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "BankTransactions.NONAME.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = new StreamWriter(saveFileDialog.FileName);
                //file.WriteLine(result.Replace(",", " " ));
                file.WriteLine(result);
                file.Close();
            }
        }
    }
}