using System;
using CoopCheck.Repository.Contracts.Interfaces;
using CoopCheck.WPF.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class OpenBatch : IvwOpenBatch
    {
        public bool BadBatch { get; }
        public DateTime? batch_date { get; set; }
        public string batch_dscr { get; set; }
        public int batch_num { get; set; }
        public string clientid { get; set; }
        public string clientname { get; set; }
        public DateTime? created { get; set; }
        public bool IsSwiftBatch { get; }
        public string jobname { get; set; }
        public int? jobnum { get; set; }
        public DateTime? pay_date { get; set; }
        public string projmgr { get; set; }
        public decimal? total_amount { get; set; }
        public DateTime? updated { get; set; }
        public int? voucher_cnt { get; set; }
    }
}
