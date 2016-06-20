using System.Windows.Controls;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.Content.AccountManagement.Reconcile;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Pages
{
    public partial class AccountReconcilePage : UserControl
    {

    private ReconcileWindow _vwin = new ReconcileWindow();
    public AccountReconcilePage()
    {
        InitializeComponent();
        DataContext = this;
        if (UserAuth.Instance.CanRead)
        {
        var b = _vwin.ShowDialog();
        Messenger.Default.Send(new NavigationMessage()
        {
            Page = "/Pages/Home.xaml"
        });
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

