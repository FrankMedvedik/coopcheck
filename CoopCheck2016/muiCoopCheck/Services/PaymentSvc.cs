using System;
using System.Collections.Generic;
using System.Linq;
using CoopCheck.Repository;
using muiCoopCheck.Models;
using muiCoopCheck.Pages.Checks;

namespace muiCoopCheck.Services
{
    public static class PaymentSvc 
    {
        public static IQueryable<vwPayment> GetPayments(PaymentReportCriteria crc)
        {
            var ctx = new CoopCheckEntities();
            IQueryable<vwPayment> query = ctx.vwPayments.Where(x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate && x.tran_amount > 0);
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