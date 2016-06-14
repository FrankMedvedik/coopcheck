using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class OpenBatchSvc
    {
        public static async Task<List<OpenBatch>> GetOpenBatches()
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