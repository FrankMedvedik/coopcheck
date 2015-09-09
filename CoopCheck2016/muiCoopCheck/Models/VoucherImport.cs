using CoopCheck.Library;
using CoopCheck.WPF.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class VoucherImport : IVoucherEdit

    {
        public int Id { get; private set; }
        public decimal? Amount { get; set; }
        public string PersonId { get; set; }
        public string NamePrefix { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Municipality { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Updated { get; set; }
        public string JobNumber { get; set; }

        public VoucherEdit ToVoucherEdit()
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = AddressLine1;
            n.AddressLine2 = AddressLine2;
            n.Amount = Amount;
            n.Company = Company;
            n.Country = Country;
            n.EmailAddress = EmailAddress;
            n.First = First;
            n.Last = Last;
            n.Middle = Middle;
            n.Municipality = Municipality;
            n.NamePrefix = NamePrefix;
            n.PersonId = PersonId;
            n.PhoneNumber = PhoneNumber;
            n.PostalCode = PostalCode;
            n.Region = Region;
            n.Suffix = Suffix;
            n.Title = Title;
            return n;
        }
    }
}
