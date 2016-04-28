using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;

namespace CoopCheck.WPF.Messages
{
    public class ExcelVouchersMessage { 
        public List<VoucherImport> VoucherImports;
        public ExcelFileInfoMessage ExcelFileInfo;
        
    }

    public class ExcelFileInfoMessage
    {
        public string SelectedWorksheet;
        public string ExcelFilePath;
    }

    public class VoucherWrappersMessage
    {
        public ObservableCollection<VoucherImportWrapper> VoucherImports;
        public ExcelFileInfoMessage ExcelFileInfo;
    }

}
