using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Models
{
    public class ClientReportCriteriaDto : IClientReportCriteria
    {
        public CoopCheckJobLog SelectedJob { get; set; }
        public string SelectedJobNum { get; set; }
        public BatchRpt SelectedBatch { get; set; }
        public CoopCheckClient SelectedClient { get; set; }
        public string SelectedClientID { get; }
        public string SelectedJobYear { get; set; }
        public string ALLJOBYEARS { get; }
        public string SearchType { get; set; }
        public string ONEJOB { get; }
        public string ALLCLIENTJOBS { get; }
        public string ALLCLIENTJOBSFORYEAR { get; }
        public string ToFormattedString(char token)
        {
            throw new NotImplementedException();
        }
    }
}
