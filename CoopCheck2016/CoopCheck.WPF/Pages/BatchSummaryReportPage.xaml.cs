using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{ 
    public partial class BatchSummaryReportPage : UserControl
    {
        public BatchSummaryReportPage()
        {
            InitializeComponent();
            DataContext = this;
            if (UserAuth.Instance.CanRead)
            {
                prv.Visibility = System.Windows.Visibility.Visible;
                nov.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                prv.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
