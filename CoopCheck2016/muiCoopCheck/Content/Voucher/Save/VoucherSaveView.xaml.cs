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
            s.ExcelSourceWorkbook = _vm.ExcelFileInfo.ExcelFilePath;
            s.ExcelWorksheetName = _vm.VoucherImportWrappers.Max(x=>x.JobNumber) + " errors " + String.Format("{0:dMyyyyHHmmss}", DateTime.Now); ;
            //s.ExcelWorksheetName = _vm.ExcelVoucherWorksheetName.Substring(0,20) + " err " + String.Format("{0:dMyyyyHHmmss}", DateTime.Now);
            s.dataToPrint = _vm.VoucherExports.Where(x=>!x.OkComplete).ToList();
            s.GenerateReport();
        }


        private VoucherSaveViewModel _vm;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.CreateBatchEditAndImportVouchers();
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

    }

    public class Vouchers : List<VoucherExcelExport> { }
    
}