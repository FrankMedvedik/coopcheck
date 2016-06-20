using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class PaymentMethod : IPaymentMethod
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
