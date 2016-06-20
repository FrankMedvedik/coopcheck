using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.PaymentReports.PaymentFinder
{
    public partial class VoidPaymentDialog : ModernDialog
    {
        public VoidPaymentDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            Buttons = new[] {OkButton, CancelButton};
        }
    }
}