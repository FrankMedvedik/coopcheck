using System.Collections.ObjectModel;

namespace CoopCheck.WPF.Content.PaymentReports.Criteria
{
    public interface IClientReportCriteriaViewModel
    {
        ClientReportCriteria ClientReportCriteria { get; set; }
        ObservableCollection<CoopCheckClient> Clients { get; set; }
        string JobNumError { get; set; }
        ObservableCollection<string> JobYears { get; set; }
        bool ShowGridData { get; set; }
        StatusInfo Status { get; set; }

        void ResetState();
        bool ValidateJobNumber(string JobNum);
    }
}