using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Report
{
    public partial class PaymentReportCriteriaView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentReportCriteriaView()
        {
            InitializeComponent();
            _vm = new PaymentReportCriteriaViewModel();
            DataContext = _vm;
        }

        private PaymentReportCriteriaViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (asv.SelectedAccount != null)
                _vm.PaymentReportCriteria.AccountId = asv.SelectedAccount.account_id;
//            _vm.GetPayments();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();

        }
        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
            }
        }
    }
}