using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    public partial class VoucherView : UserControl
    {
        private VoucherViewModel _vm;
        public VoucherView()
        {
            InitializeComponent();
            _vm = new VoucherViewModel();
            DataContext = _vm;
        }
     
        private void PageChanged(object sender, RoutedEventArgs e)
        {
            //if (VoucherWizard.CurrentPage.Name == "DataClean")
            //{
            //    vcv.VoucherImports = iwv.Vouchers.ToList();
            //    vsv.ExcelVoucherFilePath = iwv.ExcelVoucherFilePath;
            //    vsv.ExcelVoucherWorksheetName = iwv.ExcelVoucherWorksheetName;
            //}
            //if (VoucherWizard.CurrentPage.Name == "ListVouchers")
            //    vlv.ValidationResults = vcv.ValidationResults;
            //if (VoucherWizard.CurrentPage.Name == "SaveVouchers")
            //{
            //    vsv.VoucherImports = vlv.ValidationResults.Select(r => r.Model).ToList();
            //}

        }
    }
}
