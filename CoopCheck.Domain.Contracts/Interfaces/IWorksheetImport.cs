using System.Collections.Generic;

namespace CoopCheck.Domain.Contracts.Interfaces
{
    public interface IWorksheetImport
    {
        string WorkbookFilename { get; set; }
        List<IVoucherImport> VoucherImports { get; set; }
    }
}