using DataClean.Contracts.Interfaces;


namespace DataClean.Personator
{
    public partial class RequestRecord 
    {
        public RequestRecord(IInputStreetAddress a)
        {
            RecordID = a.RecordID + "|" + a.ID;
            AddressLine1 = a.AddressLine1;
            AddressLine2 = a.AddressLine2;
            City = a.City;
            CompanyName = a.CompanyName;
            Country = a.Country;
            EmailAddress = a.EmailAddress;
            FirstName = a.FirstName;
            FullName = a.FullName;
            LastName = a.LastName;
            PhoneNumber = a.PhoneNumber;
            PostalCode = a.PostalCode;
            State = a.State;
             
        }
    }

}
