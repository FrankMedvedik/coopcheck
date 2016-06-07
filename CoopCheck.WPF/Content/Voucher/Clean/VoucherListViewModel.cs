using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherListViewModel : ViewModelBase
    {
        private bool _canPost;
        private DataCleanCriteria _dataCleanCriteria;

        private ObservableCollection<VoucherImportWrapper> _filteredVoucherImports;

        private bool _filterRows;

        private bool _isCleaning;

        private VoucherImportWrapper _selectedVoucher;

        private StatusInfo _status;

        private ObservableCollection<VoucherImportWrapper> _voucherImports =
            new ObservableCollection<VoucherImportWrapper>();

        private VoucherImportWrapper _workVoucherImport;

        public VoucherListViewModel()
        {
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

            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                }
            });

            Messenger.Default.Register<NotificationMessage<DataCleanCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.DataCleanCriteriaUpdated)
                {
                    _dataCleanCriteria = message.Content;
                }
            });
            CleanAndPostVouchersCommand = new RelayCommand(CleanVouchersStub, CanRunBackgroundCleaner);
        }

        public RelayCommand CleanAndPostVouchersCommand { get; }

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
                Status = new StatusInfo
                {
                    StatusMessage = "voucher checking is complete",
                    IsBusy = false
                };
            }
        }

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

        public VoucherImportWrapper SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                if (value != null)
                {
                    _selectedVoucher = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ShowSelectedVoucher");
                    Messenger.Default.Send(new NotificationMessage<PaymentReportCriteria>(new PaymentReportCriteria
                    { LastName = SelectedVoucher.Last, FirstName = SelectedVoucher.First },
                        Notifications.FindPayeePayments));
                }
            }
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

        public bool HasErrors => VoucherErrorCnt > 0;

        public int VoucherErrorCnt
        {
            get { return VoucherImports.Count(v => v.HasErrors); }
        }

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

        private void CleanVouchersStub()
        {
            CleanVouchers(VoucherImports.ToList());
        }

        public async void CleanVouchers(List<VoucherImportWrapper> vouchers)
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.HaveUncommittedVouchers));
            Messenger.Default.Send(new NotificationMessage(Notifications.HaveDirtyVouchers));
            Status = new StatusInfo
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
                new VoucherWrappersMessage { ExcelFileInfo = ExcelFileInfo, VoucherImports = VoucherImports },
                Notifications.VouchersDataCleaned));
            CanPost = true;
            IsCleaning = false;
        }


        public bool CanRunBackgroundCleaner()
        {
            return CanPost;
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

        private void FilterVoucherImports()
        {
            FilteredVoucherImports = (FilterRows)
                ? new ObservableCollection<VoucherImportWrapper>(
                    VoucherImports.Where(x => x.HasErrors || !x.OkEmailAddress || !x.OkMailingAddress || !x.OkPhone))
                : VoucherImports;
        }

        #region DisplayState

        public void DeleteSelectedVoucher()
        {
            VoucherImports.Remove(SelectedVoucher);
            FilterVoucherImports();
            SelectedVoucher = null;
        }

        public bool ShowSelectedVoucher
        {
            get { return (SelectedVoucher != null); }
        }

        public void CancelNewVoucher()
        {
            WorkVoucherImport = null;
            ;
        }

        #endregion
    }
}