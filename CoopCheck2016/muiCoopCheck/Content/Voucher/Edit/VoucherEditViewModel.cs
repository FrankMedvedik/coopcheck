using System;
using System.ComponentModel;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Edit
{

    public class VoucherEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private BatchEdit _selectedBatch;

        public BatchEdit SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        public int BatchNum
        {
            get { return _selectedBatch.Num; }
            set
            {
                SelectedBatch = BatchEdit.GetBatchEdit(value);
                NotifyPropertyChanged();
            }
        }


        private VoucherEdit _selectedVoucher;
        public VoucherEdit SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                NotifyPropertyChanged();
            }
        }
        public int SelectedVoucherId
        {
            get
            {
                if (SelectedVoucher != null)
                    return SelectedVoucher.Id;
                return 0;
            }
            set
            {
                SelectedVoucher = SelectedBatch.Vouchers.Find(value);
            }
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        public VoucherEditViewModel()
        {
            ResetState();
            
        }

        #region DisplayState
        private bool _canSave;

        private void ResetState()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "fill in the voucher",
                ErrorMessage = ""
            };

            // load test data 
            EditVoucher = VoucherImport.GetTestData();
            //EditVoucher = new VoucherImport();
        }

        public bool CanSave
        {
            get { return _canSave; }
            set{
                _canSave = value;
                NotifyPropertyChanged();
            }

        }

        
        #endregion


        private VoucherImport _editVoucher;
        private StatusInfo _status;

        public VoucherImport EditVoucher
        {
            get { return _editVoucher; }
            set
            {
                _editVoucher = value;
                NotifyPropertyChanged();
            }
        }


        #region FormFields

        public string First
        {
            get { return EditVoucher.First; }
            set
            {
                if (EditVoucher.First != value)
                {
                    EditVoucher.First = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Last
        {
            get { return EditVoucher.Last; }
            set
            {
                if (EditVoucher.Last != value)
                {
                    EditVoucher.Last = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Middle
        {
            get { return EditVoucher.Middle; }
            set
            {
                if (EditVoucher.Middle != value)
                {
                    EditVoucher.Middle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Address1
        {
            get { return EditVoucher.AddressLine1; }
            set
            {
                if (EditVoucher.AddressLine1 != value)
                {
                    EditVoucher.AddressLine1 = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public string Address2
        {
            get { return EditVoucher.AddressLine2; }
            set
            {
                if (EditVoucher.AddressLine2 != value)
                {
                    EditVoucher.AddressLine2 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Municipality
        {
            get { return EditVoucher.Municipality; }
            set
            {
                if (EditVoucher.Municipality != value)
                {
                    EditVoucher.Municipality = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Region
        {
            get { return EditVoucher.Region; }
            set
            {
                if (EditVoucher.Region != value)
                {
                    EditVoucher.Region= value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PostalCode
        {
            get { return EditVoucher.PostalCode; }
            set
            {
                if (EditVoucher.PostalCode != value)
                {
                    EditVoucher.PostalCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return EditVoucher.Title; }
            set
            {
                if (EditVoucher.Title != value)
                {
                    EditVoucher.Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string NamePrefix
        {
            get { return EditVoucher.NamePrefix; }
            set
            {
                if (EditVoucher.NamePrefix != value)
                {
                    EditVoucher.NamePrefix = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Suffix
        {
            get { return EditVoucher.Suffix; }
            set
            {
                if (EditVoucher.Suffix != value)
                {
                    EditVoucher.Suffix = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public string JobNumber
        {
            get { return EditVoucher.JobNumber; }
            set
            {
                if (EditVoucher.JobNumber != value)
                {
                    EditVoucher.JobNumber = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Country
        {
            get { return EditVoucher.Country; }
            set
            {
                if (EditVoucher.Country != value)
                {
                    EditVoucher.Country = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string EmailAddress
        {
            get { return EditVoucher.EmailAddress; }
            set
            {
                if (EditVoucher.EmailAddress != value)
                {
                    EditVoucher.EmailAddress = value;
                    NotifyPropertyChanged();
                }
            }
        }

        

        public string Company
        {
            get { return EditVoucher.Company; }
            set
            {
                if (EditVoucher.Company != value)
                {
                    EditVoucher.Company = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal? Amount
        {
            get { return EditVoucher.Amount; }
            set
            {
                if (EditVoucher.Amount!= value)
                {
                    EditVoucher.Amount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Error
            {
                get { return null; }
            }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "First")
                {
                    return string.IsNullOrEmpty(this.First) ? "Required value" : null;
                }
                if (columnName == "Last")
                {
                    return string.IsNullOrEmpty(this.Last) ? "Required value" : null;
                }
                if (columnName == "Address1")
                {
                    return string.IsNullOrEmpty(this.Address1) ? "Required value" : null;
                }

                if (columnName == "Amount")
                {
                    return (this.Amount.GetValueOrDefault(0) == 0) ? "Required value" : null;
                }

                if (columnName == "JobNumber")
                {
                    return string.IsNullOrEmpty(this.JobNumber) ? "Required value" : null;
                }

                if (columnName == "PostalCode")
                {
                    return string.IsNullOrEmpty(this.PostalCode) ? "Required value" : null;
                }

                if (columnName == "Municipality")
                {
                    return string.IsNullOrEmpty(this.Municipality) ? "Required value" : null;
                }

                if (columnName == "Region")
                {
                    return string.IsNullOrEmpty(this.Region) ? "Required value" : null;
                }

                return null;
            }
        }
        #endregion

        public void Save()
        {
            
        }

        public void New()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
