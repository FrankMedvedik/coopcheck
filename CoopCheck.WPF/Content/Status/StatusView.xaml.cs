using System;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Status
{
    /// <summary>
    ///     Description for StatusView.
    /// </summary>
    public partial class StatusView : UserControl
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("StatusInfoProperty", typeof(StatusInfo), typeof(StatusView),
                new PropertyMetadata(new StatusInfo()));

        private readonly StatusViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the StatusView class.
        /// </summary>
        public StatusView()
        {
            InitializeComponent();
            _vm = new StatusViewModel();
            DataContext = _vm;
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.ShowPopupStatus)
                    ModernDialog.ShowMessage(
                        string.Format("{0} {1} {2}", _vm.Status.ErrorMessage, Environment.NewLine,
                            _vm.Status.StatusMessage), "Co-op Check", MessageBoxButton.OK);
            });
        }

        public StatusInfo StatusInfoProperty
        {
            get { return _vm.Status; }
            set { _vm.Status = value; }
        }
    }
}