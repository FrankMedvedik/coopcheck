using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class PaymentSvc 
    {
        public static async Task<List<vwPayment>> GetPayments(PaymentFinderCriteria crc)
        {
            IQueryable<vwPayment> query;
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                query =
                    ctx.vwPayments.Where(
                        x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);
                if (!String.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = query.Where(x => x.check_num == crc.CheckNumber);

                }
                if (!String.IsNullOrWhiteSpace(crc.Email))
                {
                    query = query.Where(x => x.email.Contains(crc.Email));

                }

                if (!String.IsNullOrWhiteSpace(crc.PhoneNumber))
                {
                    query = query.Where(x => x.phone_number.Contains(crc.PhoneNumber));

                }

                if (!String.IsNullOrEmpty(crc.LastName))
                {
                    query = query.Where(x => x.last_name.Contains(crc.LastName));
                }

                if (!String.IsNullOrEmpty(crc.FirstName))
                {
                    query = query.Where(x => x.first_name.Contains(crc.FirstName));
                }

                if (!String.IsNullOrEmpty(crc.JobNumber))
                {
                    int n;
                    if (int.TryParse(crc.JobNumber, out n))
                        query = query.Where(x => x.job_num == n);
                }

                //string ob = String.IsNullOrEmpty(b.last_name) ? b.company : b.last_name;

                v = await (from b in query
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwPayment>> GetPayments(int jobNum)
        {
            List<vwPayment> v; 
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                               where b.job_num == jobNum
                               select b).ToListAsync();
            }
            return v;
        }
    }
}