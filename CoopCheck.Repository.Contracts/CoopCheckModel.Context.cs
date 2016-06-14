
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Repository.Contracts
{
    public partial class CoopCheckEntities : ICoopCheckDbContext
    {
        public IDbSet<Ibatch> batches { get; set; }
        public IDbSet<Icheck_tran> check_trans { get; set; }
        public IDbSet<Ivoucher> vouchers { get; set; }
        public IDbSet<IvwCheck> vwChecks { get; set; }
        public IDbSet<IvwPositivePay> vwPositivePay1 { get; set; }
        public IDbSet<IvwVoidedPayment> vwVoidedPayments { get; set; }
        public IDbSet<IvwPayment> vwPayments { get; set; }
        public IDbSet<Ibank_account> bank_accounts { get; set; }
        public IDbSet<IvwUnclearedBatch> vwUnclearedBatches { get; set; }
        public IDbSet<ICoopCheckClient> CoopCheckClients { get; set; }
        public IDbSet<ICoopCheckJobLog> CoopCheckJobLogs { get; set; }
        public IDbSet<IvwClientJobBatch> vwClientJobBatches { get; set; }
        public IDbSet<IvwJobRpt> vwJobRpts { get; set; }
        public IDbSet<IJobLog> JobLogs { get; set; }
        public IDbSet<IvwBatchRpt> vwBatchRpts { get; set; }
        public IDbSet<IvwOpenBatch> vwOpenBatches { get; set; }
    }
}
