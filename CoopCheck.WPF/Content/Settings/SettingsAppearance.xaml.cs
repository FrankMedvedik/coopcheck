using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class SettingsAppearance : UserControl
    {
        private SettingsAppearanceViewModel _vm;

        public SettingsAppearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            _vm = new SettingsAppearanceViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSettings();

            Messenger.Default.Send(new NavigationMessage()
            {
                Page = "/Pages/Home.xaml"
            });
        }
    }
}