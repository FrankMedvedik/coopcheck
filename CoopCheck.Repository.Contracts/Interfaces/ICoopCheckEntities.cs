using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace CoopCheck.Repository.Contracts.Interfaces
{
        public interface ICoopCheckEntities
        {
            IDbSet<Ibatch> batches { get; set; }
            IDbSet<Icheck_tran> check_trans { get; set; }
            IDbSet<Ivoucher> vouchers { get; set; }
            IDbSet<IvwCheck> vwChecks { get; set; }
            IDbSet<IvwPositivePay> vwPositivePays { get; set; }
            IDbSet<IvwVoidedPayment> vwVoidedPayments { get; set; }
            IDbSet<IvwPayment> vwPayments { get; set; }
            IDbSet<Ibank_account> bank_accounts { get; set; }
            IDbSet<IvwUnclearedBatch> vwUnclearedBatches { get; set; }
            IDbSet<ICoopCheckClient> CoopCheckClients { get; set; }
            IDbSet<ICoopCheckJobLog> CoopCheckJobLogs { get; set; }
            IDbSet<IvwClientJobBatch> vwClientJobBatches { get; set; }
            IDbSet<IvwJobRpt> vwJobRpts { get; set; }
            IDbSet<IJobLog> JobLogs { get; set; }
            IDbSet<IvwBatchRpt> vwBatchRpts { get; set; }
            IDbSet<IvwOpenBatch> vwOpenBatches { get; set; }
            ObjectResult<IvwJobRpt> GetJobPaymentsReport(Nullable<int> account_id, Nullable<System.DateTime> start_date,
                Nullable<System.DateTime> end_date);


            ObjectResult<IvwJobRpt> GetJobPaymentsReport(Nullable<int> account_id, Nullable<System.DateTime> start_date,
                Nullable<System.DateTime> end_date, MergeOption mergeOption);

            ObjectResult<IvwBatchRpt> GetBatchPaymentReport(Nullable<int> account_id,
                Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date);

            ObjectResult<IvwBatchRpt> GetBatchPaymentReport(Nullable<int> account_id,
                Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date, MergeOption mergeOption);
    }
}
