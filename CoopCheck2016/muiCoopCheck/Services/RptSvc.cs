using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Report;

namespace CoopCheck.WPF.Services
{
    public static class RptSvc
    {
        public static async Task<List<vwJobRpt>> GetJobRpt(ReportDateRange ReportDateRange)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwJobRpt
                    where ((l.first_pay_date >= ReportDateRange.StartRptDate) && (l.last_pay_date <= ReportDateRange.EndRptDate))
                    orderby l.first_pay_date
                    select l).ToListAsync();
                return x;
            }
        }
    }
}
