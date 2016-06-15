using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IJobLogSvc
    {
        Task<IJobLog> GetJobLog(int jobNum);
        Task<string> GetJobName(int jobNum);
    }
}