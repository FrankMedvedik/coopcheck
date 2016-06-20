using System;
using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class OutstandingBalanceStats : IOutstandingBalanceStats
    {
        public String AccountName { get; set; }
        public String OutstandingBalance { get; set; }
        public DateTime AsOfDate { get; set; }
        public String ItemCount { get; set; }
        public String PreparedBy { get; set; }

    }
}
