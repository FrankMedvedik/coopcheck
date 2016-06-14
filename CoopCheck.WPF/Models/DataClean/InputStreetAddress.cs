using DataClean.Contracts.Interfaces;


namespace CoopCheck.WPF.Models
{
    public class InputStreetAddress : IInputStreetAddress
    {
        public int ID { get; set; }
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
    }
}