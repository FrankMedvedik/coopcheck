using System;
using System.Windows;
using System.Windows.Media;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.Content.Settings;
using CoopCheck.WPF.Messages;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
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
   
        }
       

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private void RegisterNavigation()
        {
            Messenger.Default.Register<NavigationMessage>(this, p =>
            {
                var frame = GetDescendantFromName(this, "ContentFrame") as ModernFrame;
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
  
        private void LoadThemeAndColor()
        {

            // Loads appearence from application properties
            if(CoopCheck.WPF.Properties.Settings.Default.Theme != "")
                AppearanceManager.Current.ThemeSource = new System.Uri(CoopCheck.WPF.Properties.Settings.Default.Theme, UriKind.Relative);
            if (CoopCheck.WPF.Properties.Settings.Default.FontSize != "")
                AppearanceManager.Current.FontSize = CoopCheck.WPF.Properties.Settings.Default.FontSize == AppearanceViewModel.FontLarge ? FirstFloor.ModernUI.Presentation.FontSize.Large : FirstFloor.ModernUI.Presentation.FontSize.Small;
            if (CoopCheck.WPF.Properties.Settings.Default.AccentColor != "")
                AppearanceManager.Current.AccentColor = (Color)ColorConverter.ConvertFromString(CoopCheck.WPF.Properties.Settings.Default.AccentColor);
        }
    
        }
    }
