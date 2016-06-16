using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Open
{
    public partial class OpenPaymentReportView : UserControl
    {
        private readonly IOpenPaymentReportViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public OpenPaymentReportView(IOpenPaymentReportViewModel openPaymentReportViewModel)
        {
            InitializeComponent();
            _vm = openPaymentReportViewModel;
            DataContext = _vm;
            prcv.StartDate.Visibility = Visibility.Collapsed;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments(prcv.PaymentReportCriteria);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            _vm.PrintReport(prcv.PaymentReportCriteria);
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            dgPayments.SelectAllCells();
            dgPayments.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgPayments);
            var result = (string) Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgPayments.UnselectAllCells();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("OpenPayments{0}.csv",
                prcv.PaymentReportCriteria.ToFormattedString('.'));
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