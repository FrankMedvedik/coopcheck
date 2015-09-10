using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
 

namespace CoopCheck.WPF.Pages.Voucher.Import
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
        public List<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports.ToList(); }
            set
            {
                _vm.VoucherImports = new ObservableCollection<VoucherImport>(value);
            }
        }

        public static readonly DependencyProperty VouchersProperty =
    DependencyProperty.Register("Vouchers", typeof(List<VoucherImport>), typeof(BatchEditView),
        new PropertyMetadata(new List<VoucherImport>()));

        public BatchEdit SelectedBatch
        {
            get { return _vm.SelectedBatch; }
            set
            {
                _vm.SelectedBatch = value;
            }
        }

        public static readonly DependencyProperty SelectedBatchProperty =
    DependencyProperty.Register("SelectedBatch", typeof(BatchEdit), typeof(BatchEditView),
        new PropertyMetadata(BatchEdit.NewBatchEdit()));


    }
}
