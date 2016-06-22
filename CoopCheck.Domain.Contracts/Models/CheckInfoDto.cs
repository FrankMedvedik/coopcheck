using CoopCheck.Domain.Contracts.Interfaces;
using Csla;


namespace CoopCheck.Domain.Contracts.Models
{

    public class CheckInfoDto : ICheckInfoDto
    {
        public int? AccountId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public decimal? Amount { get; set; }
        public int? BatchNum { get; set; }
        public string CheckNum { get; set; }
        public decimal? ClearedAmount { get; set; }
        public SmartDate ClearedDate { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public SmartDate Date { get; set; }
        public string Email { get; set; }
        public string First { get; set; }
        public int Id { get; set; }
        public bool IsCleared { get; set; }
        public bool IsPrinted { get; set; }
        public string Last { get; set; }
        public string Middle { get; set; }
        public string Municipality { get; set; }
        public string PersonId { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Prefix { get; set; }
        public string Region { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public SmartDate Updated { get; set; }
    }
}
