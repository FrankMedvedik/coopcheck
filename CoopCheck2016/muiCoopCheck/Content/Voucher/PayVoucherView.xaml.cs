using System;
using System.Deployment.Application;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class PayVoucherView : UserControl
    {
        private PayVouchersViewModel _vm;
        public PayVoucherView()
        {
            InitializeComponent();
            _vm = new PayVouchersViewModel();
            DataContext = _vm;
        }

        private void SwiftPay(object sender, RoutedEventArgs e)
        {
            _vm.SwiftPay();

        }

        private void PrintChecks(object sender, RoutedEventArgs e)
        {
            var f = ApplicationDeployment.CurrentDeployment.DataDirectory + @"\" +
                    @Properties.Settings.Default.CheckTemplate;
                if (!File.Exists(f))
                {
                    ModernDialog.ShowMessage( f + " not found, cannot print checks. ",
                        "check template missing", MessageBoxButton.OK);
                    return;
                }
                   _vm.PrintChecks();
        }
    }
 
}
