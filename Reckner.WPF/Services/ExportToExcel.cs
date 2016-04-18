using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;

namespace Reckner.WPF.Services
{
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        private _Workbook _book;
        private Workbooks _books;
        private readonly Dictionary<string, string> _columnNameDictionary = buildColumnNameDictionary();
        // Excel object references.
        private Application _excelApp;
        private Font _font;
        private readonly List<string> _numberColumnsToDisplayAsText = buildNumberColumnList();

        // Optional argument variable
        private readonly object _optionalValue = Missing.Value;
        private Range _range;
        private _Worksheet _sheet;
        private Sheets _sheets;
        public List<T> dataToPrint;
        public string ExcelWorksheetName { get; set; } = null;
        public string ExcelSourceWorkbook { get; set; } = null;

        private static List<string> buildNumberColumnList()
        {
            var l = new List<string>();
            l.Add("ZIPCODE");
            l.Add("PHONE");
            l.Add("PostalCode");
            l.Add("PhoneNumber");
            return l;
        }


        /// <summary>
        ///     Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        buildColumnNameDictionary();
                        FillSheet();
                        _book.SaveAs();
                        _excelApp.Quit();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error while generating Excel report");
                throw;
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                if (_books != null) ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }

        /// <summary>
        ///     Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            var header = CreateHeader();
            WriteData(header);
        }

        /// <summary>
        ///     Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            var objData = new object[dataToPrint.Count, header.Length];

            for (var j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (var i = 0; i < header.Length; i++)
                {
                    var y = typeof (T).InvokeMember(header[i].ToString(),
                        BindingFlags.GetProperty, null, item, null);
                    objData[j, i] = y?.ToString() ?? "";
                }
            }
            SetColumnDataTypes(header);
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
            FixColumnHeadings("A1", header.Length);
        }

        private void SetColumnDataTypes(object[] header)
        {
            for (var i = 0; i < header.Length; i++)
            {
                if (_numberColumnsToDisplayAsText.Contains(header[i].ToString()))
                {
                    // account for zero to one array offset and add that we are working with row one 
                    var s = GetExcelColumnName(i + 1) + "1";
                    SetExcelColumnDataType(s);
                }
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1)%26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo)/26;
            }

            return columnName;
        }

        private void SetExcelColumnDataType(string columnName)
        {
            _range = _sheet.get_Range(columnName).EntireColumn;
            _range.NumberFormat = "@";
        }

        private void FixColumnHeadings(string startRange, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(1, colCount);
            foreach (var c in _columnNameDictionary)
            {
                var b = _range.Replace(c.Key, c.Value, XlLookAt.xlWhole, XlSearchOrder.xlByColumns);
            }
        }

        /// <summary>
        ///     Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }

        private string GetColumnName(string classPropertyName)
        {
            var returnValue = string.Empty;
            if (_columnNameDictionary.TryGetValue(classPropertyName, out returnValue))
                return returnValue;
            return classPropertyName;
        }


        private static Dictionary<string, string> buildColumnNameDictionary()
        {
            var d = new Dictionary<string, string>();
            d.Add("First", "FIRST NAME");
            d.Add("Last", "LAST NAME");
            d.Add("AddressLine1", "ADDRESS 1");
            d.Add("AddressLine2", "ADDRESS 2");
            d.Add("Municipality", "CITY");
            d.Add("Region", "STATE");
            d.Add("PostalCode", "ZIPCODE");
            d.Add("PhoneNumber", "PHONE");
            d.Add("Amount", "AMOUNT");
            d.Add("EmailAddress", "E-MAIL");
            d.Add("JobNumber", "JOB #");
            return d;
        }

        /// <summary>
        ///     Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            var headerInfo = typeof (T).GetProperties();
            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            var objHeaders = new List<object>();
            for (var n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
                //objHeaders.Add(GetColumnName(headerInfo[n].Name));
            }
            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();
            return headerToAdd;
        }

        /// <summary>
        ///     Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }

        /// <summary>
        ///     Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount,
            int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }

        /// <summary>
        ///     Create Excel application parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Application();
            // _excelApp.Visible = true;
            _excelApp.DisplayAlerts = false;
            _book = _excelApp.Workbooks.Add();
                _sheets = _book.Worksheets;
                _sheet = (_sheets.Add());
                if (ExcelWorksheetName != null)
                    _sheet.Name = ExcelWorksheetName;
        }

        /// <summary>
        ///     Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}