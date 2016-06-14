namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IVoucherImport
    {
        string RecordID { get; set; }
        int ID { get; set; }
        decimal? Amount { get; set; }
        string PersonId { get; set; }
        string NamePrefix { get; set; }
        string First { get; set; }
        string Middle { get; set; }
        string Last { get; set; }
        string Suffix { get; set; }
        string Title { get; set; }
        string Company { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Municipality { get; set; }
        string Region { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string PhoneNumber { get; set; }
        string EmailAddress { get; set; }
        string Updated { get; set; }
        string JobNumber { get; set; }
        bool AddressOk { get; set; }
        IVoucherImport GetNewWithDefaults();
    }
}