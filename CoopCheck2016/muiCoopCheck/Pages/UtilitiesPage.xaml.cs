using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for NewBatchPage.xaml
    /// </summary>
    public partial class UtilitiesPage : UserControl
    {
        public UtilitiesPage()
        {
            InitializeComponent();
            DataContext = this;

            pfv.Visibility = System.Windows.Visibility.Collapsed;
            nov.Visibility = System.Windows.Visibility.Visible;

            //if (UserAuth.Instance.CanRead)
            //{
            //    pfv.Visibility = System.Windows.Visibility.Visible;
            //    nov.Visibility = System.Windows.Visibility.Collapsed;

            //}
            //else
            //{
            //    pfv.Visibility = System.Windows.Visibility.Collapsed;
            //    nov.Visibility = System.Windows.Visibility.Visible;
            //}
        }
    }
}
