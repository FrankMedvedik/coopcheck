using System;
using DataClean.Interfaces;
using DataClean.Services;

namespace DataClean.Models
{
    public class InputStreetAddress : IInputStreetAddress
    {

        public int ID {get; set;}
        public string RecordID { get; set; } 
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public override string ToString()
        {
            return string.Concat(FirstName, LastName, FullName, AddressLine1, " ", AddressLine2, City, State, PostalCode);

        }

     
    }
}