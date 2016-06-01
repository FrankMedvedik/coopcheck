using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    public partial class VoucherImportWizardView : UserControl
    {
        private readonly VoucherImportWizardViewModel _vm;

        public VoucherImportWizardView()
        {
            InitializeComponent();
            _vm = new VoucherImportWizardViewModel();
            DataContext = _vm;
        }
    }
}