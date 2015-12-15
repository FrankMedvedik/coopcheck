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

        private void Button_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            CleanVouchers();

        }
        private async void CleanVouchers()
        {
            _vm.Status = new StatusInfo()
            {
                StatusMessage = String.Format("running address validation."),
                IsBusy = true
            };

            try
            {
                await Task.Factory.StartNew(() =>
                {
                    _vm.DataCleanAddresses(dccv.Criteria);
                });
                _vm.CanDataClean = false;
              //  dcfbv.DataCleanEvents = _vm.ValidationResults;
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


        public List<VoucherImport> VoucherImports { 
            set {
                _vm.Vi = value;
                CleanVouchers();
            } 
        }

        public ObservableCollection<VoucherImportWrapper> ValidationResults { get { return _vm.VoucherImports; } }
       }
}