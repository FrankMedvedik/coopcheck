using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CoopCheck.WPF.Converters;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Import
{

    public class VoucherListViewModel : ViewModelBase
    {

        public ObservableCollection<OutputStreetAddress> ValidationResults
        {
            get { return _validationResults; }
            set
            {
                _validationResults = value;
                NotifyPropertyChanged();
            }
        }

        public List<VoucherImport> Vi
        {
            get { return VoucherImports.Select(r => r.Model).ToList(); }
            set
            {
                var a = new ObservableCollection<VoucherImportWrapper>();
                foreach (var v in value)
                {
                    a.Add(new VoucherImportWrapper(v));
                }

                VoucherImports = a;
            }
        }

        private ObservableCollection<VoucherImportWrapper> _filteredVoucherImports;

        public ObservableCollection<VoucherImportWrapper> FilteredVoucherImports
        {
            get { return _filteredVoucherImports; }
            set
            {
                _filteredVoucherImports = value;
                NotifyPropertyChanged();
                var s = new StatusInfo()
                {
                    StatusMessage = string.Format("{0} Vouchers Loaded", FilteredVoucherImports.Count)
                };
                Status = s;
            }
        }

        public RelayCommand DataCleanAddressesCommand { get; private set; }


        public bool CanDataClean { get; set; }

        public async void DataCleanAddresses()
        {
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("Remote Address Validation in progress"),
                IsBusy = true
            };
            try
            {
                var a = VoucherImports.Select(v => VoucherImportConverter.ToInputStreetAddress(v.Model)).ToList();
                var results = new ObservableCollection<OutputStreetAddress>(await DataCleanSvc.ValidateAddresses(a));
              //  ValidationResults = results;
                Status = new StatusInfo()
                {
                    StatusMessage = String.Format("Address Validation Complete. {0} messages returned", results.Count)
                };
                int n = 0;
                while (n < (VoucherImports.Count - 1))
                {
                    //VoucherImports[n].AddressOk = ValidationResults[n].AddressOk;
                    VoucherImports[n].AltAddress = results[n];
                    n++;
                }
            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = String.Format("Error during validation"),
                    ErrorMessage = e.Message  
                };
            }
        }

        private ObservableCollection<VoucherImportWrapper> _voucherImports =
            new ObservableCollection<VoucherImportWrapper>();

        public ObservableCollection<VoucherImportWrapper> VoucherImports
        {
            get { return _voucherImports; }
            set
            {
                _voucherImports = value;
                NotifyPropertyChanged();
                FilterVoucherImports();
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

        public VoucherListViewModel()
        {
            DataCleanAddressesCommand = new RelayCommand(DataCleanAddresses, CanDataCleanAddresses);
        }

        public bool CanDataCleanAddresses() { return true;}

    #region DisplayState

        public void DeleteSelectedVoucher()
        {
                FilteredVoucherImports.Remove(SelectedVoucher);
                SelectedVoucher = null;
        }

        public Boolean ShowSelectedVoucher
        {
            get { return (SelectedVoucher != null); }
        }

        public void CancelNewVoucher()
        {
            WorkVoucherImport = null; ;

        }
        #endregion

        private StatusInfo _status;

        private VoucherImportWrapper _selectedVoucher ;

        public VoucherImportWrapper SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ShowSelectedVoucher");
            }
        }


    public  void AddNewVoucher()
    {
            FilteredVoucherImports.Add(WorkVoucherImport);
    }
    public void CreateNewVoucher()
    {
            var v = new VoucherImportWrapper(new VoucherImport().GetNewWithDefaults());
            WorkVoucherImport = v;
     }
    public VoucherImportWrapper WorkVoucherImport
    {
        get { return _workVoucherImport; }
        set
        {
            _workVoucherImport = value;
            NotifyPropertyChanged();
        }
    }
    private VoucherImportWrapper _workVoucherImport;


    public Boolean HasErrors => VoucherErrorCnt > 0;

    public int VoucherErrorCnt
    {
        get { return VoucherImports.Count(v => v.HasErrors); }
    }

    private bool _filterRows;
   private ObservableCollection<OutputStreetAddress> _validationResults;

        public bool FilterRows
    {
        get { return _filterRows; }
        set
        {
            _filterRows = value;
            FilterVoucherImports();
            NotifyPropertyChanged();
            
        }
        }

        private void FilterVoucherImports()
        {
            FilteredVoucherImports = (FilterRows) ? new ObservableCollection<VoucherImportWrapper>( VoucherImports.Where(x =>x.HasErrors)) : VoucherImports;
        }
    }
}
