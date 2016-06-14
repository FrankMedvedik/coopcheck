using System;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Models
{
    public class UnclearedBatch :IvwUnclearedBatch
    {
        public DateTime? batch_date { get; set; }
        public int batch_num { get; set; }
        public int? job_num { get; set; }
        public DateTime? pay_date { get; set; }
        public decimal? total_amount { get; set; }
        public int? voucher_cnt { get; set; }
    }
}