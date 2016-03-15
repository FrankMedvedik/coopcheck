﻿using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Edit;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Utilities.Unpay
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
        //public ObservableCollection<VoucherImport> VoucherImports
        //{
        //    get { return _vm.VoucherImports; }
        //    set { _vm.VoucherImports = value; }
        //}
        //public static readonly DependencyProperty VoucherImportsProperty =
        //DependencyProperty.Register("VoucherImports", typeof(ObservableCollection<VoucherImport>), typeof(BatchEditView), new PropertyMetadata(new ObservableCollection<VoucherImport>()));

        private void SaveSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSelectedBatch();
        }

        private void SaveNewVoucher_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveNewVoucher();
        }


        private void CancelVoucher_Click(object sender, RoutedEventArgs e)
        {
            _vm.CancelNewVoucher();
        }

        private void DeleteSelectedBatch_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedBatch == null) return;
            MessageBoxResult result = ModernDialog.ShowMessage("Delete this entire batch of vouchers?", "Confirm", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                _vm.DeleteSelectedBatch(); ;
            }
        }

        private void DeleteVoucher_Click(object sender, RoutedEventArgs e)
        {
                // set the status of the callback to closed
                if (_vm.SelectedVoucher == null) return;
                MessageBoxResult result = ModernDialog.ShowMessage("Delete this Voucher?", "Confirm", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    _vm.DeleteSelectedVoucher();
                }
            }

        private void AddVoucher_Click(object sender, RoutedEventArgs e)
        {
            _vm.CreateNewVoucher();
            var cw = new VoucherImportAddDialog() {DataContext = _vm};
            var result = cw.ShowDialog();

            if (result == true)
            {
                _vm.SaveNewVoucher();
            }
            else
                _vm.CancelNewVoucher();
        }

    }
    }
