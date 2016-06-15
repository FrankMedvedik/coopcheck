using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;
using Remotion.Reflection;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public class ClientReportCriteriaViewModel : ViewModelBase
    {
        private  IClientReportCriteria _clientReportCriteria;
        private readonly IClientSvc _clientSvc;
        public ClientReportCriteriaViewModel(IClientSvc clientSvc, IClientReportCriteria clientReportCriteria)
        {
            _clientSvc = clientSvc;
            _clientReportCriteria = clientReportCriteria;
            ResetState();
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

        public ClientReportCriteria ClientReportCriteria
        {
            get { return (ClientReportCriteria) _clientReportCriteria; }
            set
            {
                _clientReportCriteria = value;
                NotifyPropertyChanged();
            }
        }

        public string JobNumError
        {
            get { return _jobNumError; }
            set
            {
                _jobNumError = value;
                NotifyPropertyChanged();
            }
        }

        public bool ValidateJobNumber(string JobNum)
        {
            JobNumError = "";
            var retVal = false;
            if (string.IsNullOrWhiteSpace(JobNum))
                JobNumError = "New Job Number Required";
            else if ((JobNum.Length != 8))
                JobNumError = "Supplied Job number invalid";
            else
                retVal = true;
            return retVal;
        }

        #region DisplayState

        public async void ResetState()
        {
            Status = new StatusInfo
            {
                IsBusy = true,
                StatusMessage = "please wait. loading clients..."
            };
            ClientReportCriteria = new ClientReportCriteria();
            ClientReportCriteria.SearchType = ClientReportCriteria.ALLCLIENTJOBS;
            var cs = await _clientSvc.GetClients();
            var clients = new List<CoopCheckClient>();
            foreach(var c in cs)
                clients.Add((CoopCheckClient) c);
            Clients = new ObservableCollection<CoopCheckClient>(clients);

            JobYears = new ObservableCollection<string>(JobYearSvc.GetJobYears());
            ClientReportCriteria.SelectedJobYear = ClientReportCriteria.ALLJOBYEARS;
            Status = new StatusInfo
            {
                StatusMessage = "pick a client or enter a specific job number"
            };
            ShowGridData = false;
        }

        public ObservableCollection<CoopCheckClient> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyPropertyChanged();
            }
        }

        private bool _showGridData;

        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<CoopCheckClient> _clients;
        private ObservableCollection<string> _jobYears;
        private StatusInfo _status;
        private string _jobNumError;

        public ObservableCollection<string> JobYears
        {
            get { return _jobYears; }
            set
            {
                _jobYears = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}