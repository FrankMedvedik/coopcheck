using System.IO;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment
{
    public partial class PositivePayReportView : UserControl
    {
        private PositivePayReportViewModel _vm = null;
        
        public PositivePayReportView()
        {
            InitializeComponent();
            _vm = new PositivePayReportViewModel();
            DataContext = _vm;
        }
 

      private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.PositivePays.Count == 0) return;
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.FileName = string.Format("PositivePay.{0}.txt", _vm.PaymentReportCriteria.ToFormattedString('.'));
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