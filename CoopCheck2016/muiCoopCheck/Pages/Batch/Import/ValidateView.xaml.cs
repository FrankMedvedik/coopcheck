using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using muiCoopCheck.Models;
using muiCoopCheck.Pages.Vouchers;

namespace muiCoopCheck.Pages.Batch.Import
{
    public partial class ValidateView : UserControl
    {
        private BatchEditViewModel _vm;

        public ValidateView()
        {
            InitializeComponent();
            _vm = new BatchEditViewModel();
            DataContext = _vm;
            
        }

        public List<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports.ToList(); }
            set { _vm.VoucherImports = new ObservableCollection<VoucherImport>(value); }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VouchersProperty =
            DependencyProperty.Register("Vouchers", typeof ( List<VoucherImport>), typeof (ValidateView),
                new PropertyMetadata(new List<VoucherImport>()));

        
    }
}
