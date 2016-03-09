using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    public class JobReportCriteriaViewModel : ViewModelBase
    {
        public JobReportCriteriaViewModel()
        {
            ResetState();
        }

 
        private JobReportCriteria  _jobReportCriteria;

        public JobReportCriteria JobReportCriteria
        {
            get { return _jobReportCriteria; }
            set
            {
                _jobReportCriteria = value;
                NotifyPropertyChanged();

            }
        }

        #region DisplayState

        public async void ResetState()
        {

            JobReportCriteria = new JobReportCriteria();
            Clients = new ObservableCollection<client>(await ClientSvc.GetClients());

            ShowGridData = false;
        }

        public ObservableCollection<client> Clients
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

        private ObservableCollection<client> _clients;

        #endregion
    }
}