using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Payment.Summary;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.Batch
{
    /// <summary>
    /// </summary>
    public partial class BatchReportView : UserControl
    {
        private BatchReportViewModel _vm = null;
   

        public BatchReportView()
        {
            InitializeComponent();
            _vm = new BatchReportViewModel();
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