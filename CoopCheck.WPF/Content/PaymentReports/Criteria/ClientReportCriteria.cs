using System.Collections.Generic;
using System.Linq;
using CoopCheck.Reports.Contracts;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.Criteria
{
    public class ClientReportCriteria : ViewModelBase, IClientReportCriteria
    {
        private CoopCheckJobLog _selectedJob;
        public CoopCheckJobLog SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedJobNum;
        public string SelectedJobNum
        {
            get { return _selectedJobNum; }
            set
            {
                _selectedJobNum = value.Trim();
                NotifyPropertyChanged();
                SetSearchType();
            }
        }

        private BatchRpt _selectedBatch;
        public BatchRpt SelectedBatch
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
            set
            {
                _selectedClient = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SelectedClientID");
                SetSearchType();
            }
        }
        public string SelectedClientID
        {
            get { return _selectedClient.ClientID; }

        }

        private string _selectedJobYear;
        private string _searchType;

        public string SelectedJobYear
        {
            get { return _selectedJobYear; }
            set
            {
                _selectedJobYear = value.Trim();
                NotifyPropertyChanged();
                SetSearchType();
            }
        }

        private void SetSearchType()
        {
            if (SelectedJobNum != null && SelectedJobNum.Length == 8)
                SearchType = ReportConstants.OneJob;
            else if (SelectedJobYear == ReportConstants.AllJobYears && SelectedClient != null)
                SearchType = ReportConstants.AllClientJobs;
            else if (SelectedJobYear != null && SelectedClient != null)
                SearchType = ReportConstants.AllClientJobsForOneYear;
            else
                SearchType = "INVALID";

        }


        public string SearchType
        {
            get { return _searchType; }
            set
            {
                _searchType = value;
                NotifyPropertyChanged();
            }
        }


        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            if (SelectedClient.ClientID != null)
                v.Add(new KeyValuePair<string, string>("ClientId", SelectedClient.ClientID));
            v.Add(new KeyValuePair<string, string>("JobNum", SelectedJob.jobnum.ToString()));
            return v.Aggregate(s, (current, a) => string.Concat(current, token, a.Key, token, a.Value));
        }

    }
}