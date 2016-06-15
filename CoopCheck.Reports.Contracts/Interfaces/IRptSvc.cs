﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IRptSvc
    {
        Task<List<IvwPayment>> GetAllBatchPayments(int batchNum);
        Task<List<IvwJobRpt>> GetAllClientJobs(string clientId);
        Task<List<IvwJobRpt>> GetAllClientJobs(string clientId, string jobYear);
        Task<List<IvwPayment>> GetBatchPayments(IPaymentReportCriteria grc);
        Task<List<IvwBatchRpt>> GetBatchRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetPayments(IPaymentReportCriteria crc);
        Task<List<IvwJobRpt>> GetJob(int jobNumber);
        Task<List<IvwBatchRpt>> GetJobBatchRpt(int jobNumber);
        Task<List<IvwPayment>> GetJobPayments(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetJobPayments(int jobNumber);
        Task<List<IvwJobRpt>> GetJobRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetOpenPayments(IPaymentReportCriteria prc);
        Task<List<IvwPayment>> GetPayeePayments(string lastName, string firstName);
        Task<List<IvwPayment>> GetPaymentReconcileReport(IPaymentReportCriteria grc);
        Task<List<IvwPositivePay>> GetPositivePayRpt(IPaymentReportCriteria grc);
        Task<List<IvwPayment>> GetUnclearedCheckReport(IPaymentReportCriteria grc);
        Task<List<IvwVoidedPayment>> GetVoidedPayments(IPaymentReportCriteria crc);
    }
}