namespace CoopCheck.WPF.Messages
{
    public static class Notifications
    {

        public static string GlobalReportCriteriaChanged
        {
            get { return "GlobalReportCriteriaChanged"; }
        }

        public static string DateRangeChanged
        {
            get { return "DateRangeChanged"; }
        }

        public static string StatusInfoChanged
        {
            get { return "StatusInfoChanged"; }
        }

        public static string ImportWorksheetReady
        {
            get { return "ImportWorksheetDone"; }
        }

        public static string OpenBatchChanged
        {
            get { return "OpenBatchChanged"; }
        }

        public static string RefreshOpenBatchList
        {
            get { return "RefreshOpenBatchList"; }
        }

        public static string PaymentReportCriteriaChanged
        {
            get { return "PaymentReportCriteriaChanged"; }
        }

        public static string SelectedJobChanged
        {
            get { return "SelectedJobChanged"; }
        }

        public static string SelectedBatchChanged
        {
            get { return "SelectedBatchChanged"; }
        }

        public static string SelectedAccountChanged
        {
            get { return "SelectedAccountChanged"; }
        }

        public static string ReconcileBankFileLoaded
        {
            get { return "ReconcileBankFileLoaded"; }
        }
        public static string ReconcileAccountPaymentsLoaded
        {
            get { return "ReconcileAccountPaymentsLoaded"; }
        }
        public static string HonorariaWorksheetImportComplete
        {
            get { return "HonorariaWorksheetImportComplete"; }
        }

        public static string ShowPopupStatus
        {
            get { return "ShowPopupStatus"; }
        }

        public static string NewDataCleanEvents { get { return "NewDataCleanEvents"; } }

        public static string DataCleanCriteriaUpdated
        {
            get { return "DataCleanCriteriaUpdated"; }
        }

        public static string VouchersDataCleaned { get { return "VouchersDataCleaned"; } }
        public static string HaveReviewedVouchers { get { return "HaveReviewedVouchers"; } }
        public static string HaveCommittedVouchers { get { return "HaveCommittedVouchers"; } }
        public static string FindChecks { get { return "FindChecks"; } }
        public static string PaymentReportCriteriaCheckNumberChanged { get { return "PaymentReportCriteriaCheckNumberChanged"; } }
        public static string BankAccountReconcileWizardCanFinish { get { return "BankAccountReconcileWizardCanFinish"; } }
    }
}