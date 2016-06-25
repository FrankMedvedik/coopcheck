using System.Windows.Controls;
using CoopCheck.WPF.Content.Interfaces;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class PaymentHistoryView : UserControl
    {
        private readonly IPaymentHistoryViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentHistoryView(IPaymentHistoryViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }
    }
}