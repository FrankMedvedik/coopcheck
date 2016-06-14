using System;
using CoopCheck.Repository.Contracts.Interfaces;


namespace CoopCheck.WPF.Models
{
    public  class JobRpt : IvwJobRpt
    {
        public int? batch_cnt { get; set; }
        public decimal? cleared_amount { get; set; }
        public int? cleared_cnt { get; set; }
        public string clientid { get; set; }
        public string clientname { get; set; }
        public DateTime? first_batch_date { get; set; }
        public DateTime? first_check_date { get; set; }
        public string first_check_num { get; set; }
        public DateTime? first_pay_date { get; set; }
        public string jobname { get; set; }
        public int job_num { get; set; }
        public string job_year { get; set; }
        public DateTime? last_batch_date { get; set; }
        public DateTime? last_check_date { get; set; }
        public string last_check_num { get; set; }
        public DateTime? last_pay_date { get; set; }
        public decimal? open_amount { get; set; }
        public int? open_cnt { get; set; }
        public string projmgr { get; set; }
        public decimal? total_amount { get; set; }
        public int? total_cnt { get; set; }
    }
}