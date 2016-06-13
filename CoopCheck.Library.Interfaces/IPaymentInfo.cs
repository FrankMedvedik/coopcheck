using Csla;

namespace CoopCheck.Library.Interfaces
{
    public interface IPaymentInfo
    {
        string Address1 { get; }
        string Address2 { get; }
        decimal Amount { get; }
        int BatchNum { get; }
        SmartDate CheckDate { get; }
        string CheckNum { get; }
        string Company { get; }
        bool Completed { get; }
        string Country { get; }
        string Email { get; }
        string FirstName { get; }
        string FullName { get; }
        int Id { get; }
        int JobNum { get; }
        string LastName { get; }
        string MarketingResearchMessage { get; }
        string MiddleName { get; }
        string Municipality { get; }
        string NameSuffix { get; }
        SmartDate PayDate { get; }
        string PersonId { get; }
        string PhoneNumber { get; }
        string PostalCode { get; }
        string Prefix { get; }
        string Region { get; }
        string StudyTopic { get; }
        string ThankYou1 { get; }
        string ThankYou2 { get; }
        string Title { get; }
    }
}