using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
using DataClean;
using DataClean.Models;

namespace CoopCheck.WPF.Converters
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

            return n;
        }
        public static InputStreetAddress ToInputStreetAddress(VoucherImportWrapper v)
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
            return n;
        }


    }
}
