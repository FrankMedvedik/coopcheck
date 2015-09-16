using System.ServiceModel.Channels;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using Xceed.Wpf.Toolkit;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for EditBatchPage.xaml
    /// </summary>
    public partial class NewBatchPage : UserControl, IContent
    {
        public NewBatchPage()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            be.ResetState();
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (be.IsDirty)
                be.ResetState();
            MessageBox.Show("tesing reset State");
        }
    }
}
