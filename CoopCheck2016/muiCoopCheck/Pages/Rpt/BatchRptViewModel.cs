using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using GalaSoft.MvvmLight.Messaging;

namespace muiCoopCheck.Pages.Rpt
{
    public class BatchRptViewModel : ViewModelBase
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

        public BatchRptViewModel()
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

        private vwRptBatchPayment _selectedBatch = new vwRptBatchPayment();
        public vwRptBatchPayment SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
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

        private ObservableCollection<vwRptBatchPayment> _batches = new ObservableCollection<vwRptBatchPayment>();
        public ObservableCollection<vwRptBatchPayment> Batches
        {
            get { return _batches; }
            set
            {
                //var s = SelectedBatch;

                _batches = value;
                NotifyPropertyChanged();
                //if (s != null)
                //{
                //    SelectedBatch = Batches.FirstOrDefault(x => x.batch_num == s.batch_num);
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
                GetBatches();
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
        public async void GetBatches()
        {
              ShowGridData = false;
              HeadingText = "Loading...";
            try
            {
                CoopCheckEntities ctx = new CoopCheckEntities();
                var a = ctx.vwRptBatchPayments.Where(x => x.total_amount > 35000);
                        //x => x.batch_date >= ReportDateRange.StartRptDate && x.batch_date <= ReportDateRange.EndRptDate).OrderBy(x => x.batch_date).ToList();
                Batches = new ObservableCollection<vwRptBatchPayment>(a);
                ShowGridData = true;
                HeadingText = String.Format("{0} Batches Dated between {1} and {2} ",
                Batches.Count, ReportDateRange.StartRptDate, ReportDateRange.EndRptDate);
        
            }
            catch (Exception e)
            {
                HeadingText = e.Message;
            }

        }
    }
}