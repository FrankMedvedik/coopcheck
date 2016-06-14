using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IvwPositivePay
    {
        string account_number { get; set; }
        DateTime? check_date { get; set; }
        string check_num { get; set; }
        string payee { get; set; }
        string rpt { get; set; }
        decimal? tran_amount { get; set; }
    }
}