using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF
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

        public static string ImportCannotProceed
        {
            get { return "ImportCannotProceed "; }
        }

        public static string ImportCanProceed
        {
            get { return "ImportCanProceed "; }
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
    }
}