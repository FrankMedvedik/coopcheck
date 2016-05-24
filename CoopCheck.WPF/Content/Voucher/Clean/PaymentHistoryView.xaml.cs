using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Edit;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class PaymentHistoryView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentHistoryView()
        {
            InitializeComponent();
            _vm = new PaymentHistoryViewModel();
            DataContext = _vm;
        }

        private PaymentHistoryViewModel _vm;

        
    }
}