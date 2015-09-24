﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Report
{
    public class PositiveRptViewModel : ViewModelBase
    {
        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public PositiveRptViewModel()
        {
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                        GlobalReportCriteria.ReportDateRange = message.Content.ReportDateRange;
                        GlobalReportCriteria.SelectedAccount = message.Content.SelectedAccount;
                        RefreshAll();
                }
            });
        }

        private vwPositivePay _selectedPositivePay = new vwPositivePay();
        public vwPositivePay SelectedPositivePay
        {
            get { return _selectedPositivePay; }
            set
            {
                _selectedPositivePay = value;

                NotifyPropertyChanged();
            }
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh       
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public  GlobalReportCriteria GlobalReportCriteria = new GlobalReportCriteria();
        #endregion

        private ObservableCollection<vwPositivePay> _positivePays = new ObservableCollection<vwPositivePay>();
        public ObservableCollection<vwPositivePay> PositivePays
        {
            get { return _positivePays; }
            set
            {
                _positivePays = value;
                NotifyPropertyChanged();
                ShowGridData = true;
                HeadingText = String.Format("{0} Account Positive Payment Report for {1} checks from  {2:ddd, MMM d, yyyy} through {3:ddd, MMM d, yyyy}  ",
                                        GlobalReportCriteria.SelectedAccount.account_name, 
                                        PositivePays.Count, GlobalReportCriteria.ReportDateRange.StartRptDate, GlobalReportCriteria.ReportDateRange.EndRptDate);
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    StatusMessage = HeadingText
                };
            }
        }

        public  void RefreshAll()
        {
                GetPositivePays();
        }
        

        private string _headingText;
        private bool _showGridData;
        private StatusInfo _status;

        public string HeadingText
        {
            get {return _headingText;}
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
                Status = new StatusInfo()
                {
                    ErrorMessage = "",
                    IsBusy = true,
                    StatusMessage = "refreshing Positive Pay report"
                };
                PositivePays = new ObservableCollection<vwPositivePay>(await RptSvc.GetPositivePayRpt(GlobalReportCriteria));
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading PositivePay list",
                    ErrorMessage = e.Message
                };

            }
        }

        }
    }
