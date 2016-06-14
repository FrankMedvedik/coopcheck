namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IUserSecurityProfile
    {
        bool CanRead { get; set; }
        bool CanWrite { get; set; }
        string UserName { get; }
    }
}