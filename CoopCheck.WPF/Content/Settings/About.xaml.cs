using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    ///     Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        private readonly AboutViewModel _vm;

        public About()
        {
            InitializeComponent();
            _vm = new AboutViewModel();
            DataContext = _vm;
        }
    }
}