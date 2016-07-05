using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Edit;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class VoucherListView : UserControl
    {
        private readonly VoucherListViewModel _vm;

        public VoucherListView()
        {
            _vm = new VoucherListViewModel();
            DataContext = _vm;
            InitializeComponent();
            Loaded += (s, e) =>
            {
                // only at this point the control is ready
                Window.GetWindow(this) // get the parent window
                    .Closing += (s1, e1) => ClosingControl();
            };
        }

        private void ClosingControl()
        {
            var disposable = DataContext as IDisposable;
            if (!ReferenceEquals(null, disposable))
            {
                disposable.Dispose();
            }
        }
    
        private void DeleteVoucher_Click(object sender, RoutedEventArgs e)
        {
            // set the status of the callback to closed
            if (_vm.SelectedVoucher == null) return;
            var result = ModernDialog.ShowMessage("Delete this Voucher?", "Confirm", MessageBoxButton.OKCancel,
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));
            if (result == MessageBoxResult.OK)
            {
                _vm.DeleteSelectedVoucher();
            }
        }

        private void AddVoucher_Click(object sender, RoutedEventArgs e)
        {
            var cw = new VoucherImportAddDialog {DataContext = _vm};
            var result = cw.ShowDialog();

            if (result == true)
                _vm.AddNewVoucher();
            else
                _vm.CancelNewVoucher();
        }

        private void chkFilterRows_Checked(object sender, RoutedEventArgs e)
        {
            _vm.FilterRows = true;
        }

        private void chkFilterRows_Unchecked(object sender, RoutedEventArgs e)
        {
            _vm.FilterRows = false;
        }

        private void dgVouchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedVoucher != null)
            {
                ve.RefreshLookups();
                btnDelete.IsEnabled = true;
            }
            else
                btnDelete.IsEnabled = false;
        }
    }
}