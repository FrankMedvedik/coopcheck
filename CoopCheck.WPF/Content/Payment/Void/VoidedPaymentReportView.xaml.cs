using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.Void
{
    public partial class VoidedPaymentReportView : UserControl
    {
        private readonly VoidedPaymentReportViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public VoidedPaymentReportView()
        {
            InitializeComponent();
            _vm = new VoidedPaymentReportViewModel();
            DataContext = _vm;
            prcv.DetailedCriteria.Visibility = Visibility.Visible;
            prcv.CheckNumSP.Visibility = Visibility.Visible;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments(prcv.PaymentReportCriteria);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();
            prcv.ResetState();
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
            //String result = (string)Clipboard.GetData(DataFormats.);
            dgPayments.UnselectAllCells();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("VoidedPayments{0}.csv",
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