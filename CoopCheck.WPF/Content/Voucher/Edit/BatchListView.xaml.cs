using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    /// <summary>
    ///     Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class BatchListView : UserControl
    {
        private readonly BatchListViewModel _vm;

        public BatchListView()
        {
            InitializeComponent();
            _vm = new BatchListViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();
        }
    }
}