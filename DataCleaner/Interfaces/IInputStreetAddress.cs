using System;

namespace DataClean.Interfaces
{
    public interface IInputStreetAddress
    {
        int ID { get; } // this is the unq composite hash for the address
        string  RecordID { get; set; } // this is the unq key for the entire data record since address may appear more than once
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string CompanyName { get; set; }
        string Country { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string FullName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string PostalCode { get; set; }
        string State { get; set; }
        string ToString();
    }
}