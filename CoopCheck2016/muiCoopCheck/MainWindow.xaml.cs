using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CoopCheck.WPF.Content.Settings;
using CoopCheck.WPF.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadThemeAndColor();
            RegisterNavigation();
            //ContentSource = MenuLinkGroups.First().Links.First().Source;

        }
       

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private void RegisterNavigation()
        {
            Messenger.Default.Register<NavigationMessage>(this, p =>
            {
                var frame = GetDescendantFromName(this, "ContentFrame") as ModernFrame;

                // Set the frame source, which initiates navigation
                if (frame != null)
                {
                    frame.Source = new Uri(p.Page, UriKind.Relative);
                }
            });
        }

        /// <summary>
        /// Gets the name of the descendant from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The FrameworkElement.</returns>
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }
        //public List<VoucherImport> Vouchers
        //{
        //    get { return (List<VoucherImport>)GetValue(VouchersProperty); }
        //    set { SetValue(VouchersProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty VouchersProperty =
        //    DependencyProperty.Register("VouchersProperty", typeof(List<VoucherImport>), typeof(MainWindow), new PropertyMetadata(new List<VoucherImport>()));
        private void LoadThemeAndColor()
        {

            // Loads appearence from application properties
            if(CoopCheck.WPF.Properties.Settings.Default.Theme != "")
                AppearanceManager.Current.ThemeSource = new System.Uri(CoopCheck.WPF.Properties.Settings.Default.Theme, UriKind.Relative);
            if (CoopCheck.WPF.Properties.Settings.Default.FontSize != "")
                AppearanceManager.Current.FontSize = CoopCheck.WPF.Properties.Settings.Default.FontSize == AppearanceViewModel.FontLarge ? FirstFloor.ModernUI.Presentation.FontSize.Large : FirstFloor.ModernUI.Presentation.FontSize.Small;
            if (CoopCheck.WPF.Properties.Settings.Default.AccentColor != "")
                AppearanceManager.Current.AccentColor = (Color)ColorConverter.ConvertFromString(CoopCheck.WPF.Properties.Settings.Default.AccentColor);
            // and make sure accent color is up-to-date
            //this.SelectedAccentColor = new Color(CoopCheck.WPF.Properties.Settings.Default.AccentColor);
        }
        //private void ShowPopUP()
        //{
        //    string title = "Co-op Check";
        //    string text = "Checks are printed";

        //    MyNotifyIcon.ShowBalloonTip(title, text, MyNotifyIcon.Icon);
        //}


    }
}
