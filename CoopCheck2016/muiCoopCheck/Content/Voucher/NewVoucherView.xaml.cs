using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Pages.Voucher.Import;

namespace CoopCheck.WPF.Content.Voucher
{
    public partial class NewVoucherView : UserControl
    {
        private NewVoucherViewModel _vm;

        public NewVoucherView()
        {
            InitializeComponent();
            _vm = new NewVoucherViewModel();
            DataContext = _vm;
            
        }

        public List<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports.ToList(); }
            set { _vm.VoucherImports = new ObservableCollection<VoucherImport>(value); }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VouchersProperty =
            DependencyProperty.Register("Vouchers", typeof ( List<VoucherImport>), typeof (NewVoucherView),
                new PropertyMetadata(new List<VoucherImport>()));

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSelectedVoucher();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _vm.CreateNewVoucher();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _vm.DeleteSelectedVoucher();
        }


    }
}
