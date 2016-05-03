using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Content.Voucher.Edit
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _vm.ResetState();
        }

    
    }
}
