using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IvwUnclearedBatch
    {
        DateTime? batch_date { get; set; }
        int batch_num { get; set; }
        int? job_num { get; set; }
        DateTime? pay_date { get; set; }
        decimal? total_amount { get; set; }
        int? voucher_cnt { get; set; }
    }
}