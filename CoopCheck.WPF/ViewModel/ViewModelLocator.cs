using CoopCheck.WPF.Content.BankAccount;
using CoopCheck.WPF.Content.BankAccount.Open;
using CoopCheck.WPF.Content.BankAccount.PositivePay;
using CoopCheck.WPF.Content.BankAccount.Reconcile;
using CoopCheck.WPF.Content.Payment.Batch;
using CoopCheck.WPF.Content.Payment.Criteria;
using CoopCheck.WPF.Content.Payment.Job;
using CoopCheck.WPF.Content.Payment.PaymentFinder;
using CoopCheck.WPF.Content.Payment.Summary;
using CoopCheck.WPF.Content.Payment.Void;
using CoopCheck.WPF.Content.Settings;
using CoopCheck.WPF.Content.Status;
using CoopCheck.WPF.Content.Voucher;
using CoopCheck.WPF.Content.Voucher.Clean;
using CoopCheck.WPF.Content.Voucher.Edit;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Content.Voucher.Pay;
using CoopCheck.WPF.Content.Voucher.Save;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight;
using Ninject;
using Ninject.Modules;

namespace CoopCheck.WPF.ViewModel
{

        public class DesignTimeModule : NinjectModule
        {
            public override void Load()
            {
      
            }
        }

        public class RunTimeModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IAccountViewModel>().To<AccountViewModel>();
                Bind<IOpenPaymentReportViewModel>().To<OpenPaymentReportViewModel>();
                Bind<IPositivePayReportViewModel>().To<PositivePayReportViewModel>();
                Bind<IAccountPaymentsViewModel>().To<AccountPaymentsViewModel>();
                Bind<IBankFileViewModel>().To<BankFileViewModel>();
                Bind<IBankTransactionViewModel>().To<BankTransactionViewModel>();
                Bind<IReconcileBankViewModel>().To<ReconcileBankViewModel>();
                Bind<IReconcileWizardViewModel>().To<ReconcileWizardViewModel>();
                Bind<IBatchReportViewModel>().To<BatchReportViewModel>();
                Bind<IBatchSummaryPaymentReportViewModel>().To<BatchSummaryPaymentReportViewModel>();
                Bind<IClientReportCriteriaViewModel>().To<ClientReportCriteriaViewModel>();
                Bind<IPaymentReportCriteriaViewModel>().To<PaymentReportCriteriaViewModel>();
                Bind<IJobReportViewModel>().To<JobReportViewModel>();
                Bind<IJobSummaryPaymentReportViewModel>().To<JobSummaryPaymentReportViewModel>();
                Bind<IPaymentFinderViewModel>().To<PaymentFinderViewModel>();
                Bind<IBatchViewModel>().To<BatchViewModel>();
                Bind<IClientPaymentViewModel>().To<ClientPaymentViewModel>();
                Bind<IJobViewModel>().To<JobViewModel>();
                Bind<IVoidedPaymentReportViewModel>().To<VoidedPaymentReportViewModel>();
                Bind<IAboutViewModel>().To<AboutViewModel>();
                Bind<IAppearanceViewModel>().To<AppearanceViewModel>();
                Bind<ISettingsAppearanceViewModel>().To<SettingsAppearanceViewModel>();
                Bind<IStatusViewModel>().To<StatusViewModel>();
                Bind<IVoucherImportWizardViewModel>().To<VoucherImportWizardViewModel>();
                Bind<IPaymentHistoryViewModel>().To<PaymentHistoryViewModel>();
                Bind<IVoucherListViewModel>().To<VoucherListViewModel>();
                Bind<IBatchEditViewModel>().To<BatchEditViewModel>();
                Bind<IBatchListViewModel>().To<BatchListViewModel>();
                Bind<IImportWorksheetViewModel>().To<ImportWorksheetViewModel>();
                Bind<IPayVouchersViewModel>().To<PayVouchersViewModel>();
                Bind<IVoucherSaveViewModel>().To<VoucherSaveViewModel>();
            }
        }
public class ViewModelLocator
{
    private static StandardKernel kernel = null;

    /// <summary>
    /// Initializes a new instance of the ViewModelLocator class.
    /// </summary>
    public ViewModelLocator()
    {
        if (ViewModelBase.IsInDesignModeStatic)
        {
            // Create design time view models
            kernel = new StandardKernel(new DesignTimeModule());
        }
        else
        {
            // Create run time view models
            kernel = new StandardKernel(new RunTimeModule());
        }
    }
}

}
