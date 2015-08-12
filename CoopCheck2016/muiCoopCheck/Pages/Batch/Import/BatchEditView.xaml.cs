using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using muiCoopCheck.Models;
using muiCoopCheck.Pages.Vouchers;

namespace muiCoopCheck.Pages.Batch.Import
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


    }
}
