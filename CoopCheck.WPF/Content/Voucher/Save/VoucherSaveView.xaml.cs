using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Save
{
    public partial class VoucherSaveView : UserControl
    {
        private readonly VoucherSaveViewModel _vm;

        public VoucherSaveView()
        {
            InitializeComponent();
            _vm = new VoucherSaveViewModel();
            DataContext = _vm;
        }
    }
}