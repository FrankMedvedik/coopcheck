using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for VoucherImportAddDialog.xaml
    /// </summary>
    public partial class ConfirmLastCheckPrintedDialog : ModernDialog
    {
        public ConfirmLastCheckPrintedDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            this.Buttons = new Button[] { this.OkButton};
        }
    }
}
