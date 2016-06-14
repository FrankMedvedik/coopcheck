using CoopCheck.WPF.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class VoucherImport : IVoucherImport
    {
        private string _first;
        private string _middle;
        private string _last;
        private string _company;
        private string _addressLine1;
        private string _addressLine2;
        private string _municipality;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _phoneNumber;
        private string _emailAddress;
        private string _updated;
        private string _jobNumber;
        public string RecordID { get; set; }

        public VoucherImport()
        {
            AddressOk = false;
        }
        public int ID { get; set; }

        public decimal? Amount { get; set; }
        public string PersonId { get; set; }
        public string NamePrefix { get; set; }

        public string First
        {
            get { return _first; }
            set
            {
                _first = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _first = value.Trim();
                }

            }
        }

        public string Middle
        {
            get { return _middle; }
            set
            {
                _middle = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _middle = value.Trim();
                }
            }
        }

        public string Last
        {
            get { return _last; }
            set
            {
                _last = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _last = value.Trim();
                }
            }
        }

        public string Suffix { get; set; }

        public string Title { get; set; }

        public string Company
        {
            get { return _company; }
            set
            {
                _company = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _company = value.Trim();
                }
            }
        }

        public string AddressLine1
        {
            get { return _addressLine1; }
            set {
                _addressLine1 = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _addressLine1 = value.Trim();
                }
            }
        }

        public string AddressLine2
        {
            get { return _addressLine2; }
            set
            {
                _addressLine2 = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _addressLine2 = value.Trim();
                }
            }
        }

        public string Municipality
        {
            get { return _municipality; }
            set
            {
                _municipality = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _municipality = value.Trim();
                }
            }
        }

        public string Region
        {
            get { return _region; }
            set
            {
                _region = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _region = value.Trim();
                }
            }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _postalCode = value.Trim();
                }

            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _country = value.Trim();
                }
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _phoneNumber = value.Trim();
                }
            }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _emailAddress = value.Trim();
                }
            }
        }

        public string Updated
        {
            get { return _updated; }
            set
            {
                _updated = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _updated = value.Trim();
                }
            }
        }

        public string JobNumber
        {
            get { return _jobNumber; }
            set
            {
                _jobNumber = null;
                if (!string.IsNullOrEmpty(value))
                {
                    _jobNumber = value.Trim();
                }
            }
        }

        public IVoucherImport GetNewWithDefaults()
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

