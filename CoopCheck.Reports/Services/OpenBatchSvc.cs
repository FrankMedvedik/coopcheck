using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public static class OpenBatchSvc
    {
        public static async Task<List<IvwOpenBatch>> GetOpenBatches()
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from a in ctx.OpenBatches
                    orderby a.batch_num
                    select a).ToListAsync();
                return x;
            }
        }
    }
}