using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using CoopCheck.WPF.Converters;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean;
using DataClean.Models;
using DataClean.Services;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Clean
{

    public class VoucherListViewModel : ViewModelBase
    {

        //private List<DataCleanEvent> _validationResults;
        //public List<DataCleanEvent> ValidationResults
        //{
        //    get { return _validationResults; }
        //    set
        //    {
        //        _validationResults = value;
        //        var ilist = new List<VoucherImportWrapper>();
        //        foreach (var e in ValidationResults)
        //        {
        //            var i = DataCleanEventConverter.ToVoucherImportWrapper(e, VoucherImports.First(x => x.ID == e.ID)); // we want to join the row to get the data we did not send to the cleaner
        //            ilist.Add(i);
        //        };
        //        VoucherImports = new ObservableCollection<VoucherImportWrapper>(ilist);
        //        NotifyPropertyChanged();
        //    }
        //}

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

        private ObservableCollection<VoucherImportWrapper> _voucherImports = new ObservableCollection<VoucherImportWrapper>();
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
           
        }

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
