using System;

namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IVoucherExcelExport
    {
        string First { get; set; }
        string Last { get; set; }
        string Company { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Municipality { get; set; }
        string Region { get; set; }
        string PostalCode { get; set; }
        string Country { get; set; }
        string PhoneNumber { get; set; }
        string EmailAddress { get; set; }
        string JobNumber { get; set; }
        decimal Amount { get; set; }
        Boolean OkComplete { get; set; }
        Boolean OkMailingAddress { get; set; }
        Boolean OkEmailAddress { get; set; }
        Boolean OkPhone { get; set; }
        string SuggestedAddress { get; set; }
    }
}