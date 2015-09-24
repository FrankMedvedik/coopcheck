﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class RptSvc
    {
        public static async Task<List<vwJobRpt>> GetJobRpt(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwJobRpt
                    where ((l.first_pay_date >= grc.StartDate) 
                        && (l.last_pay_date <= grc.EndDate) 
                        && l.account_id == grc.AccountId) 
                    orderby l.first_pay_date
                    select l).ToListAsync();
                return x;
            }
        }

        public static async Task<List<vwBatchRpt>> GetBatchRpt(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwBatchRpts
                    where ((l.batch_date >= grc.StartDate)
                        && (l.batch_date <= grc.EndDate)
                        && l.account_id == grc.AccountId)
                    orderby l.batch_num
                    select l).ToListAsync();
                return x;
            }
        }

        public static async Task<List<vwPayment>> GetPayments(PaymentReportCriteria crc)
        {
            IQueryable<vwPayment> query;
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                query =
                    ctx.vwPayments.Where(
                        x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate).Take(1000);
                if (!String.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = query.Where(x => x.check_num == crc.CheckNumber);

                }

                if (crc.AccountId !=null)
                {
                    query = query.Where(x => x.account_id == crc.AccountId);

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

                if (crc.IsCleared)
                {
                   query = query.Where(x => x.cleared_flag == true);
                }

                if (crc.IsPrinted)
                {
                    query = query.Where(x => x.print_flag == true);
                }

                if (!String.IsNullOrEmpty(crc.JobNumber))
                {
                    //int filter;
                    //if (int.TryParse(crc.JobNumber, out filter))
                        //query = query.Where(x => x.job_num == n);
                    query =
                        query.Where(
                            x =>
                                System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double) x.job_num)
                                    .Contains(crc.JobNumber));
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

        public static async Task<List<vwPositivePay>> GetPositivePayRpt(GlobalReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await(
                    from l in ctx.vwPositivePay
                    where ((l.check_date >= grc.ReportDateRange.StartRptDate) 
                        && (l.check_date <= grc.ReportDateRange.EndRptDate) 
                        && l.account_number == grc.SelectedAccount.account_number)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
            }
        }
    }
}
