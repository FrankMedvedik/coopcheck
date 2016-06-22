namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface ISvrBatchSwiftVoidCommand
    {
        void Execute(int batchNum, string email);
    }
}