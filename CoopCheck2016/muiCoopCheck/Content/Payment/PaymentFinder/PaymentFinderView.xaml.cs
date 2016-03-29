using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.PaymentFinder
{
    public partial class PaymentFinderView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentFinderView()
        {
            InitializeComponent();
            _vm = new PaymentFinderViewModel();
            DataContext = _vm;
            prcv.DetailedCriteria.Visibility = Visibility.Visible;
            prcv.CheckNumSP.Visibility = Visibility.Visible;
            prcv.ShowAllAccountsOption = true;
        }

        private PaymentFinderViewModel _vm;

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.GetPayments();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            prcv.ResetState();
            _vm.ResetState();
        }
    
        private void ClearCheck_Click(object sender, RoutedEventArgs e)
        {
            _vm.SelectedPayment.cleared_date = DateTime.Today;
            _vm.SelectedPayment.cleared_amount = _vm.SelectedPayment.tran_amount;
            var cw = new ClearPaymentDialog() { DataContext = _vm };
            var result = cw.ShowDialog();
            if (result == true)
            {
                _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
                _vm.ClearCheck();
                _vm.GetPayments();
            }
        }

        private async void VoidCheck_Click(object sender, RoutedEventArgs e)
        {
            var cw = new VoidPaymentDialog() {DataContext = _vm};
            var result = cw.ShowDialog();
            if (result == true)
            {
                _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
                _vm.VoidCheck();
            }
        }

        
    }
}