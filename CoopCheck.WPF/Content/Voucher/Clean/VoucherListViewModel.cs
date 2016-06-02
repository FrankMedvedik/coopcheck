using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Reckner.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using DataClean.Services;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherListViewModel : ViewModelBase
    {
      

        private DataCleanEventFactory _dataCleanEventFactory;
        public RelayCommand CleanAndPostVouchersCommand { get; private set; }

        public VoucherListViewModel()
        {
            var c = ConfigurationManager.AppSettings;
            var dataCleanCriteria = new DataCleanCriteria
            {
                AutoFixAddressLine1 = false,
                AutoFixCity = false,
                AutoFixPostalCode = true,
                AutoFixState = false,
                ForceValidation = false
            };
            _dataCleanEventFactory = new DataCleanEventFactory(new DataCleaner(c), new DataCleanRespository(),
                dataCleanCriteria);


            // called when the vouchers come out of the excel import process
            Messenger.Default.Register<NotificationMessage<ExcelVouchersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.ImportWorksheetReady && message.Content.VoucherImports.Any())
                {
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                    var a = message.Content.VoucherImports.Select(v => new VoucherImportWrapper(v)).ToList();
                    CleanVouchers(a);
                }
            });

            //Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            //{
            //    if (message.Notification == Notifications.VouchersDataCleaned)
            //    {
            //    }
            //});

            CleanAndPostVouchersCommand = new RelayCommand(CleanVouchersStub, CanRunCleaner);
        }

        private void CleanVouchersStub()
        {
            CleanVouchers(VoucherImports.ToList());
        }

        public async void CleanVouchers(List<VoucherImportWrapper> vouchers)
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.HaveUncommittedVouchers));
            Messenger.Default.Send(new NotificationMessage(Notifications.HaveDirtyVouchers));
            Status = new StatusInfo()
            {
                StatusMessage =
                    "please wait  - checking the street addresses, email and phone numbers of the vouchers...",
                IsBusy = true
            };
            IsCleaning = true;
            CanPost = false;
            var results = await DataCleanVoucherImportSvc.CleanVouchers(vouchers);
            VoucherImports =
                new ObservableCollection<VoucherImportWrapper>(
                    results.OrderBy(x => x.OkMailingAddress)
                        .ThenBy(x => x.OkPhone)
                        .ThenBy(x => x.OkEmailAddress)
                        .ToList());
            FilterVoucherImports();
            Messenger.Default.Send(new NotificationMessage<VoucherWrappersMessage>(
                new VoucherWrappersMessage {ExcelFileInfo = this.ExcelFileInfo, VoucherImports = this.VoucherImports},
                Notifications.VouchersDataCleaned));
            CanPost = true;
            IsCleaning = false;
        }

        //public async Task<ObservableCollection<VoucherImportWrapper>> DoCleanVouchers(
        //    List<VoucherImportWrapper> vouchers)
        //{
        //    var l = await Task<ObservableCollection<VoucherImportWrapper>>.Factory.StartNew(() =>
        //    {
        //        var cleanVouchers = new ObservableCollection<VoucherImportWrapper>();
        //        foreach (var v in vouchers)
        //        {
        //            v.ID = HashHelperSvc.GetHashCode(v.Region, v.Municipality, v.PostalCode, v.AddressLine1,
        //                v.AddressLine2, v.EmailAddress, v.PhoneNumber, v.Last, v.First);
        //        }
        //        var inputAddresses =
        //            vouchers.Select(v => VoucherImportWrapperConverter.ToInputStreetAddress(v)).ToList();
        //        List<DataCleanEvent> dataCleanEvents = new List<DataCleanEvent>();
        //        var json = JsonConvert.SerializeObject(inputAddresses);
        //        Console.Write(json);
        //        try
        //        {
        //            dataCleanEvents = _dataCleanEventFactory.ValidateAddresses(inputAddresses);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("DataCleaner failed" + e.Message);
        //        }

        //        var ilist = new List<VoucherImportWrapper>();
        //        foreach (var e in dataCleanEvents)
        //        {
        //            var i = DataCleanEventConverter.ToVoucherImportWrapper(e,
        //                vouchers.First(x => x.RecordID == e.RecordID));
        //            // we want to join the row to get the data we did not send to the cleaner
        //            ilist.Add(i);
        //        }
        //        return new ObservableCollection<VoucherImportWrapper>(ilist);
        //    });
        //    return l;
        //}

        public bool CanRunCleaner()
        {
            return CanPost;
        }

        public bool CanPost
        {
            get { return _canPost; }
            set
            {
                _canPost = value;
                NotifyPropertyChanged();
                CleanAndPostVouchersCommand.RaiseCanExecuteChanged();
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
                //var s = new StatusInfo()
                //{
                //    StatusMessage = string.Format("{0} Vouchers Loaded", FilteredVoucherImports.Count)
                //};
                //Status = s;
                Status = new StatusInfo()
                {
                    StatusMessage = "voucher checking is complete",
                    IsBusy = false
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

        public ExcelFileInfoMessage ExcelFileInfo { get; set; }

        #region DisplayState

        public void DeleteSelectedVoucher()
        {
            VoucherImports.Remove(SelectedVoucher);
            FilterVoucherImports();
            SelectedVoucher = null;
        }

        public Boolean ShowSelectedVoucher
        {
            get { return (SelectedVoucher != null); }
        }

        public void CancelNewVoucher()
        {
            WorkVoucherImport = null;
            ;
        }

        #endregion

        private StatusInfo _status;

        private VoucherImportWrapper _selectedVoucher;

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


        public void AddNewVoucher()
        {
            VoucherImports.Add(WorkVoucherImport);
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
        private bool _canPost;

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
            FilteredVoucherImports = (FilterRows)
                ? new ObservableCollection<VoucherImportWrapper>(
                    VoucherImports.Where(x => x.HasErrors || !x.OkEmailAddress || !x.OkMailingAddress || !x.OkPhone))
                : VoucherImports;
        }

        private bool _isCleaning = false;

        public bool IsCleaning
        {
            get { return _isCleaning; }
            set
            {
                if (_isCleaning != value)
                {
                    _isCleaning = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}