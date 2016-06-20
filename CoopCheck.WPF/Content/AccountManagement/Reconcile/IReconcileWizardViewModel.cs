namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IReconcileWizardViewModel
    {
        bool CanFinish { get; set; }
        bool HaveAutoReconciledAccount { get; set; }
        bool HaveValidAccount { get; set; }
        bool HaveValidBankFile { get; set; }
        StatusInfo Status { get; set; }
    }
}