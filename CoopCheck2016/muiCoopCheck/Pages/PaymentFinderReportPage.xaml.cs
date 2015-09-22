using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// Interaction logic for CheckReportPage.xaml
    /// </summary>
    public partial class PaymentFinderReportPage : UserControl
    {
        public PaymentFinderReportPage()
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
