using System;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using DataClean;
using DataClean.Models;
using Reckner.WPF.Extensions;

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
            n.ID = v.ID;
            n.RecordID = v.RecordID;
            return n;
        }


        public static VoucherEdit ToVoucherEdit(VoucherImport v)
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = v.AddressLine1?.ToUpper().Truncate(49);
            n.AddressLine2 = v.AddressLine2?.ToUpper().Truncate(49);
            n.Amount = v.Amount;
            n.Company = v.Company?.ToUpper().Truncate(34);
            //n.Country = Country;
            n.EmailAddress = v.EmailAddress?.Truncate(45);
            n.First = v.First?.ToUpper().Truncate(19);
            n.Last = v.Last?.ToUpper().Truncate( 19);
            n.Middle = v.Middle?.ToUpper().Truncate(19);
            n.Municipality = v.Municipality?.ToUpper().Truncate(33);
            n.NamePrefix = v?.NamePrefix?.ToUpper().Truncate(4);
            n.PersonId = v.PersonId;
            n.PhoneNumber = v.PhoneNumber;
            n.PostalCode = v.PostalCode;
            n.Region = v.Region?.ToUpper().Truncate(34);
            n.Suffix = v.Suffix?.ToUpper().Truncate(14);
            n.Title = v.Title?.ToUpper().Truncate(34);
            return n;
        }

        public static voucher toVoucher(VoucherImport v)
        {
            var n = new voucher();
            n.address_1= v.AddressLine1?.ToUpper().Truncate(49);
            n.address_2 = v.AddressLine2?.ToUpper().Truncate(49);
            n.tran_amount = v.Amount;
            n.company = v.Company?.ToUpper().Truncate(34);
            //n.Country = Country;
            n.email = v.EmailAddress?.Truncate(45);
            n.first_name = v.First?.ToUpper().Truncate(19);
            n.last_name = v.Last?.ToUpper().Truncate(19);
            n.middle_name = v.Middle?.ToUpper().Truncate(19);
            n.municipality = v.Municipality?.ToUpper().Truncate(33);
            n.name_prefix = v?.NamePrefix?.ToUpper().Truncate(4);
            n.person_id = v.PersonId;
            n.phone_number = v.PhoneNumber;
            n.postal_code = v.PostalCode;
            n.region = v.Region?.ToUpper().Truncate(34);
            n.name_suffix = v.Suffix?.ToUpper().Truncate(14);
            n.title= v.Title?.ToUpper().Truncate(34);
            return n;

        }
    }
    
}
