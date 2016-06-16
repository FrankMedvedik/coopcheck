using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    /// <summary>
    /// </summary>
    public partial class BatchReportView : UserControl
    {
        private readonly IBatchReportViewModel _vm;


        public BatchReportView(IBatchReportViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
                _vm.RefreshAll();
            }
        }
    }
}