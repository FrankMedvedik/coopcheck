﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class RptSvc
    {
        public static async Task<List<vwJobRpt>> GetJobRpt(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                return await Task.Factory.StartNew(() =>
                    ctx.GetJobPaymentsReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());
            }
        }

        public static async Task<List<vwBatchRpt>> GetBatchRpt(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                return await Task.Factory.StartNew(() =>
                    ctx.GetBatchPaymentReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());

                //var x = await (
                //    from l in ctx.vwBatchRpts
                //    where ((l.batch_date >= grc.StartDate)
                //        && (l.batch_date <= grc.EndDate)
                //        && l.account_id == grc.Account.account_id)
                //    orderby l.batch_num
                //    select l).ToListAsync();
                //return x;
            }
        }


        public static async Task<List<vwPayment>> GetPayments(PaymentReportCriteria crc)
        {
            IQueryable<vwPayment> query;
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                if (!string.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = ctx.vwPayments.Where(x => x.check_num == crc.CheckNumber);
                }
                else
                {
                    query =
                        ctx.vwPayments.Where(
                            x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);

                    if (crc.Account.account_id != BankAccountSvc.AllAccountsOption.account_id)
                        query = query.Where(x => x.account_id == crc.Account.account_id);

                    if (!string.IsNullOrWhiteSpace(crc.Email))
                    {
                        query = query.Where(x => x.email.Contains(crc.Email));
                    }

                    if (!string.IsNullOrWhiteSpace(crc.PhoneNumber))
                    {
                        query = query.Where(x => x.phone_number.Contains(crc.PhoneNumber));
                    }

                    if (!string.IsNullOrEmpty(crc.LastName))
                    {
                        query = query.Where(x => x.last_name.Contains(crc.LastName));
                    }

                    if (!string.IsNullOrEmpty(crc.FirstName))
                    {
                        query = query.Where(x => x.first_name.Contains(crc.FirstName));
                    }


                    if (crc.BatchNumber != null)
                    {
                        query =
                            query.Where(
                                x => x.batch_num == crc.BatchNumber);
                    }

                    if (!string.IsNullOrEmpty(crc.JobNumber))
                    {
                        query =
                            query.Where(
                                x =>
                                    SqlFunctions.StringConvert((double) x.job_num)
                                        .Contains(crc.JobNumber));
                    }
                    //string ob = String.IsNullOrEmpty(b.last_name) ? b.company : b.last_name;
                }
                v = await (from b in query
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwBatchRpt>> GetJobBatchRpt(int jobNumber)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwBatchRpts
                    where (l.job_num == jobNumber)
                    orderby l.batch_num
                    select l).ToListAsync();
                return x;
            }
        }


        public static async Task<List<vwPayment>> GetAllBatchPayments(int batchNum)
        {
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                    where ((b.batch_num == batchNum))
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwJobRpt>> GetJob(int jobNumber)
        {
            List<vwJobRpt> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwJobRpts
                    where (b.job_num == jobNumber)
                    select b).ToListAsync();
            }
            return v;
        }


        public static async Task<List<vwJobRpt>> GetAllClientJobs(string clientId)
        {
            List<vwJobRpt> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwJobRpts
                    where (b.clientid == clientId)
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwJobRpt>> GetAllClientJobs(string clientId, string jobYear)
        {
            List<vwJobRpt> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwJobRpts
                    where ((b.clientid == clientId) && (b.job_year == jobYear))
                    select b).ToListAsync();
            }
            return v;
        }


        public static async Task<List<vwPayment>> GetJobPayments(int jobNumber)
        {
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                    where (b.job_num == jobNumber)
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwPayment>> GetBatchPayments(PaymentReportCriteria grc)
        {
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                    where ((b.batch_num == grc.BatchNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            }
            return v;
        }


        public static async Task<List<vwPayment>> GetJobPayments(PaymentReportCriteria grc)
        {
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                v = await (from b in ctx.vwPayments
                    where ((b.job_num.ToString() == grc.JobNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwPositivePay>> GetPositivePayRpt(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwPositivePay
                    where ((l.check_date >= grc.StartDate)
                           && (l.check_date <= grc.EndDate)
                           && l.account_number == grc.Account.account_number)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
            }
        }

        public static async Task<List<vwPayment>> GetPaymentReconcileReport(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwPayments
                    where (l.account_id == grc.Account.account_id && !l.cleared_flag)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
            }
        }

        public static async Task<List<vwPayment>> GetUnclearedCheckReport(PaymentReportCriteria grc)
        {
            using (var ctx = new CoopCheckEntities())
            {
                var x = await (
                    from l in ctx.vwPayments
                    where (l.account_id == grc.Account.account_id && !l.cleared_flag && l.check_num == grc.CheckNumber)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
            }
        }

        public static async Task<List<vwPayment>> GetOpenPayments(PaymentReportCriteria prc)
        {
            IQueryable<vwPayment> query;
            List<vwPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                query =
                    ctx.vwPayments.Where(
                        x => x.check_date <= prc.EndDate
                             && x.account_id == prc.Account.account_id
                             && x.cleared_flag == false);
                v = await (from b in query
                    select b).ToListAsync();
            }
            return v;
        }

        public static async Task<List<vwVoidedPayment>> GetVoidedPayments(PaymentReportCriteria crc)
        {
            IQueryable<vwVoidedPayment> query;
            List<vwVoidedPayment> v;
            using (var ctx = new CoopCheckEntities())
            {
                query =
                    ctx.vwVoidedPayments.Where(x => x.account_id == crc.Account.account_id)
                        .OrderBy(x => x.check_num)
                        .ThenBy(x => x.check_date);
                ;
                // if they pass a check number in ignore other criteria
                if (!string.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = query.Where(x => x.check_num == crc.CheckNumber);
                }
                else
                {
                    query = query.Where(x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);

                    if (!string.IsNullOrWhiteSpace(crc.Email))
                    {
                        query = query.Where(x => x.email.Contains(crc.Email));
                    }

                    if (!string.IsNullOrWhiteSpace(crc.PhoneNumber))
                    {
                        query = query.Where(x => x.phone_number.Contains(crc.PhoneNumber));
                    }

                    if (!string.IsNullOrEmpty(crc.LastName))
                    {
                        query = query.Where(x => x.last_name.Contains(crc.LastName));
                    }

                    if (!string.IsNullOrEmpty(crc.FirstName))
                    {
                        query = query.Where(x => x.first_name.Contains(crc.FirstName));
                    }

                    if (crc.BatchNumber != null)
                    {
                        query =
                            query.Where(
                                x => x.batch_num == crc.BatchNumber);
                    }

                    if (!string.IsNullOrEmpty(crc.JobNumber))
                    {
                        query =
                            query.Where(
                                x =>
                                    SqlFunctions.StringConvert((double) x.job_num)
                                        .Contains(crc.JobNumber));
                    }
                }
                v = await (from b in query select b).ToListAsync();
            }
            return v;
        }
    }
}