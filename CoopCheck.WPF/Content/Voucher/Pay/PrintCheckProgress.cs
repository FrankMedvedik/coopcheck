using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public class PrintCheckProgress
    {
        public decimal ProgressPercentage { get; set; }
        public int CurrentCheckNum { get; set; }
        public StatusInfo Status { get; set; }
    }
}