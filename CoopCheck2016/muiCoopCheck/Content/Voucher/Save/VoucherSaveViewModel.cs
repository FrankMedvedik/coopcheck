
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class Vouchers : List<VoucherExcelExport> { }

    public class VoucherSaveViewModel : ViewModelBase
    {
        private StatusInfo _status;
        public RelayCommand SaveVouchersCommand { get; private set; }
        public RelayCommand ExportVouchersCommand { get; private set; }
        public bool CanSaveVouchers()
        {
            return true;
        }

        
        public VoucherSaveViewModel()
        {
            CanSave = false;
            CanExport = false;

            SaveVouchersCommand = new RelayCommand(CreateBatchEditAndImportVouchers, CanSaveVouchers);
            ExportVouchersCommand = new RelayCommand(ExportVouchers, CanSaveVouchers);

            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.HaveReviewedVouchers)
                {
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                    VoucherImportWrappers = message.Content.VoucherImports;
                    Messenger.Default.Send(new NotificationMessage(Notifications.HaveCommittedVouchers));
                    if(VoucherImportWrappers.Any(x => x.OkMailingAddress)) CanSave = true;
                    if (VoucherImportWrappers.Any(x => !x.OkMailingAddress)) CanExport = true;
                }
            });
        }

        public async void ExportVouchers()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "exporting vouchers to Excel",
                IsBusy = true
            };
           
            await Task.Factory.StartNew(() =>
            {
                string dtFmt = String.Format("{0:g}", DateTime.Now).Replace('/', '.');
                dtFmt = dtFmt.Replace(':', '.');
                dtFmt = dtFmt.Replace(' ', '.');

                try
                {
                    if (BadVoucherExports.Count > 0)
                    {
                        ExportToExcel<VoucherExcelExport, Vouchers> s = new ExportToExcel<VoucherExcelExport, Vouchers>
                        {
                            ExcelSourceWorkbook = ExcelFileInfo.ExcelFilePath,
                            ExcelWorksheetName = String.Format("Errors.{0}", dtFmt),
                            dataToPrint = BadVoucherExports
                        };
                        s.GenerateReport();
                        
                        if (GoodVoucherExports.Count > 0)
                        {
                            s.ExcelWorksheetName = String.Format("Processed.{0}", dtFmt);
                            s.dataToPrint = GoodVoucherExports;
                            s.GenerateReport();
                            CanExport = false;
                        }
                        ErrorBatchInfoMessage = String.Format("Vouchers Exported to {0}",
                            Path.GetFileName(ExcelFileInfo.ExcelFilePath));
                    }
                }
                catch (Exception e)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = "export to excel failed",
                        ErrorMessage = e.Message,
                        ShowMessageBox = true
                    };
                }
            });

            Status = new StatusInfo()
            {
                StatusMessage = "export complete",
            };
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
        public bool CanExport
        {
            get { return _canExport; }
            set
            {
                _canExport = value;
                NotifyPropertyChanged();
            }
        }

        private void SetupMessages()
        {

            //SaveBatchInfoMessage = string.Format("8 Vouchers will be posted totalling $900");
            //ErrorBatchInfoMessage = string.Format("3 Vouchers WITH ERRORS totaling $300 will be saved to the job errors worksheet");
            //CanSave =true;

                SaveBatchInfoMessage = string.Format("{0} Vouchers will be posted totalling {1:C}",
                VoucherImportWrappers.Count(x => x.AltAddress.OkComplete),
                VoucherImportWrappers.Where(x => x.AltAddress.OkComplete).Select(x => x.Amount).Sum());
                ErrorBatchInfoMessage = string.Format("{0} Vouchers with errors totalling {1:C} will be saved to Excel",
                VoucherImportWrappers.Count(x => !x.AltAddress.OkMailingAddress),
                VoucherImportWrappers.Where(x => !x.AltAddress.OkMailingAddress).Select(x => x.Amount).Sum());

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
        public List<VoucherExcelExport> GoodVoucherExports
        {
            get
            { return VoucherImportWrappers.Where(x => x.AltAddress.OkMailingAddress).Select(VoucherImportWrapperConverter.ToVoucherExcelExport).ToList(); }
        }

        public List<VoucherExcelExport> BadVoucherExports { get
            { return  VoucherImportWrappers.Where(x => !x.AltAddress.OkMailingAddress).Select(VoucherImportWrapperConverter.ToVoucherExcelExport).ToList(); } }

        private string _errorBatchInfoMessage;
        private bool _canSave;
        private bool _canExport;

        public async void CreateBatchEditAndImportVouchers()
        {

            Status = new StatusInfo()
            {
                StatusMessage = "creating voucher batch",
                IsBusy = true
            };


            try
            {
                List<VoucherImport> vouchers = VoucherImportWrappers.Where(x => x.AltAddress.OkComplete).Select(VoucherImportWrapperConverter.ToVoucherImport).ToList();
                var batchNum = await BatchSvc.ImportVouchers(vouchers);
                Status = new StatusInfo()
                {
                    StatusMessage = "batch created"
                };
                CanSave = false;
                SaveBatchInfoMessage = String.Format("batch {0} has been created", batchNum);

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

