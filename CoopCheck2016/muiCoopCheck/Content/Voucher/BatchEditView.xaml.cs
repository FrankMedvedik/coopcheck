using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
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

        public int BatchNum
        {
            get { return _vm.SelectedBatch.Num; }
            set { _vm.BatchNum = value; }
        }

        public static readonly DependencyProperty BatchNumProperty =
        DependencyProperty.Register("BatchNum", typeof(int), typeof(BatchEditView),new PropertyMetadata(0));
    }
}
