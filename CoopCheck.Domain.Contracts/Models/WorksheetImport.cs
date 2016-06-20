using System.Collections.Generic;
using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class WorksheetImport : IWorksheetImport
    {
        public string WorkbookFilename { get; set; }
        public List<IVoucherImport> VoucherImports { get; set; }

    }
}