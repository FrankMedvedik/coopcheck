using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinqToExcel;
using Microsoft.Office.Interop.Excel;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Vouchers
{

    internal class ImportViewModel : ViewModelBase
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

        public ImportViewModel()
        {
            ResetState();
            StatusMsg = "Select an Study Honoraria Excel Workbook";
            _selectedWorkSheet = DefaultSelected;
        }

        #region DisplayState
        private string _statusMsg;
        private string _errorMsg;
        private bool _canProceed;

        private void ResetState()
        {
            SrcColumnNames = new ObservableCollection<string>();
            ColumnMap = new ObservableCollection<ColumnPropertyMap>();
            StatusMsg = "";
            ErrorMsg = "";
            CanProceed = false;
            ShowGridData = false;
        }
        public string StatusMsg
        {
            get { return _statusMsg; }
            set
            {
                _statusMsg = value;
                NotifyPropertyChanged();
            }
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

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
                NotifyPropertyChanged();
            }
        }

        private Boolean _showGridData;

        public Boolean ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
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
                loadWorksheetMetaData();
            }
        }


        public void loadWorksheetMetaData()
        {
            ExcelBook = new ExcelQueryFactory(ExcelFilePath);
            WorkSheets = new ObservableCollection<string>(ExcelBook.GetWorksheetNames());
        }
        private ObservableCollection<string> _workSheets = new ObservableCollection<string>();

        public ObservableCollection<string> WorkSheets
        {
            get { return _workSheets; }
            set
            {
                _workSheets = value;
                NotifyPropertyChanged();
                StatusMsg = "Select a worksheet";
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

        //public Microsoft.Office.Interop.Excel.Workbook ExcelBook;
        //public Microsoft.Office.Interop.Excel.Worksheet SelectedExcelSheet;

        private string _selectedWorkSheet = null;

        public string SelectedWorkSheet
        {
            get { return _selectedWorkSheet; }
            set
            {
                _selectedWorkSheet = value;
                NotifyPropertyChanged();
                LoadWorkSheetData();
            }
        }


        public void LoadWorkSheetData()
        {
            ResetState();
            LoadWorkSheetStats();
            ImportVouchers();
        }

        public void LoadWorkSheetStats()
        {
            StatusMsg = "Loading columns...";
            try
            {
                if (SelectedWorkSheet == DefaultSelected) return;
                SrcColumnNames = new ObservableCollection<string>(ExcelBook.GetColumnNames(SelectedWorkSheet));
                ColumnNameValidator();
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                StatusMsg = "Error reading column names does selected worksheet contain vouchers?";
                VoucherCnt = 0;
            }
        }

        private string DefaultSelected = "< Choose Worksheet >";

        public void ColumnNameValidator()
        {

            if (SrcColumnNames == null)
            {
                ErrorMsg = "No valid Columns found";
                StatusMsg = "Cannot Import";
                VoucherCnt = 0;
                SrcColumnNames = new ObservableCollection<string>();
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
                ErrorMsg = "No valid Columns found";
                StatusMsg = "Cannot Import";
                VoucherCnt = 0;
                ShowGridData = true;
            }
            else if (ColumnMap.Any(x => x.Available == false))
            {
                StatusMsg = "Cannot Import";
                ErrorMsg = "Some required columns were not found ";
                VoucherCnt = 0;
                ShowGridData = true;
            }
            else
            {
                StatusMsg = "Click Next to continue";
                ErrorMsg = "";
                ShowGridData = false;
                CanProceed = true;
            }
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
            var vouchers = from a in ExcelBook.Worksheet<VoucherImport>(SelectedWorkSheet) select a;

            foreach (var a in vouchers)
            {
                VoucherImports.Add(a);
            }

            VoucherCnt = VoucherImports.Count();
            VoucherTotalDollars = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
        }
        private decimal _voucherTotalDollars;
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

    }
}