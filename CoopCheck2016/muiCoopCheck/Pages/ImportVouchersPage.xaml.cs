using System.Windows.Controls;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.Content.Voucher;
using GalaSoft.MvvmLight.Messaging;
namespace CoopCheck.WPF.Pages
{
    public partial class ImportVouchersPage : UserControl
    {
        private VouchersWindow _vwin = new VouchersWindow();
        public ImportVouchersPage()
        {
            InitializeComponent();
            DataContext = this;
            if(UserAuth.Instance.CanRead)
            {
                _vwin.ShowDialog();
                //Messenger.Default.Send(new NavigationMessage()
                //{
                //    Page = "/Pages/Home.xaml"
                //});
                //iv.Visibility = System.Windows.Visibility.Visible;
                //nov.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                //iv.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }
            
        }

    }
}
