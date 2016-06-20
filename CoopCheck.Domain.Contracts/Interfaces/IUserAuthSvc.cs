namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IUserAuthSvc
    {
        bool CanRead(string userName);
        bool CanWrite(string userName);
    }
}