
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IClientReportCriteria
    {
        CoopCheckJobLog SelectedJob { get; set; }
        string SelectedJobNum { get; set; }
        BatchRpt SelectedBatch { get; set; }
        CoopCheckClient SelectedClient { get; set; }
        string SelectedClientID { get; }
        string SelectedJobYear { get; set; }
        string ALLJOBYEARS { get; }
        string SearchType { get; set; }
        string ONEJOB { get; }
        string ALLCLIENTJOBS { get; }
        string ALLCLIENTJOBSFORYEAR { get; }
        string ToFormattedString(char token);

    }
}