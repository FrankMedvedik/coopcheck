using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    public partial class JobReportCriteriaView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public JobReportCriteriaView()
        {
            InitializeComponent();
            _vm = new JobReportCriteriaViewModel();
            DataContext = _vm;
        }
        private JobReportCriteriaViewModel _vm;
        public JobReportCriteria JobReportCriteria
        {
            get { return _vm.JobReportCriteria; }
            set
            {
                _vm.JobReportCriteria = value;
            }

        }

        public void ResetState()
        {
            _vm.ResetState();
        }

 
    }
}