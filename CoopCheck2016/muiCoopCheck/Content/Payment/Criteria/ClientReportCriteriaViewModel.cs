using System;
using System.Collections.ObjectModel;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public class ClientReportCriteriaViewModel : ViewModelBase
    {
        public ClientReportCriteriaViewModel()
        {
          
            ResetState();
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

            ClientReportCriteria = new ClientReportCriteria();
            ClientReportCriteria.SearchType = ClientReportCriteria.ALLCLIENTJOBS;
            Clients = new ObservableCollection<CoopCheckClient>(await ClientSvc.GetClients());

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

        #endregion
    }
}