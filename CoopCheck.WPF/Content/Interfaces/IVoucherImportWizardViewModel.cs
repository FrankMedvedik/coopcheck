using CoopCheck.Domain.Contracts.Models;

namespace CoopCheck.WPF.Content.Interfaces
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