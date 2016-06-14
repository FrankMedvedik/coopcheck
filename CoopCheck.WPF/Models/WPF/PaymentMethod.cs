using CoopCheck.WPF.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class PaymentMethod : IPaymentMethod
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
