﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.BankAccount.PositivePay
{
    public class PositivePayReportViewModel : ViewModelBase
    {
        private string _headingText;
        private ObservableCollection<vwPositivePay> _positivePays = new ObservableCollection<vwPositivePay>();

        private vwPositivePay _selectedPositivePay = new vwPositivePay();
        private bool _showGridData;
        private StatusInfo _status;

        public PositivePayReportViewModel()
        {
            ShowGridData = false;
        }

        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public vwPositivePay SelectedPositivePay
        {
            get { return _selectedPositivePay; }
            set
            {
                _selectedPositivePay = value;
                NotifyPropertyChanged();
            }
        }

        public PaymentReportCriteria PaymentReportCriteria { get; set; }

        public ObservableCollection<vwPositivePay> PositivePays
        {
            get { return _positivePays; }
            set
            {
                _positivePays = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PaymentsCnt");
                NotifyPropertyChanged("PaymentsTotalDollars");
                ShowGridData = true;
                HeadingText =
                    string.Format(
                        "{0} Account Positive Payment Report for Payments from  {1:ddd, MMM d, yyyy} through {2:ddd, MMM d, yyyy} ",
                        PaymentReportCriteria.Account.account_name,
                        PaymentReportCriteria.StartDate, PaymentReportCriteria.EndDate);
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    StatusMessage = HeadingText
                };
            }
        }

        public int PaymentsCnt
        {
            get { return PositivePays.Count(); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return PositivePays.Sum(x => x.tran_amount); }
        }

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        public List<string> PositivePayData
        {
            get
            {
                return
                    (from c in PositivePays select c.rpt).ToList();
            }
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        public async void GetPositivePays()
        {
            ShowGridData = false;
            try
            {
                Status = new StatusInfo
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing Positive Pay report"
                };
                PositivePays =
                    new ObservableCollection<vwPositivePay>(await RptSvc.GetPositivePayRpt(PaymentReportCriteria));
            }
            catch (Exception e)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error loading PositivePay list",
                    ErrorMessage = e.Message
                };
            }
        }

        public void RefreshAll()
        {
            GetPositivePays();
        }
    }
}