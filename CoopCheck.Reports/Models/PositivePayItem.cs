using System;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Models
{
    public class PositivePayItem : IvwPositivePay
    {
        public string account_number { get; set; }
        public DateTime? check_date { get; set; }
        public string check_num { get; set; }
        public string payee { get; set; }
        public string rpt { get; set; }
        public decimal? tran_amount { get; set; }
    }
}
