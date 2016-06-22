namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface ISvrBatchSwiftFulfillCommand
    {
        void Execute(int batchNum, string email);
    }
}

