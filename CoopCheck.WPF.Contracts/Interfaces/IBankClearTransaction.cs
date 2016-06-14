using System;

namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IBankClearTransaction
    {
        string AccountDesignator { get; set; }
        decimal Amount { get; }
        string CRDR { get; set; }
        string Description { get; set; }
        DateTime PostDate { get; set; }
        decimal RawAmount { get; set; }
        string SerialNumber { get; set; }
    }
}