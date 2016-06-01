using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public partial class ReplacerView : UserControl
    {
        public static DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof (SolidColorBrush), typeof (ReplacerView),
                new PropertyMetadata(new SolidColorBrush()));

        public static DependencyProperty AlternateValueProperty =
            DependencyProperty.Register("AlternateValue", typeof (string), typeof (ReplacerView),
                new PropertyMetadata(string.Empty));

        public static DependencyProperty SourceValueProperty =
            DependencyProperty.Register("SourceValue", typeof (string), typeof (ReplacerView),
                new PropertyMetadata(string.Empty));

        private StatusInfo _status;

        public ReplacerView()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
            PropertyChanged = SetState;
            SetState(null, null);
        }

        public SolidColorBrush AccentColor
        {
            get { return new SolidColorBrush(AppearanceManager.Current.AccentColor); }
        }

        public string AlternateValue
        {
            get { return (string) GetValue(AlternateValueProperty); }
            set { SetValueDp(AlternateValueProperty, value); }
        }


        public string SourceValue
        {
            get { return (string) GetValue(SourceValueProperty); }
            set { SetValueDp(SourceValueProperty, value); }
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void SetValueDp(DependencyProperty property, object value, [CallerMemberName] string p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        public void SetState(object o, PropertyChangedEventArgs e)
        {
            Replacer.Visibility = Visibility.Collapsed;
            if (AlternateValue == null) return;
            if ((AlternateValue.Trim().Length > 0) && (SourceValue == null))
            {
                Replacer.Visibility = Visibility.Visible;
                return;
            }

            if ((AlternateValue.Trim().Length > 0) &&
                (!SourceValue.Equals(AlternateValue, StringComparison.OrdinalIgnoreCase)))
            {
                Replacer.Visibility = Visibility.Visible;
            }
        }

        private void BtnSwapit_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(string.Format("Swap  {0} with {1}", tbSourceValue.Text, tbAlternateValue.Text));
            SourceValue = AlternateValue;
        }
    }
}