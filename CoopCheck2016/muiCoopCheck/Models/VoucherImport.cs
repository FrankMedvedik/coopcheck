using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CoopCheck.Library;
using CoopCheck.WPF.Interfaces;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Models
{
    public class VoucherImport : ViewModelBase,  IVoucherEdit, IDataErrorInfo

    {
        private decimal? _amount;
        private string _namePrefix;
        private string _personId;
        private int _id;
        private string _first;
        private string _middle;
        private string _last;
        private string _suffix;
        private string _title;
        private string _company;
        private string _addressLine1;
        private string _addressLine2;
        private string _municipality;
        private string _region;
        private string _postalCode;
        private string _phoneNumber;
        private string _emailAddress;
        private string _updated;
        private string _jobNumber;
        private StatusInfo _status;

        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; NotifyPropertyChanged(); }
        }

        public string PersonId
        {
            get { return _personId; }
            set { _personId = value; NotifyPropertyChanged(); }
        }

        public string NamePrefix
        {
            get { return _namePrefix; }
            set { _namePrefix = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string First
        {
            get { return _first; }
            set { _first = value; NotifyPropertyChanged(); }
        }

        public string Middle
        {
            get { return _middle; }
            set { _middle = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string Last
        {
            get { return _last; }
            set { _last = value; NotifyPropertyChanged(); }
        }

        public string Suffix
        {
            get { return _suffix; }
            set { _suffix = value; NotifyPropertyChanged(); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged(); }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; NotifyPropertyChanged(); }
        }

        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string Municipality
        {
            get { return _municipality; }
            set { _municipality = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string Region
        {
            get { return _region; }
            set { _region = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; NotifyPropertyChanged(); }
        }

        public string Country { get; set; }

        [Required]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; NotifyPropertyChanged(); }
        }

        [Required]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; NotifyPropertyChanged(); }
        }

        public string Updated
        {
            get { return _updated; }
            set { _updated = value; NotifyPropertyChanged(); }
        }

        public string JobNumber
        {
            get { return _jobNumber; }
            set { _jobNumber = value; NotifyPropertyChanged(); }
        }

        public VoucherEdit ToVoucherEdit()
        {
            var n = VoucherEdit.NewVoucherEdit();
            n.AddressLine1 = AddressLine1;
            n.AddressLine2 = AddressLine2;
            n.Amount = Amount;
            n.Company = Company;
            //n.Country = Country;
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

        public static VoucherImport GetTestData()
        {
            return new VoucherImport()
            {
                AddressLine1 = "Address 1",
                AddressLine2 = "Address 2 ",
                Amount = 1000,
                Company = "company name",
               // Country = "US",
                EmailAddress = "ME@reckner.com",
                First = "FirstName",
                Last = "LastName",
                Middle = "middlename",
                Municipality = "cityname",
                NamePrefix = "Dr",
                PhoneNumber = "5551212",
                PostalCode = "100000000",
                Region = "PA",
                Suffix = "jr",
                Title = "title"
            };
        }

        public VoucherImport GetNewWithDefaults()
        {
            return new VoucherImport()
            {
                Amount = 0
                //Country = "US"
            };
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "First")
                {
                    return string.IsNullOrEmpty(this.First) ? "first name required" : null;
                }
                if (columnName == "Last")
                {
                    return string.IsNullOrEmpty(this.Last) ? "last name required" : null;
                }
                if (columnName == "PhoneNumber")
                {
                    return string.IsNullOrEmpty(this.PhoneNumber) ? "phone number required" : null;
                }
                if (columnName == "AddressLine1")
                {
                    return string.IsNullOrEmpty(this.AddressLine1) ? "street address required" : null;
                }
                if (columnName == "Municipality")
                {
                    return string.IsNullOrEmpty(this.Municipality) ? "city required" : null;
                }

                if (columnName == "Region")
                {
                    return string.IsNullOrEmpty(this.Region) ? "state required" : null;
                }

                if (columnName == "PostalCode")
                {
                    return string.IsNullOrEmpty(this.PostalCode) ? "postal code required" : null;
                }

                if (columnName == "EmailAddress")
                {
                    return string.IsNullOrEmpty(this.EmailAddress) ? "email address required" : null;
                }
                if (columnName == "Amount")
                {
                    return (this.Amount == 0) ? "an amount in dollars required" : null;
                }
                return null;
            }
        }

        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                StatusInfo s = new StatusInfo()
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }
        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }
    }
    }

