using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

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

    public StatusInfo StatusInfoProperty
    {
        get { return _vm.Status; }
        set { _vm.Status = value; }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("StatusInfoProperty", typeof(StatusInfo), typeof(StatusView), new PropertyMetadata(new StatusInfo()));
    }
    
}