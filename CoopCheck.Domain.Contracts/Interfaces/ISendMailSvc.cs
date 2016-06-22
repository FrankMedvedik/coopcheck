namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface ISendMailSvc
    {
        void SendEmail(string to, string subject, string body);
        string uEmail(string username);
    }
}