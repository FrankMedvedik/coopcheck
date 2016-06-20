using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.PaymentReports.Job
{
    public interface IJobReportViewModel
    {
        bool CanRefresh { get; set; }
        string HeadingText { get; set; }
        ObservableCollection<JobRpt> Jobs { get; set; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        JobRpt SelectedJob { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetJobs();
        void RefreshAll();
    }
}