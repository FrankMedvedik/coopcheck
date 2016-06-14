using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoopCheck.WPF.Contracts.Interfaces;
using CoopCheck.WPF.Models;
using DataClean.Contracts.Interfaces;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Wrappers
{
    public class VoucherImportWrapper : ModelWrappper<IVoucherImport>, IVoucherImportWrapper
    {
        public VoucherImportWrapper(IVoucherImport model) : base(model)
        {
        }

      
        public int ID
        {
            get
            {
                return Model.ID;
            }
            set { Model.ID = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean OkMailingAddress
        {
            get { return _altAddress.OkMailingAddress; }
        }
        public Boolean OkEmailAddress
        {
            get { return _altAddress.OkEmailAddress; }
        }

        public Boolean OkPhone
        {
            get { return _altAddress.OkPhone; }
        }

        private IParseResult emptyParse = new AddressCleanerFeedback();

        private IOutputStreetAddress _altAddress = new CleanedAddress()
        {
            Results = new List<IParseResult>()
        };
        private string _altAddressLine1;
        private string _altAddressLine2;
        private string _altRegion;
        private string _altPostalCode;
        private string _altMunicipality;
        private DateTime? _dataCleanDate = null;

        public string RecordID
        {
            get
            {
                return Model.RecordID;
            }
            set
            {
                Model.RecordID = value;
                NotifyPropertyChanged();
            }
        }

        public IOutputStreetAddress AltAddress
        {
            get { return (IOutputStreetAddress) _altAddress; }
            set
            {
                if (value == null) return;
                 _altAddress = value;
                SetAlternateValues();
                NotifyPropertyChanged();
                NotifyPropertyChanged("AltAddressText");
                NotifyPropertyChanged("OKMailingAddress");
                NotifyPropertyChanged("OKEmailAddress");
                NotifyPropertyChanged("OKPhone");
            }
        }

   

        private void SetAlternateValues()
        {
            if (AltAddress.AddressLine1.Length > 0) AltAddressLine1 = AltAddress.AddressLine1;
            if (AltAddress.AddressLine2.Length > 0) AltAddressLine1 = AltAddress.AddressLine2;
            if (AltAddress.PostalCode.Length > 0) AltPostalCode = AltAddress.PostalCode;
            if (AltAddress.State.Length > 0) AltRegion = AltAddress.State;
            if (AltAddress.City.Length > 0) AltMunicipality = AltAddress.City;
        }

        public string AltAddressText
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4}", AltAddress.AddressLine1 + Environment.NewLine,
                    (AltAddress.AddressLine2.Length > 0) ? AltAddress.AddressLine1 + Environment.NewLine : "",
                    AltAddress.City, AltAddress.State, AltAddress.PostalCode);
            }
        }
   public bool AddressOk
        {
            get { return Model.AddressOk; }
            set
            {
                Model.AddressOk = value;
                NotifyPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Honoraria is required.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Honoraria Amount", Description = "Enter an dollar amount")]
        public decimal? Amount
        {
            get { return Model.Amount; }
            set
            {
                Model.Amount = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string PersonId
        {
            get { return Model.PersonId; }
            set
            {
                Model.PersonId = value;
                NotifyPropertyChanged();
                Validate();
            }
        }
        public DateTime? DataCleanDate
        {
            get { return _dataCleanDate; }
            set
            {
                _dataCleanDate = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasBeenDataCleaned");
            }
        }

        public bool HasBeenDataCleaned
        {
            get { return (_dataCleanDate != null); }
        }

        public string NamePrefix
        {
            get { return Model.NamePrefix; }
            set
            {
                Model.NamePrefix = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        [Display(Name = "Name", Description = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string First
        {
            get { return Model.First; }
            set
            {
                Model.First = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string Middle
        {
            get { return Model.Middle; }
            set
            {
                Model.Middle = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        [Display(Name = "Name", Description = "Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage =
            "Last name must be between 2 and 25 characters long.")]
        public string Last
        {
            get { return Model.Last; }
            set
            {
                Model.Last = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string Suffix
        {
            get { return Model.Suffix; }
            set
            {
                Model.Suffix = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string Title
        {
            get { return Model.Title; }
            set
            {
                Model.Title = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string Company
        {
            get { return Model.Company; }
            set
            {
                Model.Company = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        [Display(Name = "Address 1", Description = "Address Line 1 ")]
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage =
            "Address line 1 must be between 2 and 50 characters long.")]
        public string AddressLine1
        {
            get { return Model.AddressLine1; }
            set
            {
                Model.AddressLine1 = value;
                NotifyPropertyChanged();
                Validate();
            }
        }


        public string AltAddressLine1
        {
            get { return _altAddressLine1; }
            set
            {
                _altAddressLine1 = value;
                NotifyPropertyChanged();
            }
        }

        public string AltAddressLine2
        {
            get { return _altAddressLine2; }
            set
            {
                _altAddressLine2 = value;
                NotifyPropertyChanged();
            }
        }

        [StringLength(50, MinimumLength = 0, ErrorMessage =
            "Address line 1 must be between 0 and 50 characters long.")]
        public string AddressLine2
        {
            get { return Model.AddressLine2; }
            set
            {
                Model.AddressLine2 = value;
                NotifyPropertyChanged();
                Validate();
            }
        }


        [Display(Name = "City", Description = "City or Municipality")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(35, MinimumLength = 2, ErrorMessage =
            "City 1 must be between 2 and 50 characters long.")]

        public string Municipality
        {
            get { return Model.Municipality; }
            set
            {
                Model.Municipality = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string AltMunicipality
        {
            get { return _altMunicipality; }
            set
            {
                _altMunicipality = value;
                NotifyPropertyChanged();
            }
        }

        [Display(Name = "State", Description = "State or Region")]
        [Required(ErrorMessage = "State is required.")]
        [StringLength(35, MinimumLength = 2, ErrorMessage =
            "State  must be between 2 and 50 characters long.")]
        public string Region
        {
            get { return Model.Region; }
            set
            {
                Model.Region = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string AltRegion
        {
            get { return _altRegion; }
            set
            {
                _altRegion = value;
                NotifyPropertyChanged();
            }
        }

        [Display(Name = "Postal", Description = "Postal or Zip Code")]
        [Required(ErrorMessage = "Postal Code is required.")]
        [DataType(DataType.PostalCode)]
        public string PostalCode
        {
            get { return Model.PostalCode; }
            set
            {
                Model.PostalCode = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string AltPostalCode
        {
            get { return _altPostalCode; }
            set
            {
                _altPostalCode = value;
                NotifyPropertyChanged();
            }
        }

        public string Country
        {
            get { return Model.Country; }
            set
            {
                Model.Country = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        [Display(Name = "Phone", Description = "Phone Number")]
        //  [Required(ErrorMessage = "Phone Number is required.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber
        {
            get { return Model.PhoneNumber; }
            set
            {
                Model.PhoneNumber = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        [Display(Name = "Email", Description = "email address")]
        [Required(ErrorMessage = "a valid email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress
        {
            get { return Model.EmailAddress; }
            set
            {
                Model.EmailAddress = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string Updated
        {
            get { return Model.Updated; }
            set
            {
                Model.Updated = value;
                NotifyPropertyChanged();
                Validate();
            }
        }

        public string JobNumber
        {
            get { return Model.JobNumber; }
            set
            {
                Model.JobNumber = value;
                NotifyPropertyChanged();
                Validate();
            }
        }
    }



}