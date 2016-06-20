using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Models.DataClean;
using CoopCheck.Domain.Contracts.Wrappers;
using DataClean.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Converters
{
    public class VoucherImportWrapperConverter 
    {
        public static VoucherExcelExport ToVoucherExcelExport(VoucherImportWrapper e)
        {
            var n = new VoucherExcelExport();
            n.AddressLine1 = e.AddressLine1;
            n.AddressLine2 = e.AddressLine2;
            n.Amount = e.Amount.GetValueOrDefault(0);
            n.Company = e.Company;
            n.Country = e.Country;
            n.EmailAddress = e.EmailAddress;
            n.First = e.First;
            n.JobNumber = e.JobNumber;
            n.Last = e.Last;
            n.Municipality = e.Municipality;
            n.PhoneNumber = e.PhoneNumber;
            n.PostalCode = e.PostalCode;
            n.Region = e.Region;
            n.OkComplete = e.AltAddress.OkComplete;
            n.OkMailingAddress = e.AltAddress.OkMailingAddress;
            n.OkEmailAddress = e.AltAddress.OkEmailAddress;
            n.OkPhone = e.AltAddress.OkPhone;
            n.SuggestedAddress = n.SuggestedAddress;
            return n;
        }
        public static VoucherImport ToVoucherImport(VoucherImportWrapper v)
        {
            var n = new VoucherImport();
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
            n.JobNumber = v.JobNumber;
            n.Amount = v.Amount;
            n.RecordID = v.RecordID;
            return n;
        }
        public static IInputStreetAddress ToInputStreetAddress(IVoucherImportWrapper v)
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
            return (IInputStreetAddress) n;
        }


    }
}
