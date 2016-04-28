using System;

namespace CoopCheck.WPF.Models
{
    public class OutstandingBalanceStats
    {
        public String AccountName { get; set; }
        public String OutstandingBalance { get; set; }
        public DateTime AsOfDate { get; set; }
        public String ItemCount { get; set; }
        public String PreparedBy { get; set; }

    }
}
