using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using LinqToExcel;

namespace CoopCheck.WPF.Content.Voucher.Import
{

    internal class ImportWorksheetViewModel : ViewModelBase, IDataErrorInfo
    {

        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports  {             
            get { return _voucherImports ; }
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

        public ImportWorksheetViewModel()
        {
            ResetState();
            SelectedWorksheet = DefaultSelectedWorksheet;
            Status = new StatusInfo()
            {
                StatusMessage = "Please select an study honoraria excel workbook",
                ErrorMessage = ""
            };


        }

        #region DisplayState
        private bool _canProceed = false;

        private void ResetState()
        {
            SrcColumnNames = new ObservableCollection<string>();
            ColumnMap = new ObservableCollection<ColumnPropertyMap>();
            CanProceed = false;
            ShowColumnErrorData = false;
            CanImport = true;
            VoucherImports = new ObservableCollection<VoucherImport>();
            VoucherCnt = 0;
            VoucherTotalDollars = 0;

            var s = new StatusInfo()
            {
                StatusMessage = "",
                ErrorMessage = ""
            };

            Status = s;
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
                NotifyPropertyChanged();
                //if (CanProceed)
                //    Messenger.Default.Send(this, new NotificationMessage(this, Notifications.ImportCanProceed));
                //else
                //    Messenger.Default.Send(this, new NotificationMessage(this, Notifications.ImportCannotProceed));
            }

        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
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
        private Boolean _showColumnErrorData;

        public Boolean ShowColumnErrorData
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
            "STATE","ZIPCODE", "PHONE", "FAX", "E-MAIL",
            "JOB #", "AMOUNT"
        };

        public string ExcelFilePath 
        {
            get { return _excelFilePath; }
            set
            {
                _excelFilePath = value;
                NotifyPropertyChanged();
                ResetState();
                LoadWorksheetMetaData();
            }
        }


        public void LoadWorksheetMetaData()
        {
            StatusInfo s = new StatusInfo();

            if(File.Exists(ExcelFilePath))
                try
                {
                    ExcelBook = new ExcelQueryFactory(ExcelFilePath);
                    WorkSheets = new ObservableCollection<string>(ExcelBook.GetWorksheetNames());
                    SelectedWorksheet = WorkSheets.First();
                }
                catch (Exception e)
                {
                    s.ErrorMessage = String.Format("File could not be processed {0}", e.Message);
                }
            else
                s.ErrorMessage = String.Format("File Not Found {0}", ExcelFilePath);

        }
        private ObservableCollection<string> _workSheets = new ObservableCollection<string>();

        public ObservableCollection<string> WorkSheets
        {
            get { return _workSheets; }
            set
            {
                _workSheets = value;
                NotifyPropertyChanged();
                var s = new StatusInfo()
            {
                StatusMessage = "Select a worksheet",
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

                foreach (string s in _srcColumnNames)
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

        public ExcelQueryFactory ExcelBook;
        private string _selectedWorksheet = null;

        public string SelectedWorksheet
        {
            get { return _selectedWorksheet; }
            set
            {
                ResetState();
                _selectedWorksheet = value;

                NotifyPropertyChanged();
                if (SelectedWorksheet == null || SelectedWorksheet == DefaultSelectedWorksheet) return;
                IsBusy = true;
                LoadWorkSheetData();
                IsBusy = false;
            }
        }


        public void LoadWorkSheetData()
        {
            if (SelectedWorksheet == DefaultSelectedWorksheet) return;
            LoadWorksheetStats();
            ImportVouchers();
        }

        public void LoadWorksheetStats()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "Loading columns...",
                IsBusy=true,
                ErrorMessage = ""
            };
            try
            {
                SrcColumnNames = new ObservableCollection<string>(ExcelBook.GetColumnNames(SelectedWorksheet));
                ColumnNameValidator();
                Status = new StatusInfo()
                {
                    StatusMessage = "",
                    ErrorMessage = ""
                };
            }
            catch (Exception ex)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error reading column names does selected worksheet contain vouchers?",
                    ErrorMessage = ex.Message
                };
            }

        }


        private string DefaultSelectedWorksheet = "< Choose Worksheet >";

        public void ColumnNameValidator()
        {
            var s = new StatusInfo();

            if (SrcColumnNames == null)
            {
                s.ErrorMessage= "No valid Columns found";
                s.StatusMessage = "Cannot Import";
                Status = s;
                VoucherCnt = 0;
                return;
            }

            bool found = false;
            foreach (string t in ValidColumnNames)
            {
                for (int x = 0; x < SrcColumnNames.Count; x++)
                {
                    if (t == SrcColumnNames[x])
                    {
                        ColumnMap.Add(new ColumnPropertyMap()
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
                    ColumnMap.Add(new ColumnPropertyMap()
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
                Messenger.Default.Send(this, new NotificationMessage(this, Notifications.ImportCanProceed));
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
            if (!ShowColumnErrorData)
            {
                ExcelBook.AddMapping("NamePrefix", "FIRST NAME");
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
                foreach (var a in vouchers.Where(x => x.Last != ""))
                {
                    VoucherImports.Add(a);
                }

                VoucherCnt = VoucherImports.Count();
                VoucherTotalDollars = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                CanProceed = true;
            }
        }
        private decimal _voucherTotalDollars;
        private StatusInfo _status;
        private bool _canImport;
        private bool _isBusy;

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

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ExcelFilePath")
                {
                    if(string.IsNullOrEmpty(ExcelFilePath)) return "Select an excel workbook with vouchers";
                    if(!File.Exists(ExcelFilePath)) return "Invalid file name";
                }
                if (columnName == "SelectedWorksheet")
                {
                    return string.IsNullOrEmpty(SelectedWorksheet)  ? "Select a worksheet" : null;
                }
                return null;
            }
        }
        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                StatusInfo s = new StatusInfo()
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

        public void CreateVoucherBatch()
        {
            CanImport = false;
        }
    }
}