using System.ComponentModel;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IClientReportCriteria
    {
        ICoopCheckJobLog SelectedJob { get; set; }
        string SelectedJobNum { get; set; }
        Ibatch SelectedBatch { get; set; }
        ICoopCheckClient SelectedClient { get; set; }
        string SelectedClientID { get; }
        string SelectedJobYear { get; set; }
        string ALLJOBYEARS { get; }
        string SearchType { get; set; }
        string ONEJOB { get; }
        string ALLCLIENTJOBS { get; }
        string ALLCLIENTJOBSFORYEAR { get; }
        string ToFormattedString(char token);
        event PropertyChangedEventHandler PropertyChanged;
    }
}