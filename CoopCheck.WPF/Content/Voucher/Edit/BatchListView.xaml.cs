using System.Windows.Controls;
using CoopCheck.WPF.Content.Interfaces;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    /// <summary>
    ///     Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class BatchListView : UserControl
    {
        private readonly IBatchListViewModel _vm;

        public BatchListView(IBatchListViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}