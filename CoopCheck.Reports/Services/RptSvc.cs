using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Services
{
    public class RptSvc : IRptSvc
    {
        private readonly ICoopCheckEntities _coopCheckEntities = null;

        private  RptSvc(ICoopCheckEntities coopCheckEntities)
        {
            _coopCheckEntities = coopCheckEntities;
        }

        public async Task<List<IvwJobRpt>> GetJobRpt(IPaymentReportCriteria grc)
        {
                return await Task.Factory.StartNew(() =>
                    _coopCheckEntities.GetJobPaymentsReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());
        }

        public async Task<List<IvwBatchRpt>> GetBatchRpt(IPaymentReportCriteria grc)
        {
                return await Task.Factory.StartNew(() =>
                    _coopCheckEntities.GetBatchPaymentReport(grc.Account.account_id, grc.StartDate, grc.EndDate).ToList());
        }


        public async Task<List<IvwPayment>> GetPayments(IPaymentReportCriteria crc)
        {
            IQueryable<IvwPayment> query;
            List<IvwPayment> v;
            if (!string.IsNullOrEmpty(crc.CheckNumber))
                {
                    query = _coopCheckEntities.vwPayments.Where(x => x.check_num == crc.CheckNumber);
                }
                else
                {
                    query =
                        _coopCheckEntities.vwPayments.Where(
                            x => x.check_date >= crc.StartDate && x.check_date <= crc.EndDate);

                    if (crc.Account.account_id != new BankAccountSvc(_coopCheckEntities).AllAccountsOption.account_id)
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
            return v;
        }

        public async Task<List<IvwBatchRpt>> GetJobBatchRpt(int jobNumber)
        {
                var x = await (
                    from l in _coopCheckEntities.vwBatchRpts
                    where (l.job_num == jobNumber)
                    orderby l.batch_num
                    select l).ToListAsync();
                return x;
            }
        
        public async Task<List<IvwPayment>> GetAllBatchPayments(int batchNum)
        {
            List<IvwPayment> v;
                v = await (from b in _coopCheckEntities.vwPayments
                    where ((b.batch_num == batchNum))
                    select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwJobRpt>> GetJob(int jobNumber)
        {
            List<IvwJobRpt> v;
                v = await (from b in _coopCheckEntities.vwJobRpts
                    where (b.job_num == jobNumber)
                    select b).ToListAsync();
            return v;
        }


        public async Task<List<IvwJobRpt>> GetAllClientJobs(string clientId)
        {
            List<IvwJobRpt> v;
                v = await (from b in _coopCheckEntities.vwJobRpts
                    where (b.clientid == clientId)
                    select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwJobRpt>> GetAllClientJobs(string clientId, string jobYear)
        {
            List<IvwJobRpt> v;
                v = await (from b in _coopCheckEntities.vwJobRpts
                    where ((b.clientid == clientId) && (b.job_year == jobYear))
                    select b).ToListAsync();
            return v;
        }


        public async Task<List<IvwPayment>> GetJobPayments(int jobNumber)
        {
            List<IvwPayment> v;
                v = await (from b in _coopCheckEntities.vwPayments
                    where (b.job_num == jobNumber)
                    select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwPayment>> GetBatchPayments(IPaymentReportCriteria grc)
        {
            List<IvwPayment> v;
            v = await (from b in _coopCheckEntities.vwPayments
                    where ((b.batch_num == grc.BatchNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            return v;
        }
        public async Task<List<IvwPayment>> GetJobPayments(IPaymentReportCriteria grc)
        {
            List<IvwPayment> v;
                v = await (from b in _coopCheckEntities.vwPayments
                    where ((b.job_num.ToString() == grc.JobNumber)
                           && (b.check_date >= grc.StartDate)
                           && (b.check_date <= grc.EndDate))
                    select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwPositivePay>> GetPositivePayRpt(IPaymentReportCriteria grc)
        {
                var x = await (
                    from l in _coopCheckEntities.vwPositivePays
                    where ((l.check_date >= grc.StartDate)
                           && (l.check_date <= grc.EndDate)
                           && l.account_number == grc.Account.account_number)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
        }

        public async Task<List<IvwPayment>> GetPaymentReconcileReport(IPaymentReportCriteria grc)
        {
            var x = await (
                    from l in _coopCheckEntities.vwPayments
                    where (l.account_id == grc.Account.account_id && !l.cleared_flag)
                    orderby l.check_date
                    select l).ToListAsync();
                return x;
        }

        public async Task<List<IvwPayment>> GetUnclearedCheckReport(IPaymentReportCriteria grc)
        {
            var x = await (
                from l in _coopCheckEntities.vwPayments
                where (l.account_id == grc.Account.account_id && !l.cleared_flag && l.check_num == grc.CheckNumber)
                orderby l.check_date
                select l).ToListAsync();
                return x;
        }

        public async Task<List<IvwPayment>> GetOpenPayments(IPaymentReportCriteria prc)
        {
            IQueryable<IvwPayment> query;
            List<IvwPayment> v;
                query =
                    _coopCheckEntities.vwPayments.Where(
                        x => x.check_date <= prc.EndDate
                             && x.account_id == prc.Account.account_id
                             && x.cleared_flag == false);
                v = await (from b in query
                    select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwVoidedPayment>> GetVoidedPayments(IPaymentReportCriteria crc)
        {
            IQueryable<IvwVoidedPayment> query;
            List<IvwVoidedPayment> v;
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
                v = await (from b in query select b).ToListAsync();
            return v;
        }

        public async Task<List<IvwPayment>> GetPayeePayments(string lastName, string firstName)
        {
            List<IvwPayment> v;
            var query = _coopCheckEntities.vwPayments.Where(x => x.last_name == lastName).Where(x=> x.first_name== firstName).OrderByDescending(x=>x.check_date) ;
                v = await(from b in query select b).ToListAsync();
            return v;
        }
    }
}
