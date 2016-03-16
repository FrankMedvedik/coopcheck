using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.Repository;
using LinqToExcel.Extensions;

namespace CoopCheck.WPF.Services
{
    public static class JobYearSvc
    {
        public static List<string> GetJobYears()
        {
            //using (var ctx = new CoopCheckEntities())
            //{
            //    var x = await ctx.CoopCheckJobLogs.Select(a => a.jobnum.ToString().Substring(0, 4).Distinct()).ToListAsync();

            {
                var x = new List<string> {"All"};
                for (int y = 2002;y <= DateTime.Now.Year; y++ )
                    x.Add(y.ToString());
                x = x.OrderByDescending(a => a).ToList();

                return x;
            }
        }
    }
}
