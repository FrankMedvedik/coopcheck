using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IvwBatchRpt
    {
        int? account_id { get; set; }
        string account_name { get; set; }
        bool BadBatch { get; }
        decimal? batch_amount { get; set; }
        DateTime? batch_date { get; set; }
        string batch_dscr { get; set; }
        int batch_num { get; set; }
        decimal? cleared_amount { get; set; }
        int? cleared_cnt { get; set; }
        string clientid { get; set; }
        string clientname { get; set; }
        string first_check_num { get; set; }
        string jobname { get; set; }
        int? job_num { get; set; }
        string job_year { get; set; }
        string last_check_num { get; set; }
        decimal? open_amount { get; set; }
        int? open_cnt { get; set; }
        string payment_type { get; set; }
        DateTime? pay_date { get; set; }
        string projmgr { get; set; }
        decimal? total_amount { get; set; }
        int? total_cnt { get; set; }
    }
}