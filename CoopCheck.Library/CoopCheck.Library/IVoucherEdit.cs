namespace CoopCheck.Library
{
    public interface IVoucherEdit
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        decimal? Amount { get; set; }
        string Company { get; set; }
        string Country { get; set; }
        string EmailAddress { get; set; }
        string First { get; set; }
        string FullName { get; }
        int Id { get; }
        string Last { get; set; }
        string Middle { get; set; }
        string Municipality { get; set; }
        string NamePrefix { get; set; }
        string PersonId { get; set; }
        string PhoneNumber { get; set; }
        string PostalCode { get; set; }
        string Region { get; set; }
        string Suffix { get; set; }
        string Title { get; set; }
        string Updated { get; set; }
    }
}