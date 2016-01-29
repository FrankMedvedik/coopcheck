using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment
{
    public partial class PaymentFinderView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentFinderView()
        {
            InitializeComponent();
            _vm = new PaymentFinderViewModel();
            DataContext = _vm;
            prcv.DetailedCriteria.Visibility = Visibility.Visible;
            prcv.CheckNumSP.Visibility = Visibility.Visible;
        }

        private PaymentFinderViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments(prcv.PaymentReportCriteria);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            prcv.ResetState();
            _vm.ResetState();
        }
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();

        }
        private void ClearCheck_Click(object sender, RoutedEventArgs e)
        {
            _vm.SelectedPayment.cleared_date = DateTime.Today;
            _vm.SelectedPayment.cleared_amount = _vm.SelectedPayment.tran_amount;
            var cw = new ClearPaymentDialog() { DataContext = _vm };
            var result = cw.ShowDialog();
            if (result == true)
            {
                _vm.ClearCheck();
                _vm.GetPayments(prcv.PaymentReportCriteria);
            }
        }

        private void VoidCheck_Click(object sender, RoutedEventArgs e)
        {
            var cw = new VoidPaymentDialog() {DataContext = _vm};
            var result = cw.ShowDialog();
            if (result == true)
            {
                _vm.VoidCheck();
                _vm.GetPayments(prcv.PaymentReportCriteria);
            }
        }

        private void ExportToExcel()
        {
            dgPayments.SelectAllCells();
            dgPayments.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgPayments);
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats.);
            dgPayments.UnselectAllCells();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("Payments{0}.csv", prcv.PaymentReportCriteria.ToFormattedString('.'));
            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName);
                //file.WriteLine(result.Replace(",", " " ));
                file.WriteLine(result);
                file.Close();
            }
        }
    }
}