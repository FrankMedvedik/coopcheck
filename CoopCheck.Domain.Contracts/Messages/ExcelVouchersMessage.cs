using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Contracts.Wrappers;

namespace CoopCheck.Domain.Contracts.Messages
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
