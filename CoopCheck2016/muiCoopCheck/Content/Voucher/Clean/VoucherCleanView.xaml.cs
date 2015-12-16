 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class VoucherCleanView : UserControl
    {
        private VoucherCleanViewModel _vm;

        private async void Button_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            
            _vm.Status = new StatusInfo()
            {
                StatusMessage = String.Format("running address validation."),
                IsBusy = true
            };
            await CleanVouchers();
        }
        private async Task CleanVouchers()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.CleanTheVouchers();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public VoucherCleanView()
        {
            InitializeComponent();
            _vm = new VoucherCleanViewModel();
            DataContext = _vm;
        }

        //public List<VoucherImport> VoucherImports { set { _vm.Vi = value; } }

        //public ObservableCollection<VoucherImportWrapper> ValidationResults { get { return _vm.VoucherImports; } }

       }
}