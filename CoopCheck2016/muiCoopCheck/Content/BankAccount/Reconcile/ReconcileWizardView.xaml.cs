using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public partial class ReconcileWizardView : UserControl
    {
        private ReconcileWizardViewModel _vm;
        public ReconcileWizardView()
        {
            InitializeComponent();
            _vm = new ReconcileWizardViewModel();
            DataContext = _vm;
        }
     
        private void PageChanged(object sender, RoutedEventArgs e)
        {
            //if (ReconcileWizard.CurrentPage.Name == "DataClean")
            //{
            //    vcv.VoucherImports = Enumerable.ToList<VoucherImport>(iwv.Vouchers);
            //    vsv.ExcelVoucherFilePath = iwv.ExcelVoucherFilePath;
            //    vsv.ExcelVoucherWorksheetName = iwv.ExcelVoucherWorksheetName;
            //}
            //if (VoucherWizard.CurrentPage.Name == "ListVouchers")
            //    vlv.ValidationResults = vcv.ValidationResults;
            //if (VoucherWizard.CurrentPage.Name == "SaveVouchers")
            //{
            //    vsv.VoucherImports = Enumerable.Select<VoucherImportWrapper, VoucherImport>(vlv.ValidationResults, r => r.Model).ToList();
            //}

        }
    }
}
