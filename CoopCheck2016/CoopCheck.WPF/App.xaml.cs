﻿using System.Windows;
using System.Windows.Media;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //create the notifyicon (it's a resource declared in NotifyIconResources.xaml
            //notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}