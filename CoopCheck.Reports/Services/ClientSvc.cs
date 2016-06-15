using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public class ClientSvc : IClientSvc
    {
        private readonly ICoopCheckEntities _coopCheckEntities = null;

        public ClientSvc(ICoopCheckEntities coopCheckEntities)
        {
            _coopCheckEntities = coopCheckEntities;
        }

        public async Task<List<ICoopCheckClient>> GetClients()
        {
            var x = await (_coopCheckEntities.CoopCheckClients.OrderBy(a => a.ClientName).ToListAsync());
            return x;
        }
    }
}