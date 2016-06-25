using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Interfaces;

namespace CoopCheck.WPF.Content.PaymentReports.Criteria
{
    public partial class ClientReportCriteriaView : UserControl
    {
        private readonly IClientReportCriteriaViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public ClientReportCriteriaView(IClientReportCriteriaViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
        }

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _vm.ClientReportCriteria; }
            set { _vm.ClientReportCriteria = value; }
        }

        public void ResetState()
        {
            _vm.ResetState();
        }

        private void JobNumTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_vm.ValidateJobNumber(JobNumTxt.Text))
                _vm.ClientReportCriteria.SelectedJobNum = JobNumTxt.Text;
        }
    }
}