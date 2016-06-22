using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Models;


namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IJobLogSvc
    {
        Task<JobLog> GetJobLog(int jobNum);
        Task<string> GetJobName(int jobNum);
    }
}