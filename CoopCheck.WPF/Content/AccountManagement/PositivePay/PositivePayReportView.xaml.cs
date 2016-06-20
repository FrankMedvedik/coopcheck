using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.AccountManagement.PositivePay
{
    public partial class PositivePayReportView : UserControl
    {
        private readonly IPositivePayReportViewModel _vm;

        public PositivePayReportView(IPositivePayReportViewModel positivePayReportViewModel)
        {
            InitializeComponent();
            _vm = positivePayReportViewModel;
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today;
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
        }


        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.PositivePays.Count == 0) return;
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("PositivePay{0}.txt",
                _vm.PaymentReportCriteria.ToFormattedString('.'));
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, _vm.PositivePayData);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.RefreshAll();
        }
    }
}