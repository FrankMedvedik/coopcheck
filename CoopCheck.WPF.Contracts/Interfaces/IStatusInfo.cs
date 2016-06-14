namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IStatusInfo
    {
        bool IsBusy { get; set; }
        string ErrorMessage { get; set; }
        string StatusMessage { get; set; }
        string whoami { get; }
    }
}