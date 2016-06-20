using Csla;

namespace CoopCheck.DAL
{
    public interface ICheckInfoDto
    {
        int? AccountId { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        decimal? Amount { get; set; }
        int? BatchNum { get; set; }
        string CheckNum { get; set; }
        decimal? ClearedAmount { get; set; }
        SmartDate ClearedDate { get; set; }
        string Company { get; set; }
        string Country { get; set; }
        SmartDate Date { get; set; }
        string Email { get; set; }
        string First { get; set; }
        int Id { get; set; }
        bool IsCleared { get; set; }
        bool IsPrinted { get; set; }
        string Last { get; set; }
        string Middle { get; set; }
        string Municipality { get; set; }
        string PersonId { get; set; }
        string PhoneNumber { get; set; }
        string PostalCode { get; set; }
        string Prefix { get; set; }
        string Region { get; set; }
        string Suffix { get; set; }
        string Title { get; set; }
        SmartDate Updated { get; set; }
    }
}