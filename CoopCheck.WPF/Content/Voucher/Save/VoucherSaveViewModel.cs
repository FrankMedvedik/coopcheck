using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using CoopCheck.Domain.Contracts.Converters;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Wrappers;
using CoopCheck.Domain.Models;
using CoopCheck.Domain.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Save
{
    public class Vouchers : List<VoucherExcelExport>
    {
    }

    public class VoucherSaveViewModel : ViewModelBase, IVoucherSaveViewModel
    {
        private readonly BackgroundWorker _batchCreator;
        private bool _canExport;
        private bool _canSave;

        private string _errorBatchInfoMessage;

        //public async void  ExportVouchers()
        //{
        //    Status = new StatusInfo()
        //    {
        //        StatusMessage = "exporting vouchers to Excel",
        //        IsBusy = true
        //    };

        //    await Task.Factory.StartNew(() =>
        //    {
        //        string dtFmt = String.Format("{0:g}", DateTime.Now).Replace('/', '.');
        //        dtFmt = dtFmt.Replace(':', '.');
        //        dtFmt = dtFmt.Replace(' ', '.');

        //        try
        //        {
        //            if (BadVoucherExports.Count > 0)
        //            {
        //                ExportToExcel<VoucherExcelExport, Vouchers> s = new ExportToExcel<VoucherExcelExport, Vouchers>
        //                {
        //                    ExcelSourceWorkbook = ExcelFileInfo.ExcelFilePath,
        //                    ExcelWorksheetName = String.Format("Errors.{0}", dtFmt),
        //                    dataToPrint = BadVoucherExports
        //                };
        //                s.GenerateReport();

        //                if (GoodVoucherExports.Count > 0)
        //                {
        //                    s.ExcelWorksheetName = String.Format("Processed.{0}", dtFmt);
        //                    s.dataToPrint = GoodVoucherExports;
        //                    s.GenerateReport();
        //                    CanExport = false;
        //                }
        //                ErrorBatchInfoMessage = String.Format("Vouchers Exported to {0}",
        //                    Path.GetFileName(ExcelFileInfo.ExcelFilePath));
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Status = new StatusInfo()
        //            {
        //                StatusMessage = "export to excel failed",
        //                ErrorMessage = e.Message //,ShowMessageBox = true
        //            };
        //        }
        //    });

        //    Status = new StatusInfo()
        //    {
        //        StatusMessage = "export complete",
        //    };
        //}

        private ExcelFileInfoMessage _excelVoucherInfo;

        private string _saveBatchInfoMessage;

        private bool _startEnabled = true;
        private StatusInfo _status;

        private ObservableCollection<VoucherImportWrapper> _voucherImportWrappers =
            new ObservableCollection<VoucherImportWrapper>();


        public VoucherSaveViewModel()
        {
            CanSave = false;
            CanExport = false;

            SaveVouchersCommand = new RelayCommand(CreateBatchEditAndImportVouchers, CanSaveVouchers);
            //ExportVouchersCommand = new RelayCommand(ExportVouchers, CanSaveVouchers);

            _batchCreator = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = false
            };
            _batchCreator.DoWork += worker_DoWork;
            _batchCreator.RunWorkerCompleted += worker_RunWorkerCompleted;

            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                    ExcelFileInfo = message.Content.ExcelFileInfo;
                    VoucherImportWrappers = message.Content.VoucherImports;
                    // Messenger.Default.Send(new NotificationMessage(Notifications.HaveCommittedVouchers));
                    if (GoodVouchers.Any()) CanSave = true;
                    if (BadVouchers.Any()) CanExport = true;
                }
            });
        }

        public RelayCommand SaveVouchersCommand { get; private set; }
        public RelayCommand ExportVouchersCommand { get; private set; }

        public ExcelFileInfoMessage ExcelFileInfo
        {
            get { return _excelVoucherInfo; }
            set
            {
                _excelVoucherInfo = value;
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

        public List<VoucherImportWrapper> BadVouchers => VoucherImportWrappers.Where(x => !x.OkMailingAddress).ToList();

        public List<VoucherImportWrapper> GoodVouchers => VoucherImportWrappers.ToList();

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
            set
            {
                _canSave = value;
                NotifyPropertyChanged();
            }
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

        public string SaveBatchInfoMessage
        {
            get { return _saveBatchInfoMessage; }
            set
            {
                _saveBatchInfoMessage = value;
                NotifyPropertyChanged();
            }
        }

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
            => GoodVouchers.Select(VoucherImportWrapperConverter.ToVoucherExcelExport).ToList();

        public List<VoucherExcelExport> BadVoucherExports
            => BadVouchers.Select(VoucherImportWrapperConverter.ToVoucherExcelExport).ToList();

        public bool StartEnabled
        {
            get { return _startEnabled; }
            set
            {
                if (_startEnabled != value)
                {
                    _startEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool CanSaveVouchers()
        {
            return StartEnabled;
        }

        private void CreateBatchEditAndImportVouchers()
        {
            Status = new StatusInfo
            {
                StatusMessage = "Saving vouchers in batch...",
                IsBusy = true
            };
            if (!_batchCreator.IsBusy)
                _batchCreator.RunWorkerAsync();
            StartEnabled = !_batchCreator.IsBusy;
            CanSave = false;
        }

        private void SetupMessages()
        {
            //SaveBatchInfoMessage = string.Format("8 Vouchers will be posted totalling $900");
            //ErrorBatchInfoMessage = string.Format("3 Vouchers WITH ERRORS totaling $300 will be saved to the job errors worksheet");
            //CanSave =true;

            SaveBatchInfoMessage = string.Format("{0} Vouchers will be posted totalling {1:C}",
                GoodVouchers.Count,
                GoodVouchers.Select(x => x.Amount).Sum());
            ErrorBatchInfoMessage = string.Format("{0} Vouchers with errors totalling {1:C} will be saved to Excel",
                BadVouchers.Count,
                BadVouchers.Select(x => x.Amount).Sum());

            CanSave = GoodVouchers.Any();
        }

        #region BackgroundWorker Events

        // Note: This event fires on the background thread.
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var x = new List<VoucherImport>();
                foreach (var a in GoodVouchers)
                    x.Add(VoucherImportWrapperConverter.ToVoucherImport(a));
                e.Result = BatchSvc.ImportVouchers(x);
                var b = BatchSvc.GetBatchEditAsync((int) e.Result).Result;
                b.Description = string.Format("{0} - {1}",
                    Path.GetFileName(ExcelFileInfo.ExcelFilePath).TrimEnd().TrimStart(),
                    ExcelFileInfo.SelectedWorksheet.TrimEnd().TrimStart());
                b.PayDate = null;
                var batch = b.Save();
            }
            catch (Exception ex)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "there was a problem creating the batch",
                    ErrorMessage = ex.Message,
                    IsBusy = false
                };
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SaveBatchInfoMessage = string.Format("Batch {0} created for {1} - {2}",
                e.Result,
                Path.GetFileName(ExcelFileInfo.ExcelFilePath).TrimEnd().TrimStart(),
                ExcelFileInfo.SelectedWorksheet.TrimEnd().TrimStart());
            StartEnabled = !_batchCreator.IsBusy;
            Messenger.Default.Send(new NotificationMessage(Notifications.HaveCommittedVouchers));
            Status = new StatusInfo
            {
                StatusMessage = SaveBatchInfoMessage,
                IsBusy = false
            };
        }

        #endregion
    }
}