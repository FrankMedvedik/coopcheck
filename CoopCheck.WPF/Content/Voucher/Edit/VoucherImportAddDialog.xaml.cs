using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    /// <summary>
    ///     Interaction logic for VoucherImportAddDialog.xaml
    /// </summary>
    public partial class VoucherImportAddDialog : ModernDialog
    {
        public VoucherImportAddDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            Buttons = new[] {OkButton, CancelButton};
        }
    }
}