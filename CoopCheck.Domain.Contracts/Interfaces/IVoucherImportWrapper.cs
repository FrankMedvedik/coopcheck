using System;
using System.Collections;
using System.ComponentModel;
using DataClean.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IVoucherImportWrapper
    {
        int ID { get; set; }
        Boolean OkMailingAddress { get; }
        Boolean OkEmailAddress { get; }
        Boolean OkPhone { get; }
        string RecordID { get; set; }
        IOutputStreetAddress AltAddress { get; set; }
        string AltAddressText { get; }
        bool AddressOk { get; set; }
        decimal? Amount { get; set; }
        string PersonId { get; set; }
        DateTime? DataCleanDate { get; set; }
        bool HasBeenDataCleaned { get; }
        string NamePrefix { get; set; }
        string First { get; set; }
        string Middle { get; set; }
        string Last { get; set; }
        string Suffix { get; set; }
        string Title { get; set; }
        string Company { get; set; }
        string AddressLine1 { get; set; }
        string AltAddressLine1 { get; set; }
        string AltAddressLine2 { get; set; }
        string AddressLine2 { get; set; }
        string Municipality { get; set; }
        string AltMunicipality { get; set; }
        string Region { get; set; }
        string AltRegion { get; set; }
        string PostalCode { get; set; }
        string AltPostalCode { get; set; }
        string Country { get; set; }
        string PhoneNumber { get; set; }
        string EmailAddress { get; set; }
        string Updated { get; set; }
        string JobNumber { get; set; }
        IVoucherImport Model { get; }
        bool IsValid { get; }
        bool HasErrors { get; }
        IEnumerable GetErrors(string propertyName);
        event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        event PropertyChangedEventHandler PropertyChanged;
    }
}