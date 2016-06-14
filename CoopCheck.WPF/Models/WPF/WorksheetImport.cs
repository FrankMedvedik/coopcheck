using System.Collections.Generic;
using CoopCheck.WPF.Contracts.Interfaces;


namespace CoopCheck.WPF.Models
{
    public class WorksheetImport : IWorksheetImport
    {
        public string WorkbookFilename { get; set; }
        public List<IVoucherImport> VoucherImports { get; set; }

    }
}