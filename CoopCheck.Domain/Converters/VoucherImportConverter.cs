using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Library;
using DataClean.Contracts.Models;

namespace CoopCheck.Domain.Converters
{
    public static class VoucherImportConverter 
    {
        public static InputStreetAddress ToInputStreetAddress(IVoucherImport v)
        {
            var n = new InputStreetAddress();
            n.AddressLine1 = v.AddressLine1;
            n.AddressLine2 = v.AddressLine2;
            n.CompanyName = v.Company;
            n.Country = v.Country;
            n.EmailAddress = v.EmailAddress;
            n.FirstName = v.First;
            n.LastName = v.Last;
            n.City = v.Municipality;
            n.PhoneNumber = v.PhoneNumber;
            n.PostalCode = v.PostalCode;
            n.State = v.Region;
            n.ID = v.ID;
            n.RecordID = v.RecordID;
            return n;
        }

        public static VoucherEdit ToVoucherEdit(VoucherImport v)
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = !string.IsNullOrEmpty(v.AddressLine1) ? v.AddressLine1.ToUpper():v.AddressLine1;
            n.AddressLine2 = !string.IsNullOrEmpty(v.AddressLine2) ? v.AddressLine2.ToUpper() : v.AddressLine2;
            n.Amount = v.Amount;
            n.Company = !string.IsNullOrEmpty(v.Company) ? v.Company.ToUpper(): v.Company;
            //n.Country = Country;
            n.EmailAddress = v.EmailAddress;
            n.First = !string.IsNullOrEmpty(v.First) ?  v.First.ToUpper(): v.First;
            n.Last = !string.IsNullOrEmpty(v.Last) ? v.Last.ToUpper() : v.Last;
            n.Middle = !string.IsNullOrEmpty(v.Middle) ? v.Middle.ToUpper(): v.Middle;
            n.Municipality = !string.IsNullOrEmpty(v.Municipality) ? v.Municipality.ToUpper(): v.Municipality;
            n.NamePrefix = !string.IsNullOrEmpty(v.NamePrefix) ? v.NamePrefix.ToUpper(): v.NamePrefix;
            n.PersonId = v.PersonId;
            n.PhoneNumber = v.PhoneNumber;
            n.PostalCode = v.PostalCode;
            n.Region = !string.IsNullOrEmpty(v.Region) ? v.Region.ToUpper() : v.Region;
            n.Suffix = !string.IsNullOrEmpty(v.Suffix)? v.Suffix.ToUpper(): v.Suffix;
            n.Title = !string.IsNullOrEmpty(v.Title) ? v.Title.ToUpper() : v.Title;
            return n;
        }
    }
}
