using CoopCheck.Library;
using CoopCheck.WPF.Models;
using DataClean;

namespace CoopCheck.WPF.Converters
{
    class VoucherImportConverter 
    {
        public static InputStreetAddress ToInputStreetAddress(VoucherImport v)
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
            return n;
        }

        public static VoucherEdit ToVoucherEdit(VoucherImport v)
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = v.AddressLine1;
            n.AddressLine2 = v.AddressLine2;
            n.Amount = v.Amount;
            n.Company = v.Company;
            //n.Country = Country;
            n.EmailAddress = v.EmailAddress;
            n.First = v.First;
            n.Last = v.Last;
            n.Middle = v.Middle;
            n.Municipality = v.Municipality;
            n.NamePrefix = v.NamePrefix;
            n.PersonId = v.PersonId;
            n.PhoneNumber = v.PhoneNumber;
            n.PostalCode = v.PostalCode;
            n.Region = v.Region;
            n.Suffix = v.Suffix;
            n.Title = v.Title;
            return n;
        }
    }
    
}
