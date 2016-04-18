using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public partial class AccountPaymentsView : UserControl
    {
        private readonly ReconcileBankViewModel _vm;

        public AccountPaymentsView()
        {
            InitializeComponent();
            _vm = new ReconcileBankViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this,
                message => { _vm.BankFile = message.Content; });
            prcv.StartDate.Visibility = Visibility.Collapsed;
            prcv.EndDate.Visibility = Visibility.Collapsed;
            //Recon.Visibility = Visibility.Collapsed;
        }


        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            btnPayments.IsEnabled = false;

            await Task.Factory.StartNew(() =>
            {
                _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
                _vm.GetPayments();
            });


            btnPayments.IsEnabled = true;
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            DgStats.SelectAllCells();
            DgStats.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DgStats);
            var result = (string) Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            DgStats.UnselectAllCells();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("{0}.Reconcile", Path.GetFileName(_vm.BankFile.FilePath));
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = new StreamWriter(saveFileDialog.FileName);
                file.WriteLine(result);
                file.Close();
            }
        }
    }
}