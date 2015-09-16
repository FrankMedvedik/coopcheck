using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

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
        public void ResetState()
        {
            _vm.ResetState(); 
        }
        public bool IsDirty
        {
            get { return _vm.IsDirty; }
        }
        public ObservableCollection<VoucherImport> VoucherImports
        {
            get { return _vm.VoucherImports; }
            set { _vm.VoucherImports = value; }
        }
        public static readonly DependencyProperty VoucherImportsProperty =
        DependencyProperty.Register("VoucherImports", typeof(ObservableCollection<VoucherImport>), typeof(BatchEditView), new PropertyMetadata(new ObservableCollection<VoucherImport>()));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.Save();
        }
    }
}
