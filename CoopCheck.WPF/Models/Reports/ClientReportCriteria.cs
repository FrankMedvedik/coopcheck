using System.Collections.Generic;
using System.Linq;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Models
{
    public class ClientReportCriteria : ViewModelBase, IClientReportCriteria
    {
        private ICoopCheckJobLog _selectedJob ;
        public ICoopCheckJobLog SelectedJob
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
                _selectedJobNum = value.Trim();
                NotifyPropertyChanged();
                SetSearchType();
            }
        }

        private Ibatch  _selectedBatch;
        public Ibatch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        private ICoopCheckClient _selectedClient;
        public ICoopCheckClient SelectedClient
        {
            get { return _selectedClient; }
            set {
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
            set {
                _selectedJobYear = value.Trim();
                NotifyPropertyChanged();
                SetSearchType();
            }
        }

        private void SetSearchType()
        {
            if (SelectedJobNum != null && SelectedJobNum.Length == 8)
                SearchType = ONEJOB;
            else if (SelectedJobYear == ALLJOBYEARS && SelectedClient != null)
                SearchType = ALLCLIENTJOBS;
            else if(SelectedJobYear != null && SelectedClient != null)
                SearchType = ALLCLIENTJOBSFORYEAR;
            else
                SearchType = "INVALID";

        }

        public string ALLJOBYEARS {get { return "All"; } 
        }

        public string SearchType    
        {
            get { return _searchType; }
            set { _searchType = value;
                NotifyPropertyChanged(); }
        }

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
            v.Add(new KeyValuePair<string, string>("JobNum", SelectedJob.jobnum.ToString()));
            return v.Aggregate(s, (current, a) => string.Concat(current, token, a.Key, token, a.Value));
        }

    }
}