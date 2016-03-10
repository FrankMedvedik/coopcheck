using System;
using System.Collections.Generic;
using CoopCheck.Repository;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Models
{
    public class ClientReportCriteria : ViewModelBase
    {
        private CoopCheckJobLog _selectedJob ;
        public CoopCheckJobLog SelectedJob
        {
            get { return _selectedJob; }
            set {
                _selectedJob = value;
                NotifyPropertyChanged(); }
        }

        private string _selectedJobNum;
        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value;
                NotifyPropertyChanged();
            }
        }

        private batch  _selectedBatch;
        public batch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        private CoopCheckClient _selectedClient;
        public CoopCheckClient SelectedClient
        {
            get { return _selectedClient; }
            set {
                _selectedClient = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SelectedClientID");
            }
        }
        //private string _selectedClientID;
        public string SelectedClientID
        {
            get { return _selectedClient.ClientID; }
            //set
            //{
            //    _selectedClientID = value;
            //    NotifyPropertyChanged();
            //}
        }

        private string _selectedJobYear = "ALL";
        public string SelectedJobYear
        {
            get { return _selectedJobYear; }
            set {
                _selectedJobYear = value;
                NotifyPropertyChanged(); }
        }

        public string SearchType { get; set; }
        public string ONEJOB
        {
            get { return "ONEJOB"; }
        }


        public string ALLCLIENTJOBS
        {
            get { return "ALLCLIENTJOBS "; }
        }

        public string ALLCLIENTJOBSFORYEAR
        {
            get { return "ALLCLIENTJOBSFORYEAR"; }
        }

        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            if (SelectedClient.ClientID!= null)
                v.Add(new KeyValuePair<string, string>("ClientId", SelectedClient.ClientID));
            //if (SelectedJobYear != 0)
            //    v.Add(new KeyValuePair<string, string>("JobYear", SelectedJobYear));
            if (SelectedJob.jobnum != null)
                v.Add(new KeyValuePair<string, string>("JobNum", SelectedJob.jobnum.ToString()));
            foreach (var a  in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }

    }
}