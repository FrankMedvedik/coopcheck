using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IvwJobRpt
    {
        int? batch_cnt { get; set; }
        decimal? cleared_amount { get; set; }
        int? cleared_cnt { get; set; }
        string clientid { get; set; }
        string clientname { get; set; }
        DateTime? first_batch_date { get; set; }
        DateTime? first_check_date { get; set; }
        string first_check_num { get; set; }
        DateTime? first_pay_date { get; set; }
        string jobname { get; set; }
        int job_num { get; set; }
        string job_year { get; set; }
        DateTime? last_batch_date { get; set; }
        DateTime? last_check_date { get; set; }
        string last_check_num { get; set; }
        DateTime? last_pay_date { get; set; }
        decimal? open_amount { get; set; }
        int? open_cnt { get; set; }
        string projmgr { get; set; }
        decimal? total_amount { get; set; }
        int? total_cnt { get; set; }
    }
}