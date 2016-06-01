using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class PaymentHistoryView : UserControl
    {
        private readonly PaymentHistoryViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentHistoryView()
        {
            InitializeComponent();
            _vm = new PaymentHistoryViewModel();
            DataContext = _vm;
        }
    }
}