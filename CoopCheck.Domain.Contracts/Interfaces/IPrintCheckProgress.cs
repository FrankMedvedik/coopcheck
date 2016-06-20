using CoopCheck.Domain.Contracts.Models;

namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IPrintCheckProgress
    {
        int CurrentCheckNum { get; set; }
        decimal ProgressPercentage { get; set; }
        StatusInfo Status { get; set; }
    }
}