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
