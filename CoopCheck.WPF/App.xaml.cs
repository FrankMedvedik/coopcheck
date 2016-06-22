using System.Windows;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;

namespace CoopCheck.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {

            DispatcherHelper.Initialize();

        }
        //   private TaskbarIcon notifyIcon;
        public SolidColorBrush AccentBrush
        {
            get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); } 
            
        }
    }
}
