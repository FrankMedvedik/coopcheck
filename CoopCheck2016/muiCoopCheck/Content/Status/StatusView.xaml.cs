using System.Windows.Controls;

namespace CoopCheck.WPF.Content.Status
{
    /// <summary>
    /// Description for StatusView.
    /// </summary>
    public partial class StatusView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the StatusView class.
        /// </summary>
        /// 
        public StatusView()
        {
            InitializeComponent();
            _vm = new StatusViewModel();
            DataContext = _vm;
        }

        private StatusViewModel _vm;

    }
}