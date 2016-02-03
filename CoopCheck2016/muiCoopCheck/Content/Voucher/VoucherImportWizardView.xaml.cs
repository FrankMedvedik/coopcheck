using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    public partial class VoucherImportWizardView : UserControl
    {
        private VoucherImportWizardViewModel _vm;
        public VoucherImportWizardView()
        {
            InitializeComponent();
            _vm = new VoucherImportWizardViewModel();
            DataContext = _vm;
        }
     
    }
}
