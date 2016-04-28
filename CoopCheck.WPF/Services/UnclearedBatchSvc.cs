using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Services
{
    public static class UnclearedBatchSvc
    {
        public static async Task<List<vwUnclearedBatch>> GetUnclearedBatches()
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