using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface Ibank_account
    {
        string account_dscr { get; set; }
        int account_id { get; set; }
        string account_name { get; set; }
        string account_number { get; set; }
        string account_type { get; set; }
        decimal? balance { get; set; }
        bool? IsActive { get; set; }
        bool? IsDefault { get; set; }
        decimal? last_rec_balance { get; set; }
        DateTime? last_rec_date { get; set; }
    }
}