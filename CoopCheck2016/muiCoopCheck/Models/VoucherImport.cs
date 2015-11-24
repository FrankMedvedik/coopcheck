using CoopCheck.Library;

namespace CoopCheck.WPF.Models
{
    public class VoucherImport
    {
        public VoucherImport()
        {
            AddressOk = false;
        }
        public int Id { get; set; }

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

        public VoucherImport GetNewWithDefaults()
        {
            return new VoucherImport()
            {
                Amount = 0
                //Country = "US"
            };
        }
        public bool AddressOk { get; set; }
    }
}

