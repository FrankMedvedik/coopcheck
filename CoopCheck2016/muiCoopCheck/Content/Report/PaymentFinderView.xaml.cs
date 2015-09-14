using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// Description for PaymentReportCriteriaView.
    /// </summary>
    public partial class PaymentReportView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentReportView()
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