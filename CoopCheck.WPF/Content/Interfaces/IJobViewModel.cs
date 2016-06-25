using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;

namespace CoopCheck.WPF.Content.Interfaces
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