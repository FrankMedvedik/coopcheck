using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public interface IJobViewModel
    {
        bool CanRefresh { get; set; }
        ClientReportCriteria ClientReportCriteria { get; set; }
        string HeadingText { get; set; }
        ObservableCollection<JobRpt> Jobs { get; set; }
        int PaymentsCnt { get; }
        decimal? PaymentsTotalDollars { get; }
        JobRpt SelectedJob { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void GetJobs();
        void RefreshAll();
    }
}