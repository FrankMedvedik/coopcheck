using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    ///     Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        private readonly AppearanceViewModel _vm;

        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            _vm = new AppearanceViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSettings();
        }
    }
}