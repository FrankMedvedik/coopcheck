using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for CheckReportPage.xaml
    /// </summary>
    public partial class OpenPaymentReportPage : UserControl
    {
        public OpenPaymentReportPage()
        {
            InitializeComponent(); DataContext = this;
            if (UserAuth.Instance.CanRead)
            {
                pfv.Visibility = System.Windows.Visibility.Visible;
                nov.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                pfv.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
