using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using Reckner.WPF.ViewModel;
using Remotion.Mixins;
using CoopCheck.WPF.Converters;


namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherListViewModel : ViewModelBase
    {

        private bool _showEmailColumns;

        public bool ShowEmailColumns
        {
            get { return _showEmailColumns; }
            set
            {
                _showEmailColumns = value;
                NotifyPropertyChanged();
            } 
        }



        private System.Windows.Input.ICommand _cmdSelectVoucherType;
        public System.Windows.Input.ICommand CmdSelectVoucherType
        {
            get
            {
                return _cmdSelectVoucherType;
            }
            set
            {
                _cmdSelectVoucherType = value;
            }
        }


        private void SelectVoucherType(Object parameter)
        {
            var selectedVoucherType = (string)parameter;
            if(selectedVoucherType == "SwiftPay")
            { ShowEmailColumns = true; }
            else
            { ShowEmailColumns = false; }
        }



        private static readonly ILog log
            = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            CleanAndPostVouchersCommand = new RelayCommand(CleanVouchersStub, CanRunBackgroundCleaner);
            CmdSelectVoucherType = new RelayCommand<object>(SelectVoucherType, CanChangeVoucherType);
            ShowEmailColumns = true;
            Messenger.Default.Register<NotificationMessage<vwPayment>>(this, message =>
            {
                if (message.Notification == Notifications.HistoryPaymentSent)
                {
                    PopulateSelectedVoucherFromOldPayment(message.Content);
                }
            });
            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                    VoucherImports =
                        new ObservableCollection<VoucherImportWrapper>(
                            message.Content.VoucherImports.OrderBy(x => x.OkMailingAddress)
                                .ThenBy(x => x.OkPhone)
                                .ThenBy(x => x.OkEmailAddress)
                               .ToList());
                           FilterVoucherImports();
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
            
        }

   

        private bool CanChangeVoucherType(object c)
        {
            return true;
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<NotificationMessage<ExcelVouchersMessage>>(this);
            Messenger.Default.Unregister <NotificationMessage<vwPayment >>(this);
            Messenger.Default.Unregister<NotificationMessage<VoucherWrappersMessage>>(this);
            Messenger.Default.Unregister<NotificationMessage<DataCleanCriteria>>(this);
        }

        private void PopulateSelectedVoucherFromOldPayment(vwPayment oldPayment)
        {
            SelectedVoucher.AddressLine1 = oldPayment.address_1;
            SelectedVoucher.AddressLine2 = oldPayment.address_2;
            SelectedVoucher.EmailAddress = oldPayment.email;
            SelectedVoucher.PhoneNumber= oldPayment.phone_number;
            SelectedVoucher.First = oldPayment.first_name;
            SelectedVoucher.Last = oldPayment.last_name;
            SelectedVoucher.Company = oldPayment.company;
            SelectedVoucher.Municipality = oldPayment.municipality;
            SelectedVoucher.Region = oldPayment.region;
            SelectedVoucher.Country = oldPayment.country;
            SelectedVoucher.PostalCode = oldPayment.postal_code;
            SelectedVoucher.Middle = oldPayment.middle_name;

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
            try
            {
                var results = await DataCleanVoucherImportSvc.CleanVouchers(vouchers);
                Messenger.Default.Send(new NotificationMessage<VoucherWrappersMessage>(
                    new VoucherWrappersMessage { ExcelFileInfo = ExcelFileInfo, VoucherImports = results },
                    Notifications.VouchersDataCleaned));
                CanPost = true;
                IsCleaning = false;
            }
            catch (Exception e)
            {
                log.Error(string.Format("clean vouchers failed {0} {1}", e.Message, e.InnerException?.Message ));
                CanPost = false;
                IsCleaning = false;

                log.Error(string.Format("cleaning the vouchers failed - {0} - {1} ", e.Message,
                                     e.InnerException?.Message));
                string errorMessage = string.Format("error cleaning vouchers: {0}", e.Message);
                ModernDialog.ShowMessage(errorMessage, "Cleaning vouchers failed", MessageBoxButton.OK);
            }

        }


        public bool CanRunBackgroundCleaner()
        {
            return true;
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
            
        }

        #endregion
    }
}