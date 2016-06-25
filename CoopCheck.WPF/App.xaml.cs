using System.Windows;
using System.Windows.Media;
using CoopCheck.WPF.IOC;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;

namespace CoopCheck.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModelLocator viewModelLocator;
        static App()
        {

            DispatcherHelper.Initialize();
            viewModelLocator = new ViewModelLocator();
        }
        //   private TaskbarIcon notifyIcon;
        public SolidColorBrush AccentBrush
        {
            get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); } 
            
        }
    }
}
