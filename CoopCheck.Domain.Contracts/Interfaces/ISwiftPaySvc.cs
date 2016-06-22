namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface ISwiftPaySvc
    {
        void PayBatch(int batchNum, string email);
        void VoidBatch(int batchNum, string email);
    }
}