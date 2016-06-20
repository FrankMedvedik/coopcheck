using System.Windows.Controls;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.WPF.Content;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for EditBatchPage.xaml
    /// </summary>
    public partial class OpenBatchPage : UserControl, IContent
    {
        public OpenBatchPage()
        {
            InitializeComponent(); DataContext = this;
            if (UserAuth.Instance.CanRead)
            {
                cv.Visibility = System.Windows.Visibility.Visible;
                nov.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                cv.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            if (UserAuth.Instance.CanRead)
                Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }
    }
}
