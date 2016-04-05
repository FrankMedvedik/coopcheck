using System.Collections.Generic;

namespace CoopCheck.WPF.Models
{
    public class WorksheetImport
    {
        public string WorkbookFilename { get; set; }
        public List<VoucherImport> VoucherImports { get; set; }

    }
}