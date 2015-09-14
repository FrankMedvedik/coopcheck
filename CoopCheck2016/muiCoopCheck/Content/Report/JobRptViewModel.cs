using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Report
{
    public class JobRptViewModel : ViewModelBase
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

        public JobRptViewModel()
        {
            ShowGridData = false;
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.GlobalReportCriteriaChanged)
                {
                        ReportDateRange = message.Content.ReportDateRange;
                        RefreshAll();
                }
            });
        }

        private vwJobRpt _selectedJob = new vwJobRpt();
        public vwJobRpt SelectedJob
        {
            get { return _selectedJob; }
            set
            {
                _selectedJob = value;
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
        public  ReportDateRange ReportDateRange = new ReportDateRange();
        #endregion

        private ObservableCollection<vwJobRpt> _jobs = new ObservableCollection<vwJobRpt>();
        public ObservableCollection<vwJobRpt> Jobs
        {
            get { return _jobs; }
            set
            {
                //var s = SelectedBatch;

                _jobs = value;
                NotifyPropertyChanged();
                //if (s != null)
                //{
                //    SelectedBatch = Jobs.FirstOrDefault(x => x.batch_num == s.batch_num);
                //}

            }
        }

        #region Filters
        //private ObservableCollection<LyncCallByRecruiter > _filteredRecruiters = new ObservableCollection<LyncCallByRecruiter >();
        //public ObservableCollection<LyncCallByRecruiter > FilteredRecruiters
        //{
        //    get { return _filteredRecruiters; }
        //    set
        //    {
        //        _filteredRecruiters = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //private void FilterByPhoneRoom()
        //{
        //    var fr = new List<LyncCallByRecruiter>();
        //    if ((SelectedPhoneRoomName == null) || (SelectedPhoneRoomName == "All"))
        //        FilteredRecruiters = Recruiters;
        //    else
        //    {
        //        fr = (from fobjs in Recruiters
        //            where fobjs.PhoneRoom == SelectedPhoneRoomName
        //            select fobjs).ToList();
        //        FilteredRecruiters = new ObservableCollection<LyncCallByRecruiter>(fr);
        //    }

        //            HeadingText = String.Format("{0} Phone Room Activity between {1} and {2} - for {3} Recruiters",
        //                SelectedPhoneRoomName, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate, FilteredRecruiters.Count);
        //            ShowGridData = true;
        //}
        //#endregion


        #endregion

        public  void RefreshAll()
        {
                GetJobs();
                //FilterByPhoneRoom();
        }
        

        private string _headingText;
        private bool _showGridData;

        public string HeadingText
        {
            get {return _headingText;}
            set 
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }
        public async void GetJobs()
        {
              ShowGridData = false;
              HeadingText = "Loading...";
            try
            {
                var ctx = new CoopCheckEntities();
                var query = from l in ctx.vwJobRpt
                            where ((l.first_pay_date >= ReportDateRange.StartRptDate) && (l.last_pay_date <= ReportDateRange.EndRptDate))
                            orderby l.first_pay_date
                            select l;
                var a = new ObservableCollection<vwJobRpt>(query.ToList());
                Jobs = a;
                ShowGridData = true;
                HeadingText = String.Format("{0} Jobs paid between {1:ddd, MMM d, yyyy}  and {2:ddd, MMM d, yyyy}  ",
                Jobs.Count, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
        
            }
            catch (Exception e)
            {
                HeadingText = e.Message;
            }

        }
    }
}