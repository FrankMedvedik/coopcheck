using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using CoopCheck.WPF.Properties;
using CoopCheck.WPF.Services;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Threading;
using log4net;
using Reckner.WPF.Services;

//using Hardcodet.Wpf.TaskbarNotification;

namespace CoopCheck.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public App() : base()
        {
            log.Info("CoopCheck Started");
            DispatcherHelper.Initialize();
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;

        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            log.Error(e.Exception.StackTrace);
            log.Error(e.Exception.Message);
            if(e.Exception.InnerException != null) log.Error(e.Exception.InnerException.Message);
            List<string> RecipientList = new List<string>() { Settings.Default.TechSupport};
            try
            {
                EmailSvc.CreateOutlookEmail(RecipientList, "coopcheck crash report", e.Exception.Message, @"c:\Logs\coopcheck.log");
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                log.Error(ex.Message);
            }
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            ModernDialog.ShowMessage(errorMessage,"CoopCheck Shutdown", MessageBoxButton.OK);
            e.Handled = true;
            this.Shutdown();
        }

        public SolidColorBrush AccentBrush
        {
            get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); } 
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            base.OnStartup(e);
            ConnTest.ValidateAllServices();
        }
    }
}
