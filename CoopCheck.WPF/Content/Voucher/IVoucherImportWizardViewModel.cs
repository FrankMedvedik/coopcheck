using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher
{
    public interface IVoucherImportWizardViewModel
    {
        bool HaveCleanedVouchers { get; set; }
        bool HaveCommittedVouchers { get; set; }
        bool HaveReviewedVouchers { get; set; }
        bool HaveValidWorkbook { get; set; }
        StatusInfo Status { get; set; }
    }
}