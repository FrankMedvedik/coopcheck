using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Models;



namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IOpenBatchSvc
    {
        Task<List<OpenBatch>> GetOpenBatches();
    }
}