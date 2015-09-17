using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Report
{
    public partial class PaymentFinderView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentFinderView()
        {
            InitializeComponent();
            _vm = new PaymentFinderViewModel();
            DataContext = _vm;
        }

        private PaymentFinderViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();

        }
    }
}