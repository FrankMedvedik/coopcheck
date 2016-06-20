using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class PrintCheckProgress : IPrintCheckProgress
    {
        public decimal ProgressPercentage { get; set; }
        public int CurrentCheckNum { get; set; }
        public StatusInfo Status { get; set; }
    }
}