using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IRptSvc
    {
        Task<List<Payment>> GetAllBatchPayments(int batchNum);
        Task<List<JobRpt>> GetAllClientJobs(string clientId);
        Task<List<JobRpt>> GetAllClientJobs(string clientId, string jobYear);
        Task<List<Payment>> GetBatchPayments(IPaymentReportCriteria grc);
        Task<List<BatchRpt>> GetBatchRpt(IPaymentReportCriteria grc);
        Task<List<Payment>> GetPayments(IPaymentReportCriteria crc);
        Task<List<JobRpt>> GetJob(int jobNumber);
        Task<List<BatchRpt>> GetJobBatchRpt(int jobNumber);
        Task<List<Payment>> GetJobPayments(IPaymentReportCriteria grc);
        Task<List<Payment>> GetJobPayments(int jobNumber);
        Task<List<JobRpt>> GetJobRpt(IPaymentReportCriteria grc);
        Task<List<Payment>> GetOpenPayments(IPaymentReportCriteria prc);
        Task<List<Payment>> GetPayeePayments(string lastName, string firstName);
        Task<List<Payment>> GetPaymentReconcileReport(IPaymentReportCriteria grc);
        Task<List<PositivePayItem>> GetPositivePayRpt(IPaymentReportCriteria grc);
        Task<List<Payment>> GetUnclearedCheckReport(IPaymentReportCriteria grc);
        Task<List<VoidedPayment>> GetVoidedPayments(IPaymentReportCriteria crc);
    }
}