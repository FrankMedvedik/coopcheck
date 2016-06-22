using System;
using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Wrappers;
using CoopCheck.Library;
using CoopCheck.Library.Contracts.Interfaces;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;

namespace CoopCheck.Domain.Contracts.Converters
{
    public class DataCleanEventConverter 
    {
        public static IVoucherImportWrapper ToVoucherImportWrapper(IDataCleanEvent e)
        {
            var n = new VoucherImport();
            n.AddressLine1 = e.Input.AddressLine1;
            n.AddressLine2 = e.Input.AddressLine2;
            n.Company = e.Input.CompanyName;
            n.Country = e.Input.Country;
            n.EmailAddress = e.Input.EmailAddress;
            n.First = e.Input.FirstName;
            n.Last = e.Input.LastName;
            n.Municipality = e.Input.City;
            n.PhoneNumber = e.Input.PhoneNumber;
            n.PostalCode = e.Input.PostalCode;
            n.Region = e.Input.State;
            n.RecordID = e.Input.RecordID;
            n.ID = e.Input.ID;

            var v = new VoucherImportWrapper((IVoucherImport) n);
            v.DataCleanDate = e.DataCleanDate;
            v.AltAddress =  (OutputStreetAddress) e.Output;
            return v;
        }

        public static IVoucherEdit ToVoucherEdit(IVoucherImport v)
        {
            VoucherEdit n = VoucherEdit.NewVoucherEdit();
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
            return (IVoucherEdit) n;
        }

        public static IVoucherImportWrapper ToVoucherImportWrapper(IDataCleanEvent e, IVoucherImportWrapper src)
        {
            VoucherImportWrapper tgt = null;

            try
            {
                tgt = (VoucherImportWrapper) ToVoucherImportWrapper(e);
                tgt.JobNumber = src.JobNumber;
                tgt.Amount = src.Amount;
                return tgt;
            }
            catch (Exception x)
            {

                throw new Exception(x.Message + " - No matching wrapper for event tgtId =" + tgt.ID + " - Src ID = " + src.ID);
            }
        }
    }
    
}
