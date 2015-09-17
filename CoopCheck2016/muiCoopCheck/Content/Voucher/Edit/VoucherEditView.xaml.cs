using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public partial class VoucherEditView : UserControl
    {
        private VoucherEditViewModel _vm;

        public VoucherEditView()
        {
            InitializeComponent();
            _vm = new VoucherEditViewModel();
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
        public int SelectedVoucherId
        {
            get { return _vm.SelectedVoucherId; }
            set { _vm.SelectedVoucherId = value; }

        }

        public static readonly DependencyProperty SelectedVoucherIdProperty =
        DependencyProperty.Register("SelectedVoucherId", typeof(int), typeof(VoucherEditView), new PropertyMetadata(0));



        public int SelectedBatchNum
        {
            get { return _vm.SelectedBatch.Num; }
            set { _vm.BatchNum = value; }

        }

        public static readonly DependencyProperty SelectedBatchNumProperty =
        DependencyProperty.Register("SelectedBatchNum", typeof(int), typeof(VoucherEditView), new PropertyMetadata(0));

    }
}
