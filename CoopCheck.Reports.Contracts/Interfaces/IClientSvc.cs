using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IClientSvc
    {
        Task<List<ICoopCheckClient>> GetClients();
    }
}