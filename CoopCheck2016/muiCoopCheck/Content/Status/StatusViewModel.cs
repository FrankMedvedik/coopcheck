﻿using System;
using System.Windows;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Status
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StatusViewModel : ViewModel.ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the StatusViewModel class.
        /// </summary>
        /// 
        public StatusViewModel()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "Welcome to Coopcheck",
                ErrorMessage = "",
                IsBusy =false
            };

            Status = s;

            Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            {
                Status = ObjectCopier.Clone(message.Content);
                //if (Status.ShowMessageBox)
                //{
                //    Messenger.Default.Send(new NotificationMessage(Notifications.ShowPopupStatus));
                //    Status.ShowMessageBox = false;
                //}
            });

        }
        private StatusInfo _status;

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShowError");
            }
        }

        public Visibility ShowError
        {
            get { return Status?.ErrorMessage?.Trim().Length > 0
                    ? Visibility.Visible: Visibility.Hidden; }
        }
    }
}