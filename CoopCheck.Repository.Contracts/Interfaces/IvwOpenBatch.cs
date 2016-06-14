using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IvwOpenBatch
    {
        bool BadBatch { get; }
        DateTime? batch_date { get; set; }
        string batch_dscr { get; set; }
        int batch_num { get; set; }
        string clientid { get; set; }
        string clientname { get; set; }
        DateTime? created { get; set; }
        bool IsSwiftBatch { get; }
        string jobname { get; set; }
        int? jobnum { get; set; }
        DateTime? pay_date { get; set; }
        string projmgr { get; set; }
        decimal? total_amount { get; set; }
        DateTime? updated { get; set; }
        int? voucher_cnt { get; set; }
    }
}