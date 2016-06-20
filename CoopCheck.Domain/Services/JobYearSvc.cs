using System;
using System.Collections.Generic;
using System.Linq;

namespace CoopCheck.Domain.Services
{
    public static class JobYearSvc
    {
        public static List<string> GetJobYears()
        {
            {
                var x = new List<string> {"All"};
                for (var y = 2002; y <= DateTime.Now.Year; y++)
                    x.Add(y.ToString());
                x = x.OrderByDescending(a => a).ToList();

                return x;
            }
        }
    }
}