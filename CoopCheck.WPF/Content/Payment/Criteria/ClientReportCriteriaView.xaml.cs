using System;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public partial class ClientReportCriteriaView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public ClientReportCriteriaView()
        {
            InitializeComponent();
            _vm = new ClientReportCriteriaViewModel();
            DataContext = _vm;
        }
        private ClientReportCriteriaViewModel _vm;
        public ClientReportCriteria ClientReportCriteria
        {
            get { return _vm.ClientReportCriteria; }
            set
            {
                _vm.ClientReportCriteria = value;
            }

        }

        public void ResetState()
        {
            _vm.ResetState();
        }

        private void JobNumTxt_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_vm.ValidateJobNumber(JobNumTxt.Text))
                _vm.ClientReportCriteria.SelectedJobNum = JobNumTxt.Text;
        }
    }
}