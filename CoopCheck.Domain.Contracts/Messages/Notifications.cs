namespace CoopCheck.Domain.Contracts.Messages
{
    public static class Notifications
    {

        public static string GlobalReportCriteriaChanged => "GlobalReportCriteriaChanged";
        public static string DateRangeChanged => "DateRangeChanged";
        public static string StatusInfoChanged => "StatusInfoChanged";
        public static string ImportWorksheetReady => "ImportWorksheetDone";
        public static string OpenBatchChanged => "OpenBatchChanged";
        public static string RefreshOpenBatchList => "RefreshOpenBatchList";
        public static string PaymentReportCriteriaChanged => "PaymentReportCriteriaChanged";
        public static string SelectedJobChanged => "SelectedJobChanged";
        public static string SelectedBatchChanged => "SelectedBatchChanged";
        public static string SelectedAccountChanged => "SelectedAccountChanged";
        public static string ReconcileBankFileLoaded => "ReconcileBankFileLoaded";
        public static string ReconcileAccountPaymentsLoaded => "ReconcileAccountPaymentsLoaded";
        public static string HonorariaWorksheetImportComplete => "HonorariaWorksheetImportComplete";
        public static string ShowPopupStatus => "ShowPopupStatus";
        public static string NewDataCleanEvents => "NewDataCleanEvents";
        public static string DataCleanCriteriaUpdated => "DataCleanCriteriaUpdated";
        public static string VouchersDataCleaned => "VouchersDataCleaned";
        public static string HaveReviewedVouchers => "HaveReviewedVouchers";
        public static string HaveCommittedVouchers => "HaveCommittedVouchers";
        public static string FindChecks => "FindChecks";
        public static string PaymentReportCriteriaCheckNumberChanged => "PaymentReportCriteriaCheckNumberChanged";
        public static string BankAccountReconcileWizardCanFinish => "BankAccountReconcileWizardCanFinish";
        public static string HaveDirtyVouchers => "HaveDirtyVouchers";
        public static string JobFinderSelectedJobChanged => "JobFinderSelectedJobChanged";
        public static string JobFinderSelectedBatchChanged => "JobFinderSelectedBatchChanged";
        public static string HaveUncommittedVouchers => "HaveUncommittedVouchers";
        public static string RefreshPaymentSummaryJobs => "RefreshPaymentSummaryJobs";
        public static string FindPayeePayments => "FindPayeePayments";
    }
}