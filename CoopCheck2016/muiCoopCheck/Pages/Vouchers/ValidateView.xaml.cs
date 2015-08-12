using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Vouchers
{
    public partial class ValidateView : UserControl
    {
        private ValidateViewModel _vm;

        public ValidateView()
        {
            InitializeComponent();
            _vm = new ValidateViewModel();
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
