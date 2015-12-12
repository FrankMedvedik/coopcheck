using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;


namespace CoopCheck.WPF.Content.Voucher.Save
{
    public partial class VoucherSaveView : UserControl
    {
        private void btnExportErrors_Click(object sender, RoutedEventArgs e)
        {
            //ExportToExcel<VoucherImport, Vouchers> s = new ExportToExcel<VoucherImport, Vouchers>();
            //s.dataToPrint = VoucherImports.ToList();
            //s.GenerateReport();
        }


        private VoucherSaveViewModel _vm;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
        //
                });

          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public VoucherSaveView()
        {
            InitializeComponent();
            _vm = new VoucherSaveViewModel();
            DataContext = _vm;
        }


        public string ExcelVoucherFilePath
        {
            get { return _vm.ExcelVoucherFilePath; }
            set { _vm.ExcelVoucherFilePath = value; }
        }

        //public static readonly DependencyProperty ExcelVoucherFilePathProperty =
        //    DependencyProperty.Register("ExcelVoucherFilePath", typeof(string), typeof(VoucherSaveView), new PropertyMetadata(string.Empty));

        public string ExcelVoucherWorksheetName
        {
            get { return _vm.ExcelVoucherWorksheetName; }
            set { _vm.ExcelVoucherWorksheetName = value; }
        }

        //public static readonly DependencyProperty ExcelVoucherWorksheetNameProperty =
        //    DependencyProperty.Register("ExcelVoucherWorksheetName", typeof(string), typeof(VoucherSaveView), new PropertyMetadata(string.Empty));
        public List<VoucherImport> VoucherImports 
        {
            set { vcvs.VoucherImports = value; }
        }

    }
}