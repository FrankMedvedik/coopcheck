using System.Collections.Generic;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF
{
    public class WorksheetImport
    {
        public string WorkbookFilename { get; set; }
        public List<VoucherImport> VoucherImports { get; set; }

    }
}