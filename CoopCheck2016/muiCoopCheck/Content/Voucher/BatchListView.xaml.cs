using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher
{
    /// <summary>
    /// Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class BatchListView : UserControl
    {
        private BatchListViewModel _vm;

        public BatchListView()
        {
            InitializeComponent();
            _vm = new BatchListViewModel();
            DataContext = _vm;
        }

    }
}
