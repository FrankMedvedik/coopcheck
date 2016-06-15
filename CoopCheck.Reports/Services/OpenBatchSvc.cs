using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public  class OpenBatchSvc : IOpenBatchSvc
    {
        private readonly ICoopCheckEntities _coopCheckEntities = null;

        public OpenBatchSvc(ICoopCheckEntities coopCheckEntities)
        {
            _coopCheckEntities = coopCheckEntities;
        }
        public async Task<List<IvwOpenBatch>> GetOpenBatches()
        {
                var x = await (
                    from a in _coopCheckEntities.vwOpenBatches
                    orderby a.batch_num
                    select a).ToListAsync();
                return x;
            }
        }
    }
