using System.Data.Entity;

namespace CoopCheck.Repository.Contracts.Interfaces
{
        public interface ICoopCheckDbContext
        {
            IDbSet<Ibatch> batches { get; set; }
            IDbSet<Icheck_tran> check_trans { get; set; }
            IDbSet<Ivoucher> vouchers { get; set; }
            IDbSet<IvwCheck> vwChecks { get; set; }
            IDbSet<IvwPositivePay> vwPositivePay1 { get; set; }
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
        }
    }
