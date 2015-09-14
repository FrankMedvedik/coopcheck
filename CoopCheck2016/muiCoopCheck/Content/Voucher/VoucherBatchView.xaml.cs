using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for PrintChecksPage.xaml
    /// </summary>
    public partial class VoucherBatchView : UserControl
    {
        private VoucherBatchViewModel _vm;
        public VoucherBatchView()
        {
            InitializeComponent();
            _vm = new VoucherBatchViewModel();
            DataContext = _vm;
        }


        public List<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports.ToList(); }
            set { _vm.VoucherImports = new ObservableCollection<VoucherImport>(value); }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VouchersProperty =
            DependencyProperty.Register("Vouchers", typeof(List<VoucherImport>), typeof(VoucherBatchView),
                new PropertyMetadata(new List<VoucherImport>()));

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            _vm.Load();
        }
    }
}
