using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    /// <summary>
    /// Interaction logic for BatchEditView.xaml
    /// </summary>
    public partial class BatchEditView : UserControl
    {
        private BatchEditViewModel _vm;
        public BatchEditView()
        {
            InitializeComponent();
            _vm = new BatchEditViewModel();
            DataContext = _vm;
        }

        public int BatchNum
        {
            get { return _vm.SelectedBatch.Num; }
            set { _vm.BatchNum = value; }
        }
        public void ResetState()
        {
            _vm.ResetState(); 
        }
        public bool IsDirty
        {
            get { return _vm.IsDirty; }
        }
        public ObservableCollection<VoucherImport> VoucherImports
        {
            get { return _vm.VoucherImports; }
            set { _vm.VoucherImports = value; }
        }
        public static readonly DependencyProperty VoucherImportsProperty =
        DependencyProperty.Register("VoucherImports", typeof(ObservableCollection<VoucherImport>), typeof(BatchEditView), new PropertyMetadata(new ObservableCollection<VoucherImport>()));

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _vm.Save();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedBatch == null) return;
            MessageBoxResult result = MessageBox.Show("Delete this entire batch of vouchers?", "Confirm", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                _vm.Delete(); ;
            }
        }

        private void DeleteVoucher_Click(object sender, RoutedEventArgs e)
        {
                // set the status of the callback to closed
                if (_vm.SelectedVoucher == null) return;
                MessageBoxResult result = MessageBox.Show("Delete this Voucher?", "Confirm", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    _vm.DeleteSelectedVoucher();
                }
            }

        private void AddVoucher_Click(object sender, RoutedEventArgs e)
        {
            _vm.AddVoucher();
        }
    }
}
