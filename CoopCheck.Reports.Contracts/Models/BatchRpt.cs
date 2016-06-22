using System;

namespace CoopCheck.Reports.Contracts.Models
{
    public class BatchRpt 
    {
        public int? account_id { get; set; }
        public string account_name { get; set; }
        public bool BadBatch { get; }
        public decimal? batch_amount { get; set; }
        public DateTime? batch_date { get; set; }
        public string batch_dscr { get; set; }
        public int batch_num { get; set; }
        public decimal? cleared_amount { get; set; }
        public int? cleared_cnt { get; set; }
        public string clientid { get; set; }
        public string clientname { get; set; }
        public string first_check_num { get; set; }
        public string jobname { get; set; }
        public int? job_num { get; set; }
        public string job_year { get; set; }
        public string last_check_num { get; set; }
        public decimal? open_amount { get; set; }
        public int? open_cnt { get; set; }
        public string payment_type { get; set; }
        public DateTime? pay_date { get; set; }
        public string projmgr { get; set; }
        public decimal? total_amount { get; set; }
        public int? total_cnt { get; set; }
    }
}