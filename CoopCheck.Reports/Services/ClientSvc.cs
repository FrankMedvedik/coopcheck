using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository;

namespace CoopCheck.Reports.Services
{
    public class ClientSvc : IClientSvc
    {
        private ICoopCheckEntities _coopCheckEntities;


        public ICoopCheckEntities CoopCheckEntities
        {
            get { return _coopCheckEntities ?? (_coopCheckEntities = new CoopCheckEntities()); }
            set { _coopCheckEntities = value; }
        }
        public async Task<List<Contracts.Models.CoopCheckClient>> GetClients()
        {
            var x = await (_coopCheckEntities.CoopCheckClients.OrderBy(a => a.ClientName).ToListAsync());
            var cs = new List<Contracts.Models.CoopCheckClient>();
            foreach (var z in x)
            {
                cs.Add(Mapper.Map<Repository.CoopCheckClient,Contracts.Models.CoopCheckClient>(z));
            }

            return cs;
        }
    }
}