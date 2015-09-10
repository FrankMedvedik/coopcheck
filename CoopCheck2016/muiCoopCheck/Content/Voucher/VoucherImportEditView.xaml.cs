using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Pages.Voucher.Import;

namespace CoopCheck.WPF.Content.Voucher
{
    public partial class VoucherImportEditView : UserControl
    {
        private VoucherImportEditViewModel _vm;

        public VoucherImportEditView()
        {
            InitializeComponent();
            _vm = new VoucherImportEditViewModel();
            DataContext = _vm;
            
        }

      private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _vm.Save();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _vm.New();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _vm.Delete();
        }

    }
}
