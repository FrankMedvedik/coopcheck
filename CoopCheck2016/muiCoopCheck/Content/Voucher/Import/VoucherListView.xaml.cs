﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Edit;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Import
{
    public partial class VoucherListView : UserControl
    {
        private VoucherListViewModel _vm;

        public VoucherListView()
        {
            _vm = new VoucherListViewModel();
            DataContext = _vm;
            InitializeComponent();

        }

        public List<VoucherImport> VoucherImports
        {
            get { return _vm.Vi; }
            set { _vm.Vi = value; }
        }

        public static readonly DependencyProperty VoucherImportsProperty =
            DependencyProperty.Register("VoucherImports", typeof ( List<VoucherImport>), typeof (VoucherListView),
                new PropertyMetadata(new List<VoucherImport>()));
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
            var cw = new VoucherImportAddDialog() { DataContext = _vm };
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.DataCleanAddresses();
        }
        //private void dgVouchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (_vm.SelectedVoucher != null)
        //    {
        //        ve.Visibility = Visibility.Visible;
        //    }
        //    else
        //        ve.Visibility = Visibility.Collapsed;
        //}
    }
}
