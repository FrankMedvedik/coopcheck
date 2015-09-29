using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Import
{
    public partial class VoucherListView : UserControl
    {
        private VoucherListViewModel _vm;

        public VoucherListView()
        {
            InitializeComponent();
            _vm = new VoucherListViewModel();
            DataContext = _vm;
            
        }

        public List<VoucherImport> VoucherImports
        {
            get { return _vm.VoucherImports.ToList(); }
            set { _vm.VoucherImports = new ObservableCollection<VoucherImport>(value); }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VoucherImportsProperty =
            DependencyProperty.Register("VoucherImports", typeof ( List<VoucherImport>), typeof (VoucherListView),
                new PropertyMetadata(new List<VoucherImport>()));

        public int SelectedVoucherId
        {
            get { return _vm.SelectedVoucherId; }

        }

        public static readonly DependencyProperty SelectedVoucherIdProperty =
        DependencyProperty.Register("SelectedVoucherId", typeof(int), typeof(VoucherListView), new PropertyMetadata(0));
        
        public int SelectedBatchNum
        {
            get { return _vm.SelectedBatch.Num; }
            set { _vm.BatchNum = value; }

        }

        public static readonly DependencyProperty SelectedBatchNumProperty =
        DependencyProperty.Register("SelectedBatchNum", typeof(int), typeof(VoucherListView), new PropertyMetadata(0));

        //private void dgVouchers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    vev.SelectedBatchNum = SelectedBatchNum;
        //    vev.SelectedVoucherId = SelectedVoucherId;
        //}
    }
}
