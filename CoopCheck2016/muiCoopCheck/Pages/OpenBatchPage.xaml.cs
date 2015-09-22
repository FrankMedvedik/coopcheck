using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for EditBatchPage.xaml
    /// </summary>
    public partial class OpenBatchPage : UserControl
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
    }
}
