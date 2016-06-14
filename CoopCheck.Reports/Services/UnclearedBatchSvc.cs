using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Models;

namespace CoopCheck.Reports.Services
{
    public static class UnclearedBatchSvc
    {
        public static async Task<List<UnclearedBatch>> GetUnclearedBatches()
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from a in ctx.vwUnclearedBatches
                    orderby a.batch_num
                    select a).ToListAsync();
                return x;
            }
        }
    }
}