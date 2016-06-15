using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IRptSvc
    {
        Task<List<IvwPayment>> GetAllBatchHonorariaPayments(int batchNum);
        Task<List<IvwJobRpt>> GetAllClientJobs(string clientId);
        Task<List<IvwJobRpt>> GetAllClientJobs(string clientId, string jobYear);
        Task<List<IvwPayment>> GetBatchHonorariaPayments(IPaymentReportCriteria grc);
        Task<List<IvwBatchRpt>> GetBatchRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetHonorariaPayments(IPaymentReportCriteria crc);
        Task<List<IvwJobRpt>> GetJob(int jobNumber);
        Task<List<IvwBatchRpt>> GetJobBatchRpt(int jobNumber);
        Task<List<IvwPayment>> GetJobHonorariaPayments(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetJobHonorariaPayments(int jobNumber);
        Task<List<IvwJobRpt>> GetJobRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetOpenPayments(IPaymentReportCriteria prc);
        Task<List<IvwPayment>> GetPayeeHonorariaPayments(string lastName, string firstName);
        Task<List<IvwPayment>> GetPaymentReconcileReport(IPaymentReportCriteria grc);
        Task<List<IvwPositivePay>> GetPositivePayRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetUnclearedCheckReport(IPaymentReportCriteria grc);
        Task<List<IvwVoidedPayment>> GetVoidedHonorariaPayments(IPaymentReportCriteria crc);
    }
}