using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public partial class ChangeBatchJobDialog : ModernDialog
    {
        public ChangeBatchJobDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton, this.CancelButton };
        }
    }
}
