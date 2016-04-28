using System;
using System.Collections.ObjectModel;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public class ClientReportCriteriaViewModel : ViewModelBase
    {
        public ClientReportCriteriaViewModel()
        {
          
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
        private ClientReportCriteria  _clientReportCriteria;

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _clientReportCriteria; }
            set
            {
                _clientReportCriteria = value;
                NotifyPropertyChanged();

            }
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
            Clients = new ObservableCollection<CoopCheckClient>(await ClientSvc.GetClients());
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
            set { _clients = value;
                NotifyPropertyChanged(); }
        }

        private Boolean _showGridData;

        public Boolean ShowGridData
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