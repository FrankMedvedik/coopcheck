using System.Windows.Controls;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.PaymentReports.Batch
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