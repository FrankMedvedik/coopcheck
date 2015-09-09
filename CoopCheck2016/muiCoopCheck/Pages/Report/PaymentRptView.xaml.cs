using System;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Pages.Report;

namespace CoopCheck.WPF.Pages.Rpt
{
    public partial class PaymentRptView : UserControl
    {
        private PaymentRptViewModel _vm = null;

        public PaymentRptView()
        {
            InitializeComponent();
            _vm = new PaymentRptViewModel();
            DataContext = _vm;
        }

        public void Refresh()
        {
            _vm.RefreshAll();
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //  PaymentsDG.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
        }

        public vwBatchRpt Batch
        {
            get { return _vm.Batch; }
            set
            {
                _vm.Batch = value;
            }
        }

        public static readonly DependencyProperty BatchProperty =
            DependencyProperty.Register("Batch", typeof(vwBatchRpt), typeof(PaymentRptView),
                new PropertyMetadata(new vwBatchRpt()));

        public ReportDateRange ReportDateRange
        {
            get { return _vm.ReportDateRange; }
            set
            {
                //SetValue(ReportDateRangeProperty, value);
                _vm.ReportDateRange = value;
            }
        }


        public static readonly DependencyProperty ReportDateRangeProperty =
            DependencyProperty.Register("ReportDateRange", typeof(ReportDateRange), typeof(PaymentRptView), new PropertyMetadata(new ReportDateRange()));

        private void PaymentDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (_vm.SelectedPayment != null)
            //{
            //    cv.RecruiterSIP = _vm.RecruiterSIP;
            //    cv.SelectedJobNum = _vm.SelectedJob.JobNumber;
            //    cv.ReportDateRange = _vm.ReportDateRange;
            //    cv.Refresh();
            //}
            //else
            //    cv.ShowData = false;
        }
        public bool ShowData
        {
            get { return _vm.ShowGridData; }
            set
            {
                _vm.ShowGridData = value;

            }
        }

        public static readonly DependencyProperty ShowDataProperty =
      DependencyProperty.Register("ShowData", typeof(bool), typeof(PaymentRptView), new PropertyMetadata(false));


    }

}
