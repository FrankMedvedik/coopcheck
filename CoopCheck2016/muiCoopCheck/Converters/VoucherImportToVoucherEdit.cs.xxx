using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CoopCheck.Library;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Converters
{
    public class VoucherImportToVoucherEdit : IValueConverter
    {
        protected VoucherEdit ConvertValue(VoucherImport v)
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = v.AddressLine1;
            n.AddressLine2 = v.AddressLine2;
            n.Amount = v.Amount;
            n.Company = v.Company;
                //n.Country = v.Country;
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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = new object();
            if (value is VoucherImport)
            {
                return ConvertValue((VoucherImport) value);
            }
           return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
