
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.WPF.Content.Voucher.Edit;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Save
{
    public class VoucherSaveViewModel : ViewModelBase
    {
        private StatusInfo _status;
        public RelayCommand SaveVouchersCommand { get; private set; }
        public bool CanSaveVouchers()
        {
            return true;
        }

        
        public VoucherSaveViewModel()
        {
            this.SaveVouchersCommand = new RelayCommand(CreateBatchEditAndImportVouchers, CanSaveVouchers);

            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.HaveReviewedVouchers)
                {
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                    VoucherImportWrappers = message.Content.VoucherImports;
                    Messenger.Default.Send(new NotificationMessage(Notifications.HaveCommittedVouchers));
                }
            });
        }

        private ExcelFileInfoMessage _excelVoucherInfo;
        public ExcelFileInfoMessage ExcelFileInfo
        {
            get { return _excelVoucherInfo; }
            set {
                _excelVoucherInfo = value;
                NotifyPropertyChanged(); }
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

        private ObservableCollection<VoucherImportWrapper> _voucherImportWrappers = new ObservableCollection<VoucherImportWrapper>();

        public ObservableCollection<VoucherImportWrapper> VoucherImportWrappers
            {
                get { return _voucherImportWrappers; }
                set
                {
                    _voucherImportWrappers = value;
                    NotifyPropertyChanged();
                    SetupMessages();
                }
            }

        public bool CanSave
        {
            get { return _canSave; }
            set { _canSave = value;
                NotifyPropertyChanged(); }
        }

        private void SetupMessages()
        {

            SaveBatchInfoMessage = string.Format("{0} Vouchers will be posted totalling {1}",
                VoucherImportWrappers.Count(x => x.AltAddress.OkComplete),
                VoucherImportWrappers.Where(x => x.AltAddress.OkComplete).Select(x => x.Amount).Sum());
            ErrorBatchInfoMessage = string.Format("{0} Vouchers WITH ERRORS totalling {1} Will be saved to this workbook worksheet",
                VoucherImportWrappers.Count(x => !x.AltAddress.OkComplete),
                VoucherImportWrappers.Where(x => !x.AltAddress.OkComplete).Select(x => x.Amount).Sum());

            CanSave = (VoucherImportWrappers.Count(x => x.AltAddress.OkComplete) > 0);

        }
        public string SaveBatchInfoMessage
        {
            get { return _saveBatchInfoMessage; }
            set
            {
                _saveBatchInfoMessage = value;
                NotifyPropertyChanged();
            }
        }

        private string _saveBatchInfoMessage;
        public string ErrorBatchInfoMessage
        {
            get { return _errorBatchInfoMessage; }
            set
            {
                _errorBatchInfoMessage = value;
                NotifyPropertyChanged();
            }
        }

        public Vouchers VoucherExports { get; set; }

        private string _errorBatchInfoMessage;
        private bool _canSave;

        public async void CreateBatchEditAndImportVouchers()
        {

            try
            {
                List<VoucherImport> vouchers = new List<VoucherImport>();
                foreach (var v in VoucherImportWrappers)                /// .Where(x=>x.AltAddress.OkComplete))
                    vouchers.Add(VoucherImportWrapperConverter.ToVoucherImport(v));

                await BatchSvc.ImportVouchers(vouchers);
            }
            catch (Exception ex)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Failed to create batch",
                    ErrorMessage = ex.Message,
                    ShowMessageBox=true
                };
            }
        }

        }

    }

