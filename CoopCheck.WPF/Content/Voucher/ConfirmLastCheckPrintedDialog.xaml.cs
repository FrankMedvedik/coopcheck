using System.Linq;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    ///     Interaction logic for VoucherImportAddDialog.xaml
    /// </summary>
    public partial class ConfirmLastCheckPrintedDialog : ModernDialog
    {
        public ConfirmLastCheckPrintedDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            Buttons = new[] {OkButton, CancelButton};
            Buttons.First().IsDefault = false;
        }
    }
}