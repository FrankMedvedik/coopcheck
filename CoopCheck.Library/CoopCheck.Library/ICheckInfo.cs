namespace CoopCheck.Library
{
    public interface ICheckInfo
    {
        int? AccountId { get; }
        string AddressLine1 { get; }
        string AddressLine2 { get; }
        decimal? Amount { get; }
        int? BatchNum { get; }
        string CheckNum { get; }
        decimal? ClearedAmount { get; }
        string ClearedDate { get; }
        string Company { get; }
        string Country { get; }
        string Date { get; }
        string Email { get; }
        string First { get; }
        int Id { get; }
        bool IsCleared { get; }
        bool IsPrinted { get; }
        string Last { get; }
        string Middle { get; }
        string Municipality { get; }
        string PersonId { get; }
        string PhoneNumber { get; }
        string PostalCode { get; }
        string Prefix { get; }
        string Region { get; }
        string Suffix { get; }
        string Title { get; }
        string Updated { get; }
    }
}