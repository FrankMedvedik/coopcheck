﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public class ClientPaymentViewModel : ViewModelBase
    {
        private ObservableCollection<vwPayment> _allPayments = new ObservableCollection<vwPayment>();
        private ClientReportCriteria _clientReportCriteria;
        private ObservableCollection<vwPayment> _closedPayments = new ObservableCollection<vwPayment>();

        private int _jobNum;
        private ObservableCollection<vwPayment> _openPayments = new ObservableCollection<vwPayment>();

        private vwBatchRpt _parentBatch;
        private bool _showGridData;
        private StatusInfo _status;
        private List<vwPayment> _workPayments;

        public ClientPaymentViewModel()
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

        public int Job
        {
            get { return _jobNum; }
            set
            {
                _jobNum = value;
                NotifyPropertyChanged();
            }
        }

        public vwBatchRpt ParentBatch
        {
            get { return _parentBatch; }
            set
            {
                if (value != null)
                {
                    _parentBatch = value;
                    NotifyPropertyChanged();
                    RefreshAll();
                }
            }
        }

        public List<vwPayment> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                AllPayments = new ObservableCollection<vwPayment>(WorkPayments);
                ClosedPayments = new ObservableCollection<vwPayment>((
                    from l in _workPayments
                    where l.cleared_flag
                    orderby l.check_date
                    select l).ToList());
                OpenPayments = new ObservableCollection<vwPayment>((
                    from l in _workPayments
                    where !l.cleared_flag
                    orderby l.check_date
                    select l).ToList());
                ShowGridData = true;
            }
        }


        //public string HeadingText => JobReportCriteria.ToFormattedString(' ');
        public string HeadingText => "This is a placeholder";

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

        public ObservableCollection<vwPayment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }

        public ObservableCollection<vwPayment> ClosedPayments
        {
            get { return _closedPayments; }
            set
            {
                _closedPayments = value;
                NotifyPropertyChanged();
            }
        }

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _clientReportCriteria; }
            set
            {
                _clientReportCriteria = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<vwPayment> OpenPayments
        {
            get { return _openPayments; }
            set
            {
                _openPayments = value;
                NotifyPropertyChanged();
            }
        }

        public async void RefreshAll()
        {
            WorkPayments = await RptSvc.GetAllBatchPayments(ParentBatch.batch_num);
        }
    }
}