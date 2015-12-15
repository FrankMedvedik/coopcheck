using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;


namespace CoopCheck.WPF.Content.Voucher.Save
{
    public partial class VoucherSaveView : UserControl
    {
        private void btnExportErrors_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<VoucherExcelExport, Vouchers> s = new ExportToExcel<VoucherExcelExport, Vouchers>();
            s.ExcelSourceWorkbook = _vm.ExcelVoucherFilePath;
            s.ExcelWorksheetName = _vm.ExcelVoucherWorksheetName.Substring(0,20) + " err " + String.Format("{0:dMyyyyHHmmss}", DateTime.Now);
            s.dataToPrint = VoucherExports;
            s.GenerateReport();
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

        public string ExcelVoucherWorksheetName
        {
            get { return _vm.ExcelVoucherWorksheetName; }
            set { _vm.ExcelVoucherWorksheetName = value; }
        }

        public List<VoucherImport> VoucherImports 
        {
            set { vcvs.VoucherImports = value; }
        }

        public List<VoucherExcelExport> VoucherExports
        {
            get
            {
                List<VoucherExcelExport> errors = new List<VoucherExcelExport>();
                foreach (var v in vcvs.ValidationResults.Where(x=>!x.AltAddress.OkComplete))
                {
                    errors.Add(VoucherImportWrapperConverter.ToVoucherExcelExport(v));
                }
                return errors;
            }
        }

    }


    internal class Vouchers : List<VoucherExcelExport> { }
    
}