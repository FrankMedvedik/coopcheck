using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.Wrappers;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using LinqToExcel;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Import
{
    public class ImportWorksheetViewModel : ViewModelBase, IDataErrorInfo
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();

        public ImportWorksheetViewModel()
        {
            ResetAll();
            ImportWorksheetCommand = new RelayCommand(LoadWorksheetData, CanImportWorkSheet);
            PostVouchersCommand = new RelayCommand(PostVouchers, CanPostVouchers);
            _voucherImporter = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = false
            };
            _voucherImporter.DoWork += worker_DoWork;
            _voucherImporter.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        public ObservableCollection<VoucherImport> VoucherImports
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

        public RelayCommand ImportWorksheetCommand { get; private set; }

        public RelayCommand PostVouchersCommand { get; private set; }

        private void ResetAll()
        {
            ResetState();
            ExcelFilePath = DefaultWorkbook;
            SelectedWorksheet = DefaultSelectedWorksheet;
            Status = new StatusInfo
            {
                StatusMessage = DefaultWorkbook
            };
        }

        public bool CanImportWorkSheet()
        {
            return CanImport;
        }

        private void LoadWorksheetData()
        {
            Status = new StatusInfo
            {
                StatusMessage = "loading vouchers and checking address...",
                IsBusy = true
            };
            if (!_voucherImporter.IsBusy)
                _voucherImporter.RunWorkerAsync();
            StartEnabled = !_voucherImporter.IsBusy;
        }

     
        public void PostVouchers()
        {
            try
            {
                  Messenger.Default.Send(new NotificationMessage<VoucherWrappersMessage>(
                    new VoucherWrappersMessage
                    {
                        ExcelFileInfo = new ExcelFileInfoMessage
                        {
                            ExcelFilePath = ExcelFilePath,
                            SelectedWorksheet = SelectedWorksheet
                        },
                        VoucherImports = new ObservableCollection<VoucherImportWrapper>(VoucherImports.Select(v => new VoucherImportWrapper(v)))
            },
                    Notifications.VouchersDataCleaned));
                Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
                Messenger.Default.Send(new NotificationMessage(Notifications.ImportWorksheetReady));
                CanProceed = true;
            }
            catch (Exception e)
            {

                log.Error(string.Format("cleaning the vouchers failed - {0} - {1} ", e.Message,
                                     e.InnerException?.Message));
                string errorMessage = string.Format("error cleaning vouchers: {0}", e.Message);
                ModernDialog.ShowMessage(errorMessage, "Cleaning vouchers failed", MessageBoxButton.OK);

            }
        }

        //public async void CleanVouchers(List<VoucherImportWrapper> vouchers)
        //{
        //    Messenger.Default.Send(new NotificationMessage(Notifications.HaveUncommittedVouchers));
        //    Messenger.Default.Send(new NotificationMessage(Notifications.HaveDirtyVouchers));
        //    Messenger.Default.Send(new NotificationMessage(Notifications.ImportWorksheetReady));
        //    Status = new StatusInfo
        //    {
        //        StatusMessage =
        //            "please wait  - checking the street addresses, email and phone numbers of the vouchers...",
        //        IsBusy = true
        //    };
        //    var results = await DataCleanVoucherImportSvc.CleanVouchers(vouchers);
           
        //    Messenger.Default.Send(new NotificationMessage<VoucherWrappersMessage>(
        //        new VoucherWrappersMessage
        //        {
        //            ExcelFileInfo = new ExcelFileInfoMessage
        //            {
        //                ExcelFilePath = ExcelFilePath,
        //                SelectedWorksheet = SelectedWorksheet
        //            },
        //            VoucherImports = results
        //        },
        //        Notifications.VouchersDataCleaned));
        //}
        public bool CanPostVouchers()
        {
            return true;
        }

        #region DisplayState

        private bool _canProceed;

        public void ResetState()
        {
            SrcColumnNames = new ObservableCollection<string>();
            ColumnMap = new ObservableCollection<ColumnPropertyMap>();
            CanProceed = false;
            ShowColumnErrorData = false;
            CanImport = true;
        }

        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanImport
        {
            get { return _canImport; }
            set
            {
                _canImport = value;
                NotifyPropertyChanged();
            }
        }

        private bool _showColumnErrorData;

        public bool ShowColumnErrorData
        {
            get { return _showColumnErrorData; }
            set
            {
                _showColumnErrorData = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Workbook

        public readonly string[] ValidColumnNames =
        {
            "FIRST NAME", "LAST NAME", "ADDRESS 1", "ADDRESS 2", "CITY",
            "STATE", "ZIPCODE", "PHONE", "FAX", "E-MAIL",
            "JOB #", "AMOUNT"
        };

        public string ExcelFilePath
        {
            get { return _excelFilePath; }
            set
            {
                _excelFilePath = value;
                NotifyPropertyChanged();
                if (_excelFilePath != DefaultWorkbook)
                {
                    ResetState();
                    LoadWorksheetMetaData();
                }
            }
        }

        private readonly string DefaultWorkbook = "Select an excel workbook";

        public void LoadWorksheetMetaData()
        {
            var s = new StatusInfo();

            if (File.Exists(ExcelFilePath))
                try
                {
                    ExcelBook = new ExcelQueryFactory(ExcelFilePath);
                    WorkSheets = new ObservableCollection<string>(ExcelBook.GetWorksheetNames());
                    SelectedWorksheet = WorkSheets.First();
                }
                catch (Exception e)
                {
                    s.ErrorMessage = string.Format("File could not be processed {0}", e.Message);
                }
            else
                s.ErrorMessage = string.Format("File Not Found {0}", ExcelFilePath);

            Status = s;
        }

        private ObservableCollection<string> _workSheets = new ObservableCollection<string>();

        public ObservableCollection<string> WorkSheets
        {
            get { return _workSheets; }
            set
            {
                _workSheets = value;
                NotifyPropertyChanged();
                var s = new StatusInfo
                {
                    StatusMessage = DefaultWorkbook,
                    ErrorMessage = ""
                };
                Status = s;
            }
        }

        private ObservableCollection<string> _srcColumnNames = new ObservableCollection<string>();

        public ObservableCollection<string> SrcColumnNames
        {
            get { return _srcColumnNames; }
            set
            {
                _srcColumnNames = value;

                foreach (var s in _srcColumnNames)
                    if (s == null)
                        _srcColumnNames.Remove(s);
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ColumnPropertyMap> _columnMap = new ObservableCollection<ColumnPropertyMap>();

        public ObservableCollection<ColumnPropertyMap> ColumnMap
        {
            get { return _columnMap; }
            set
            {
                _columnMap = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("FilteredColumnMap");
            }
        }

        public ObservableCollection<ColumnPropertyMap> FilteredColumnMap
        {
            get { return new ObservableCollection<ColumnPropertyMap>(ColumnMap.Where(x => x.Available == false)); }
        }

        private ExcelQueryFactory ExcelBook;
        private string _selectedWorksheet;

        public string SelectedWorksheet
        {
            get { return _selectedWorksheet; }
            set
            {
                if (value == null || value == DefaultSelectedWorksheet) return;
                ResetState();
                _selectedWorksheet = value;
                NotifyPropertyChanged();
            }
        }


        public void LoadWorkSheetData()
        {
            if (SelectedWorksheet != DefaultSelectedWorksheet)
            {
                try
                {
                    LoadWorksheetStats();
                    ImportVouchers();
                    //Status = new StatusInfo()
                    //{
                    //    StatusMessage = "select import to load this worksheet"
                    //};
                }
                catch (Exception e)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = "Worksheet import failed",
                        ErrorMessage = e.Message
                    };
                }
            }
        }

        public void LoadWorksheetStats()
        {
            Status = new StatusInfo
            {
                StatusMessage = "Loading columns...",
                IsBusy = true,
                ErrorMessage = ""
            };
            try
            {
                SrcColumnNames = new ObservableCollection<string>(ExcelBook.GetColumnNames(SelectedWorksheet));
                ColumnNameValidator();
                Status = new StatusInfo
                {
                    StatusMessage = "",
                    ErrorMessage = ""
                };
            }
            catch (Exception ex)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error reading column names does selected worksheet contain vouchers?",
                    ErrorMessage = ex.Message
                };
            }
        }


        private readonly string DefaultSelectedWorksheet = "< Choose Worksheet >";

        public void ColumnNameValidator()
        {
            var s = new StatusInfo();

            if (SrcColumnNames == null)
            {
                s.ErrorMessage = "No valid Columns found";
                s.StatusMessage = "Cannot Import";
                Status = s;
                VoucherCnt = 0;
                return;
            }

            var found = false;
            foreach (var t in ValidColumnNames)
            {
                for (var x = 0; x < SrcColumnNames.Count; x++)
                {
                    if (t == SrcColumnNames[x])
                    {
                        ColumnMap.Add(new ColumnPropertyMap
                        {
                            ColumnName = t,
                            Available = true,
                            ColumnOffset = x
                        });
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    ColumnMap.Add(new ColumnPropertyMap
                    {
                        ColumnName = t,
                        Available = false
                    });
                }
            }

            if (!ColumnMap.Any(x => x.Available))
            {
                s.ErrorMessage = "No valid Columns found";
                s.StatusMessage = "Cannot Import";
                VoucherCnt = 0;
                ShowColumnErrorData = true;
            }
            else if (ColumnMap.Any(x => x.Available == false))
            {
                s.StatusMessage = "Cannot Import";
                s.ErrorMessage = "Some required columns were not found ";
                VoucherCnt = 0;
                ShowColumnErrorData = true;
            }
            else
            {
                s.StatusMessage = "Click Next to continue";
                s.ErrorMessage = "";
                ShowColumnErrorData = false;
                CanProceed = true;
            }
            Status = s;
        }


        private string _excelFilePath;

        private int _voucherCnt;

        public int VoucherCnt
        {
            get { return _voucherCnt; }
            set
            {
                _voucherCnt = value;
                NotifyPropertyChanged();
            }
        }

        public void ImportVouchers()
        {
            VoucherImports = new ObservableCollection<VoucherImport>();
            VoucherCnt = 0;
            VoucherTotalDollars = 0;

            try
            {
                if (!ShowColumnErrorData)
                {
                    ExcelBook.AddMapping("First", "FIRST NAME");
                    ExcelBook.AddMapping("Last", "LAST NAME");
                    ExcelBook.AddMapping("AddressLine1", "ADDRESS 1");
                    ExcelBook.AddMapping("AddressLine2", "ADDRESS 2");
                    ExcelBook.AddMapping("Municipality", "CITY");
                    ExcelBook.AddMapping("Region", "STATE");
                    ExcelBook.AddMapping("PostalCode", "ZIPCODE");
                    ExcelBook.AddMapping("PhoneNumber", "PHONE");
                    ExcelBook.AddMapping("Amount", "AMOUNT");
                    ExcelBook.AddMapping("EmailAddress", "E-MAIL");
                    ExcelBook.AddMapping("JobNumber", "JOB #");
                    var vouchers = from a in ExcelBook.Worksheet<VoucherImport>(SelectedWorksheet) select a;
                    foreach (var a in vouchers.Where(x => x.Last != "").Where(x => x.First != ""))
                    {
                        a.RecordID = Guid.NewGuid().ToString();
                        if (a.PostalCode != null && a.PostalCode.Length < 5)
                            a.PostalCode = a.PostalCode.PadLeft(5, '0');
                        VoucherImports.Add(a);
                    }

                    VoucherCnt = VoucherImports.Count();
                    VoucherTotalDollars = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                    CanProceed = true;
                }
            }
            catch (Exception e)
            {
                var s = new StatusInfo();
                s.ErrorMessage = e.Message;
                s.StatusMessage = "import failed";
                Status = s;
            }
        }

        private decimal _voucherTotalDollars;
        private StatusInfo _status;
        private bool _canImport;
        //private bool _isBusy;

        public decimal VoucherTotalDollars
        {
            get { return _voucherTotalDollars; }
            set
            {
                _voucherTotalDollars = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Validation

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ExcelFilePath")
                {
                    if (string.IsNullOrEmpty(ExcelFilePath)) return "Select an excel workbook with vouchers";
                    if (!File.Exists(ExcelFilePath)) return "Invalid file name";
                }
                if (columnName == "SelectedWorksheet")
                {
                    return string.IsNullOrEmpty(SelectedWorksheet) ? "Select a worksheet" : null;
                }
                return null;
            }
        }

        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                var s = new StatusInfo
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

        #endregion

        #region backgroundworker

        private readonly BackgroundWorker _voucherImporter;

        private bool _startEnabled = true;

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

        #endregion

        #region BackgroundWorker Events

        // Note: This event fires on the background thread.
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportVouchers();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartEnabled = !_voucherImporter.IsBusy;
            Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
            Messenger.Default.Send(new NotificationMessage(Notifications.ImportWorksheetReady));
            Status = new StatusInfo
            {
                StatusMessage = "vouchers loaded - select next to continue",
                IsBusy = false
            };
        }


        private bool _isCleaning = true;

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

        private bool _isSaving = true;

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                if (_isSaving != value)
                {
                    _isSaving = value;
                    NotifyPropertyChanged();
                }
            }

            #endregion
        }
    }
}