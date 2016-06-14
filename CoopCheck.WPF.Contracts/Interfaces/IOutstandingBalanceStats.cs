using System;

namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IOutstandingBalanceStats
    {
        String AccountName { get; set; }
        String OutstandingBalance { get; set; }
        DateTime AsOfDate { get; set; }
        String ItemCount { get; set; }
        String PreparedBy { get; set; }
    }
}