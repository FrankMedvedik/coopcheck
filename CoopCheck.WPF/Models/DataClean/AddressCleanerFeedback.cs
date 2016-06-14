using DataClean.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class AddressCleanerFeedback : IParseResult
    {
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public bool AlternateAddressExists { get; set; }
        public string LongDescription { get; set; }
        public string Type { get; set; }
    }
}
