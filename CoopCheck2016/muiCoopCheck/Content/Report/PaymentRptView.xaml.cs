using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Report
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
        
        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
            }
        }

        public static readonly DependencyProperty GlobalReportCriteriaProperty =
            DependencyProperty.Register("PaymentReportCriteria", typeof(PaymentReportCriteria), typeof(PaymentRptView), new PropertyMetadata(new PaymentReportCriteria()));

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

        public ObservableCollection<vwPayment> Payments
        {
            get { return _vm.Payments; }
            set
            {
                _vm.Payments = value;
            }
        }

        public static readonly DependencyProperty PaymentsProperty =
            DependencyProperty.Register("Payments", typeof(ObservableCollection<vwPayment>), typeof(PaymentRptView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

    }

}
