using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.PaymentReports.Criteria
{
    public partial class PaymentReportCriteriaView : UserControl
    {
        public static readonly DependencyProperty ShowAllAccountsOptionProperty =
            DependencyProperty.Register("ShowAllAccountsOption", typeof(bool), typeof(PaymentReportCriteriaView),
                new PropertyMetadata(false));

        private readonly IPaymentReportCriteriaViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentReportCriteriaView(IPaymentReportCriteriaViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _vm.PaymentReportCriteria; }
            set { _vm.PaymentReportCriteria = value; }
        }

        public bool ShowAllAccountsOption
        {
            get { return _vm.ShowAllAccountsOption; }
            set { _vm.ShowAllAccountsOption = value; }
        }

        public void ResetState()
        {
            _vm.ResetState();
        }

        private void CheckNumTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _vm.EnableCheckNum = true;
            _vm.EnableMiscFields = false;
        }
    }
}