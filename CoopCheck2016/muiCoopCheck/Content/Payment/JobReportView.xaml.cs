using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    /// </summary>
    public partial class JobReportView : UserControl
    {
        private JobReportViewModel _vm = null;
        
        public JobReportView()
        {
            InitializeComponent();
            _vm = new JobReportViewModel();
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
                _vm.RefreshAll();
            }
        }

    }
}