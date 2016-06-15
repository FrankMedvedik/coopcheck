using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IOpenBatchSvc
    {
        Task<List<IvwOpenBatch>> GetOpenBatches();
    }
}