using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.Repository;


namespace CoopCheck.Reports.Services
{
    public class RptSvc : IRptSvc
    {
        private ICoopCheckEntities _coopCheckEntities;

        public ICoopCheckEntities CoopCheckEntities
        {
            get { return _coopCheckEntities ?? (_coopCheckEntities = new CoopCheckEntities()); }
            set { _coopCheckEntities = value; }
        }

        public async Task<List<JobRpt>> GetJobRpt(IPaymentReportCriteria grc)
        {
            var dbJobRpts = await Task.Factory.StartNew(() =>
                _coopCheckEntities.GetJobPaymentsReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());
            return  DbToJobRpt(dbJobRpts);
        }

        private static List<JobRpt> DbToJobRpt(List<vwJobRpt> dbJobRpts)
        {
            var jobRpts = new List<JobRpt>();
            foreach (var dbj in dbJobRpts)
            {
                jobRpts.Add(Mapper.Map<vwJobRpt, JobRpt>(dbj));
            }
            return jobRpts;
        }

        public async Task<List<BatchRpt>> GetBatchRpt(IPaymentReportCriteria grc)
        {
            var dbRpts = await Task.Factory.StartNew(() =>
                _coopCheckEntities.GetBatchPaymentReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());
            return DbtoBatchRpt(dbRpts);
        }

        private static List<BatchRpt> DbtoBatchRpt(List<vwBatchRpt> dbRpts)
        {
            var rpts = new List<BatchRpt>();
            foreach (var dbj in dbRpts)
            {
                rpts.Add(Mapper.Map<vwBatchRpt, BatchRpt>(dbj));
            }
            return rpts;
        }


        public async Task<List<Payment>> GetPayments(IPaymentReportCriteria crc)
        {
            IQueryable<vwPayment> query;
            if (!string.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = _coopCheckEntities.vwPayments.Where(x => x.check_num == crc.CheckNumber);
                }
                else
                {
                    query =
                        _coopCheckEntities.vwPayments.Where(
                            x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);

                    if (crc.Account.account_id != new BankAccountSvc().AllAccountsOption.account_id)
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
            var dbRpts = await (from b in query
                    select b).ToListAsync();
            return  DbtoPayment(dbRpts);
        }

        private static List<Payment> DbtoPayment(List<vwPayment> dbRpts)
        {
            var rpts = new List<Payment>();
            foreach (var dbj in dbRpts)
            {
                rpts.Add(Mapper.Map<vwPayment, Payment>(dbj));
            }
            return rpts;
        }

        public async Task<List<BatchRpt>> GetJobBatchRpt(int jobNumber)
        {
            var dbRpts = await (
                from l in _coopCheckEntities.vwBatchRpts
                where (l.job_num == jobNumber)
                orderby l.batch_num
                select l).ToListAsync();
            return DbtoBatchRpt(dbRpts);
        }

        public async Task<List<Payment>> GetAllBatchPayments(int batchNum)
        {
            var dbRpts  = await (from b in _coopCheckEntities.vwPayments
                where ((b.batch_num == batchNum))
                select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }

        public async Task<List<JobRpt>> GetJob(int jobNumber)
        {
            var dbJobRpts  = await (from b in _coopCheckEntities.vwJobRpts
                where (b.job_num == jobNumber)
                select b).ToListAsync();
            return DbToJobRpt(dbJobRpts);

        }


        public async Task<List<JobRpt>> GetAllClientJobs(string clientId)
        {
            var dbJobRpts = await (from b in _coopCheckEntities.vwJobRpts
                where (b.clientid == clientId)
                select b).ToListAsync();
            return DbToJobRpt(dbJobRpts);
        }

        public async Task<List<JobRpt>> GetAllClientJobs(string clientId, string jobYear)
        {
            var dbJobRpts  = await (from b in _coopCheckEntities.vwJobRpts
                where ((b.clientid == clientId) && (b.job_year == jobYear))
                select b).ToListAsync();
            return DbToJobRpt(dbJobRpts);
        }


        public async Task<List<Payment>> GetJobPayments(int jobNumber)
        {
                var dbRpts = await (from b in _coopCheckEntities.vwPayments
                    where (b.job_num == jobNumber)
                    select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }

        public async Task<List<Payment>> GetBatchPayments(IPaymentReportCriteria grc)
        {

            var dbRpts = await (from b in _coopCheckEntities.vwPayments
                    where ((b.batch_num == grc.BatchNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }
        public async Task<List<Payment>> GetJobPayments(IPaymentReportCriteria grc)
        {
                var dbRpts = await (from b in _coopCheckEntities.vwPayments
                    where ((b.job_num.ToString() == grc.JobNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }

        public async Task<List<PositivePayItem>> GetPositivePayRpt(IPaymentReportCriteria grc)
        {
                var dbRpts = await (
                    from l in _coopCheckEntities.vwPositivePays
                    where ((l.check_date >= grc.StartDate)
                           && (l.check_date <= grc.EndDate)
                           && l.account_number == grc.Account.account_number)
                    orderby l.check_date
                    select l).ToListAsync();
            return  DbtoPositivePayRpt(dbRpts);
            
        }

        private static List<PositivePayItem> DbtoPositivePayRpt(List<vwPositivePay> dbRpts)
        {
            var rpts = new List<PositivePayItem>();
            foreach (var dbj in dbRpts)
            {
                rpts.Add(Mapper.Map<vwPositivePay, PositivePayItem>(dbj));
            }
            return rpts;
        }

        public async Task<List<Payment>> GetPaymentReconcileReport(IPaymentReportCriteria grc)
        {
            var dbRpts = await (
                    from l in _coopCheckEntities.vwPayments
                    where (l.account_id == grc.Account.account_id && !l.cleared_flag)
                    orderby l.check_date
                    select l).ToListAsync();
            return DbtoPayment(dbRpts);

        }

        public async Task<List<Payment>> GetUnclearedCheckReport(IPaymentReportCriteria grc)
        {
            var dbRpts = await (
                from l in _coopCheckEntities.vwPayments
                where (l.account_id == grc.Account.account_id && !l.cleared_flag && l.check_num == grc.CheckNumber)
                orderby l.check_date
                select l).ToListAsync();
            return DbtoPayment(dbRpts);
        }

        public async Task<List<Payment>> GetOpenPayments(IPaymentReportCriteria prc)
        {
            IQueryable<vwPayment> query;
                query =
                    _coopCheckEntities.vwPayments.Where(
                        x => x.check_date <= prc.EndDate
                             && x.account_id == prc.Account.account_id
                             && x.cleared_flag == false);
                var dbRpts = await (from b in query select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }

        public async Task<List<VoidedPayment>> GetVoidedPayments(IPaymentReportCriteria crc)
        {
            IQueryable<vwVoidedPayment> query;
                query =
                    _coopCheckEntities.vwVoidedPayments.Where(x => x.account_id == crc.Account.account_id)
                        .OrderBy(x => x.check_num)
                        .ThenBy(x => x.check_date);
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
            var dbRpts = await (from b in query select b).ToListAsync();
            var rpts = DBtoVoidedPayments(dbRpts);
            return rpts;
        }

        private static List<VoidedPayment> DBtoVoidedPayments(List<vwVoidedPayment> dbRpts)
        {
            var rpts = new List<VoidedPayment>();
            foreach (var dbj in dbRpts)
            {
                rpts.Add(Mapper.Map<vwVoidedPayment, VoidedPayment>(dbj));
            }
            return rpts;
        }

        public async Task<List<Payment>> GetPayeePayments(string lastName, string firstName)
        {
            var query = _coopCheckEntities.vwPayments.Where(x => x.last_name == lastName).Where(x=> x.first_name== firstName).OrderByDescending(x=>x.check_date) ;
            var dbRpts = await(from b in query select b).ToListAsync();
            return DbtoPayment(dbRpts);
        }
    }
}
