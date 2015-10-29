using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public partial class ReconcileBankView : UserControl
    {
        private ReconcileBankViewModel _vm;

        public ReconcileBankView()
        {
            InitializeComponent();
            _vm = new ReconcileBankViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                _vm.BankFileViewModel = message.Content;
            });
            prcv.StartDate.Visibility = Visibility.Collapsed;
            prcv.EndDate.Visibility = Visibility.Collapsed;
        }
        

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            Recon.IsExpanded = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
