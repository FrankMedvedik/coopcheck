using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Payment;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

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