using System;
using System.Collections.Generic;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
 

namespace CoopCheck.WPF.Services
{
    public static class CheckSvc 
    {
        public static IQueryable<vwCheck> GetChecks(PaymentReportCriteria crc)
        {
            var ctx = new CoopCheckEntities();
            IQueryable<vwCheck> query = 
                ctx.vwChecks.Where(x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate && x.tran_amount > 0) ;
            if (!String.IsNullOrEmpty(crc.CheckNumber))
            {
               query =  query.Where(x => x.check_num == crc.CheckNumber);

            }
            if (crc.BatchNumber != 0)
            {
                query = query.Where(x => x.batch_num == crc.BatchNumber);

            }

            if (!String.IsNullOrEmpty(crc.JobNumber))
            {
                int n;
                if(int.TryParse(crc.JobNumber, out n))
                    query = query.Where(x => x.job_num == n);
            }

            return query;
        }
    }
}