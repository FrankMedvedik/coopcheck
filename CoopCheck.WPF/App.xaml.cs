using Ninject.Modules;

using System.Windows;
using System.Windows.Media;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Services;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Threading;
//using Hardcodet.Wpf.TaskbarNotification;

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

public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IBankAccountSvc>().To<BankAccountSvc>();
        }
    }
}
