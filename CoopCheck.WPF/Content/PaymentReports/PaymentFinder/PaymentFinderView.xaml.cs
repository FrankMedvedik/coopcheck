using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.PaymentReports.PaymentFinder
{
    public partial class PaymentFinderView : UserControl
    {
        private readonly IPaymentFinderViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentFinderView(IPaymentFinderViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            DataContext = _vm;
            prcv.DetailedCriteria.Visibility = Visibility.Visible;
            prcv.CheckNumSP.Visibility = Visibility.Visible;
            prcv.ShowAllAccountsOption = true;
        }

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

        private async void ClearCheck_Click(object sender, RoutedEventArgs e)
        {
            _vm.SelectedPayment.cleared_date = DateTime.Today;
            _vm.SelectedPayment.cleared_amount = _vm.SelectedPayment.tran_amount;
            var cw = new ClearPaymentDialog {DataContext = _vm};
            var result = cw.ShowDialog();
            if (result == true)
            {
                try
                {
                    _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
                    await Task.Run(() =>
                    {
                        _vm.ClearCheck();
                        _vm.GetPayments();
                    });
                }
                catch (Exception ex)
                {
                    _vm.Status = new StatusInfo {ErrorMessage = ex.Message, StatusMessage = "clear payment failed"};
                }
            }
        }

        private void VoidCheck_Click(object sender, RoutedEventArgs e)
        {
            var cw = new VoidPaymentDialog {DataContext = _vm};
            var result = cw.ShowDialog();
            if (result == true)
            {
                try
                {
                    _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;

                    _vm.VoidCheck();
                }
                catch (Exception ex)
                {
                    _vm.Status = new StatusInfo {ErrorMessage = ex.Message, StatusMessage = "void payment failed"};
                }
            }
        }
    }
}