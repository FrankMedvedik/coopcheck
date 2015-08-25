using System.Windows;
using System.Windows.Controls;
using muiCoopCheck.Pages.Checks;

namespace muiCoopCheck.Pages.Payments
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
            _vm = new PaymentReportViewModel();
            DataContext = _vm;
        }

        private PaymentReportViewModel _vm;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments();
        }
    }
}