﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoopCheck.Repository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CoopCheckEntities : DbContext
    {
        public CoopCheckEntities()
            : base("name=CoopCheckEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<batch> batches { get; set; }
        public virtual DbSet<check_tran> check_tran { get; set; }
        public virtual DbSet<OpenBatch> OpenBatches { get; set; }
        public virtual DbSet<voucher> vouchers { get; set; }
        public virtual DbSet<bank_account> bank_accounts { get; set; }
        public virtual DbSet<vwPayment> vwPayments { get; set; }
        public virtual DbSet<vwBatchRpt> vwBatchRpts { get; set; }
        public virtual DbSet<vwJobRpt> vwJobRpt { get; set; }
        public virtual DbSet<vwCheck> vwCheck { get; set; }
        public virtual DbSet<vwPositivePay> vwPositivePay { get; set; }
        public virtual DbSet<vwBasicPayment> vwBasicPayments { get; set; }
    
        public virtual ObjectResult<dsa_GetCheckingAccounts_Result> dsa_GetCheckingAccounts()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetCheckingAccounts_Result>("dsa_GetCheckingAccounts");
        }
    
        public virtual ObjectResult<dsa_GetCheckList_Result> dsa_GetCheckList(string person_id)
        {
            var person_idParameter = person_id != null ?
                new ObjectParameter("person_id", person_id) :
                new ObjectParameter("person_id", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetCheckList_Result>("dsa_GetCheckList", person_idParameter);
        }
    
        public virtual ObjectResult<dsa_GetChecks_Result> dsa_GetChecks(Nullable<int> account_id, Nullable<int> batch_num, string starting_check_num, string ending_check_num, string starting_check_date, string ending_check_date, Nullable<int> job_num, string last_name)
        {
            var account_idParameter = account_id.HasValue ?
                new ObjectParameter("account_id", account_id) :
                new ObjectParameter("account_id", typeof(int));
    
            var batch_numParameter = batch_num.HasValue ?
                new ObjectParameter("batch_num", batch_num) :
                new ObjectParameter("batch_num", typeof(int));
    
            var starting_check_numParameter = starting_check_num != null ?
                new ObjectParameter("starting_check_num", starting_check_num) :
                new ObjectParameter("starting_check_num", typeof(string));
    
            var ending_check_numParameter = ending_check_num != null ?
                new ObjectParameter("ending_check_num", ending_check_num) :
                new ObjectParameter("ending_check_num", typeof(string));
    
            var starting_check_dateParameter = starting_check_date != null ?
                new ObjectParameter("starting_check_date", starting_check_date) :
                new ObjectParameter("starting_check_date", typeof(string));
    
            var ending_check_dateParameter = ending_check_date != null ?
                new ObjectParameter("ending_check_date", ending_check_date) :
                new ObjectParameter("ending_check_date", typeof(string));
    
            var job_numParameter = job_num.HasValue ?
                new ObjectParameter("job_num", job_num) :
                new ObjectParameter("job_num", typeof(int));
    
            var last_nameParameter = last_name != null ?
                new ObjectParameter("last_name", last_name) :
                new ObjectParameter("last_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetChecks_Result>("dsa_GetChecks", account_idParameter, batch_numParameter, starting_check_numParameter, ending_check_numParameter, starting_check_dateParameter, ending_check_dateParameter, job_numParameter, last_nameParameter);
        }
    
        public virtual ObjectResult<dsa_GetOpenBatch_Result> dsa_GetOpenBatch()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetOpenBatch_Result>("dsa_GetOpenBatch");
        }
    
        public virtual ObjectResult<dsa_GetOutstandingChecks_Result> dsa_GetOutstandingChecks(Nullable<int> account_id)
        {
            var account_idParameter = account_id.HasValue ?
                new ObjectParameter("account_id", account_id) :
                new ObjectParameter("account_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetOutstandingChecks_Result>("dsa_GetOutstandingChecks", account_idParameter);
        }
    
        public virtual ObjectResult<dsa_GetReconcilationReport_Result> dsa_GetReconcilationReport(Nullable<int> account_id, Nullable<System.DateTime> rec_date)
        {
            var account_idParameter = account_id.HasValue ?
                new ObjectParameter("account_id", account_id) :
                new ObjectParameter("account_id", typeof(int));
    
            var rec_dateParameter = rec_date.HasValue ?
                new ObjectParameter("rec_date", rec_date) :
                new ObjectParameter("rec_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetReconcilationReport_Result>("dsa_GetReconcilationReport", account_idParameter, rec_dateParameter);
        }
    
        public virtual ObjectResult<dsa_GetSwiftBatch_Result> dsa_GetSwiftBatch(Nullable<int> batch_num)
        {
            var batch_numParameter = batch_num.HasValue ?
                new ObjectParameter("batch_num", batch_num) :
                new ObjectParameter("batch_num", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetSwiftBatch_Result>("dsa_GetSwiftBatch", batch_numParameter);
        }
    
        public virtual ObjectResult<dsa_GetVoucher_Result> dsa_GetVoucher(Nullable<int> tran_id)
        {
            var tran_idParameter = tran_id.HasValue ?
                new ObjectParameter("tran_id", tran_id) :
                new ObjectParameter("tran_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<dsa_GetVoucher_Result>("dsa_GetVoucher", tran_idParameter);
        }
    
        public virtual ObjectResult<getbatchRpt_Result> getbatchRpt(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getbatchRpt_Result>("getbatchRpt", startDateParameter, endDateParameter);
        }
    }
}
