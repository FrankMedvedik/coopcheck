﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Import
{

    public partial class ReplacerView : UserControl 
    {
        public ReplacerView()
        {
            InitializeComponent();
            (Content as FrameworkElement).DataContext = this;
            PropertyChanged = SetState;
        }



        public string AlternateValue
        {
            get { return (string)GetValue(AlternateValueProperty); }
            set { SetValueDp(AlternateValueProperty, value); }
        }
        public static readonly DependencyProperty AlternateValueProperty =
            DependencyProperty.Register("AlternateValue", typeof(string), typeof(ReplacerView), new PropertyMetadata(""));


        public string SourceValue
        {
            get { return (string)GetValue(SourceValueProperty); }
            set { SetValueDp(SourceValueProperty, value);
            }
        }
        public static readonly DependencyProperty SourceValueProperty =
            DependencyProperty.Register("SourceValue", typeof(string), typeof(ReplacerView), new PropertyMetadata(""));

        private StatusInfo _status;

        public event PropertyChangedEventHandler PropertyChanged;


        void SetValueDp(DependencyProperty property, object value, [CallerMemberName] string p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        private void SetState(object o, PropertyChangedEventArgs e)
        {

            //Replacer.Visibility = Visibility.Visible;
            //Status = new StatusInfo()
            //{
            //    StatusMessage = "Set state called " + DateTime.Now
            //};

            Replacer.Visibility = Visibility.Collapsed;
            if (AlternateValue == null) return;
            if ((AlternateValue.Length > 0) && (SourceValue == null))
            {
                Replacer.Visibility = Visibility.Visible;
                return;
            }

            if ((AlternateValue.Length > 0) && (!SourceValue.Equals(AlternateValue, StringComparison.OrdinalIgnoreCase)))
            {
                Replacer.Visibility = Visibility.Visible;
            }
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

        private void BtnSwapit_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(string.Format("Swap  {0} with {1}", tbSourceValue.Text, tbAlternateValue.Text));
            SourceValue = AlternateValue;
        }
    }
}
