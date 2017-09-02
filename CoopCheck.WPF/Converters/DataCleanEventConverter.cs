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

    public enum VoucherTypes
    {
        SwiftPay,
        Check
    }
    
    public class VoucherTypeConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((string)parameter == (string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }
    class DataCleanEventConverter 
    {
        public static VoucherImportWrapper ToVoucherImportWrapper(DataCleanEvent e)
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

            var v = new VoucherImportWrapper(n);
            v.DataCleanDate = e.DataCleanDate;
            v.AltAddress = e.Output;
            return v;
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

        public static VoucherImportWrapper ToVoucherImportWrapper(DataCleanEvent e, VoucherImportWrapper src)
        {
            VoucherImportWrapper tgt = null;

            try
            {
                tgt = ToVoucherImportWrapper(e);
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
