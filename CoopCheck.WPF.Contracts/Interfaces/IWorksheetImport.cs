using System.Collections.Generic;

namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IWorksheetImport
    {
        string WorkbookFilename { get; set; }
        List<IVoucherImport> VoucherImports { get; set; }
    }
}