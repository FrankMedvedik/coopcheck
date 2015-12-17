using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.Voucher.Clean
{

    public class VoucherListViewModel : ViewModelBase
    {
        private DataCleanCriteria _dataCleanCriteria;
        public RelayCommand CleanAndPostVouchersCommand { get; private set; }
        public bool CanCleanAndPostVouchers()
        {
            return true;
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
            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                    VoucherImports = message.Content.VoucherImports;
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                }

            });

            Messenger.Default.Register<NotificationMessage<DataCleanCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.DataCleanCriteriaUpdated)
                {
                    _dataCleanCriteria = message.Content;
                }
            });

            this.CleanAndPostVouchersCommand = new RelayCommand(CleanTheVouchers, CanCleanAndPostVouchers);

        }
        public async void CleanTheVouchers()
        {
            //CanDataClean = false;
            List<VoucherImport> l = new List<VoucherImport>();
            foreach (var v in VoucherImports)
                l.Add(VoucherImportWrapperConverter.ToVoucherImport(v));
            try
            {
                VoucherImports = new ObservableCollection<VoucherImportWrapper>(
                    await DataCleanVoucherImportSvc.CleanVouchers(l, _dataCleanCriteria, ExcelFileInfo));

                Messenger.Default.Send(new NotificationMessage<VoucherWrappersMessage>
                    (new VoucherWrappersMessage {  VoucherImports = this.VoucherImports, ExcelFileInfo = this.ExcelFileInfo}
                        , Notifications.HaveReviewedVouchers));
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
        //private void CleanAndSaveVouchers()
        //{ 
        //Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
        //    {
        //        if (message.Notification == Notifications.VouchersDataCleaned)
        //        {
        //            ExcelFileInfo = message.Content.ExcelFileInfo;
        //            VoucherImportWrappers = message.Content.VoucherImports;
        //        }
        //            });
        //}


        public ExcelFileInfoMessage ExcelFileInfo { get; set; }

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
