using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public partial class AccountPaymentsView : UserControl
    {
        private ReconcileBankViewModel _vm;

        public AccountPaymentsView()
        {
            InitializeComponent();
            _vm = new ReconcileBankViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                _vm.BankFile = message.Content;
            });
            prcv.StartDate.Visibility = Visibility.Collapsed;
            prcv.EndDate.Visibility = Visibility.Collapsed;
            //Recon.Visibility = Visibility.Collapsed;
        }
        

        private async  void Refresh_Click(object sender, RoutedEventArgs e)
        {
            btnPayments.IsEnabled = false;

            await Task.Factory.StartNew(() =>
            {
                _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
                _vm.GetPayments();
            });


            btnPayments.IsEnabled = true;
        }
 
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This will clear the payments");

        }
    }

}
