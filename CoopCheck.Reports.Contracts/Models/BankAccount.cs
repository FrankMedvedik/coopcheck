﻿using System;

namespace CoopCheck.Reports.Contracts.Models
{
    public class BankAccount
    {
        public string account_dscr { get; set; }
        public int account_id { get; set; }
        public string account_name { get; set; }
        public string account_number { get; set; }
        public string account_type { get; set; }
        public decimal? balance { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDefault { get; set; }
        public decimal? last_rec_balance { get; set; }
        public DateTime? last_rec_date { get; set; }
    }
}
