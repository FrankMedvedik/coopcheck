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
using GalaSoft.MvvmLight.Messaging;
using muiCoopCheck.Models;

namespace muiCoopCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<List<VoucherImport>>(this, message =>
            {

            });
        }


        public List<VoucherImport> Vouchers
        {
            get { return (List<VoucherImport>)GetValue(VouchersProperty); }
            set { SetValue(VouchersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VouchersProperty =
            DependencyProperty.Register("VouchersProperty", typeof(List<VoucherImport>), typeof(MainWindow), new PropertyMetadata(new List<VoucherImport>()));

        
    }
}
