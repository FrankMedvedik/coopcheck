namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IPaymentMethod
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}