
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Save
{
    public class VoucherSaveViewModel : ViewModelBase
    {
        private StatusInfo _status;

        public VoucherSaveViewModel()
        {
            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                    VoucherImports = message.Content.VoucherImports;
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

        private ObservableCollection<VoucherImportWrapper> _voucherImports = new ObservableCollection<VoucherImportWrapper>();

        public ObservableCollection<VoucherImportWrapper> VoucherImports
            {
                get { return _voucherImports; }
                set
                {
                    _voucherImports = value;
                    NotifyPropertyChanged();
                    setupMessages();
                CanSave = true;

                }
            }

        public bool CanSave
        {
            get { return _canSave; }
            set { _canSave = value; NotifyPropertyChanged(); }
        }

        private void setupMessages()
        {

            SaveBatchInfoMessage = string.Format("{0} Vouchers will be posted totalling {1}",
                VoucherImports.Count(x => x.AltAddress.OkComplete),
                VoucherImports.Where(x => x.AltAddress.OkComplete).Select(x => x.Amount).Sum());
            ErrorBatchInfoMessage = string.Format("{0} Vouchers WITH ERRORS totalling {1} Will be saved to this workbook worksheet",
                VoucherImports.Count(x => !x.AltAddress.OkComplete),
                VoucherImports.Where(x => !x.AltAddress.OkComplete).Select(x => x.Amount).Sum());
            
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
    }
}
