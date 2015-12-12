using System.Collections.ObjectModel;
using CoopCheck.WPF.Wrappers;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherWorkbookMessage
    {
        public string WorkbookName { get; set; }
        public string WorksheetName { get; set; }
        public ObservableCollection<VoucherImportWrapper> Vouchers { get; set; }
        public string Sender { get; set;}

    }
}