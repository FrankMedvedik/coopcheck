using System;
using System.Windows.Media;
using CoopCheck.WPF.Content.Settings;
using CoopCheck.WPF.Models;
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
            //ShowPopUP();

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
