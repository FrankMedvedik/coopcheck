using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Utilities.Unpay
{
    /// <summary>
    /// Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class UnclearedBatchListView : UserControl
    {
        private UnclearedBatchListViewModel _vm;

        public UnclearedBatchListView()
        {
            InitializeComponent();
            _vm = new UnclearedBatchListViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.ResetState();
        }
    }
}
