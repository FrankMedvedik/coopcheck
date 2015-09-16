using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        //public List<VoucherImport> Vouchers
        //{
        //    get { return (List<VoucherImport>)GetValue(VouchersProperty); }
        //    set { SetValue(VouchersProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty VouchersProperty =
        //    DependencyProperty.Register("VouchersProperty", typeof(List<VoucherImport>), typeof(MainWindow), new PropertyMetadata(new List<VoucherImport>()));

        
    }
}
