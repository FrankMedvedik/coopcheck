using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.Repository;


namespace CoopCheck.Reports.Services
{
    public  class OpenBatchSvc : IOpenBatchSvc
    {
        private ICoopCheckEntities _coopCheckEntities;

        public ICoopCheckEntities CoopCheckEntities
        {
            get { return _coopCheckEntities ?? (_coopCheckEntities = new CoopCheckEntities()); }
            set { _coopCheckEntities = value; }
        }

        public async Task<List<OpenBatch>> GetOpenBatches()
        {
                var x = await (
                    from a in _coopCheckEntities.vwOpenBatches
                    orderby a.batch_num
                    select a).ToListAsync();
            var obs = new List<OpenBatch>();
            foreach (var z in x)
            {
                obs.Add(Mapper.Map<vwOpenBatch, OpenBatch>(z));
            }
            return obs;
            }
        }
    }
