using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CoopCheck.WPF.Services;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment
{
    public partial class OpenPaymentReportView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public OpenPaymentReportView()
        {
            InitializeComponent();
            _vm = new OpenPaymentReportViewModel();
            DataContext = _vm;
            prcv.EndDate.Visibility = Visibility.Collapsed;

        }

        private OpenPaymentReportViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments(prcv.PaymentReportCriteria);
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
            String result = (string)Clipboard.GetData(DataFormats.Html);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgPayments.UnselectAllCells();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("OpenPayments{0}.csv", prcv.PaymentReportCriteria.ToFormattedString('.'));
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