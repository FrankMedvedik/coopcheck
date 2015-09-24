using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Payment
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
            prcv.DetailedCriteria.Visibility = Visibility.Visible;
        }

        private PaymentFinderViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.GetPayments(prcv.PaymentReportCriteria);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();

        }
    }
}