using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Services;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Services;
using CoopCheck.WPF.Content.AccountManagement;
using CoopCheck.WPF.Content.AccountManagement.Open;
using CoopCheck.WPF.Content.AccountManagement.PositivePay;
using CoopCheck.WPF.Content.AccountManagement.Reconcile;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Batch;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using CoopCheck.WPF.Content.PaymentReports.Job;
using CoopCheck.WPF.Content.PaymentReports.PaymentFinder;
using CoopCheck.WPF.Content.PaymentReports.Summary;
using CoopCheck.WPF.Content.PaymentReports.Void;
using CoopCheck.WPF.Content.Settings;
using CoopCheck.WPF.Content.Status;
using CoopCheck.WPF.Content.Voucher;
using CoopCheck.WPF.Content.Voucher.Clean;
using CoopCheck.WPF.Content.Voucher.Edit;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Content.Voucher.Pay;
using CoopCheck.WPF.Content.Voucher.Save;
using GalaSoft.MvvmLight;
using Ninject;
using Ninject.Modules;

namespace CoopCheck.WPF.IOC
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

            // services

            //Bind<IBatchSvc>().To<BatchSvc>();
            //Bind<IClearCheckSvc>().To<ClearCheckSvc>();
            //Bind<IDataCleanVoucherImportSvc>().To<DataCleanVoucherImportSvc>();
            //Bind<IJobYearSvc>().To<JobYearSvc>();
            //Bind<IOutstandingBalancePrintSvc>().To<OutstandingBalancePrintSvc>();
            //Bind<IPaymentPrintSvc>().To<PaymentPrintSvc>();
            //Bind<IPaymentSvc>().To<PaymentSvc>();
            Bind<ISendMailSvc>().To<SendMailSvc>();
            Bind<ISwiftPaySvc>().To<SwiftPaySvc>();
            Bind<IUserAuthSvc>().To<UserAuthSvc>();
            Bind<IBankAccountSvc>().To<BankAccountSvc>();
            Bind<IClientSvc>().To<ClientSvc>();
            Bind<IJobLogSvc>().To<JobLogSvc>();
            Bind<IRptSvc>().To<RptSvc>();
            Bind<IOpenBatchSvc>().To<OpenBatchSvc>();
            //Bind<IPhoneNumberFormattingSvc>().To<PhoneNumberFormattingSvc>();
            //Bind<IHashHelperSvc>().To<HashHelperSvc>();






        }
    }

    public  class ViewModelLocator    
    {
        private static StandardKernel _kernel = null;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
                _kernel = new StandardKernel(new DesignTimeModule());
            }
            else
            {
                // Create run time view models
                _kernel = new StandardKernel(new RunTimeModule());
            }
        }



        public static IVoucherSaveViewModel VoucherSaveViewModelStatic
        {
            get
            {
                // Ask the kernel to give us an instance of IMainViewModel
                var model = _kernel.Get<IVoucherSaveViewModel>();
                return model;
            }
        }

        public IVoucherSaveViewModel VoucherSaveViewModel => _kernel.Get<IVoucherSaveViewModel>();
        public ISettingsAppearanceViewModel SettingsAppearanceViewModel => _kernel.Get<ISettingsAppearanceViewModel>();
        public IStatusViewModel StatusViewModel => _kernel.Get<IStatusViewModel>();
        public IVoucherImportWizardViewModel VoucherImportWizardViewModel => _kernel.Get<IVoucherImportWizardViewModel>();
        public IPaymentHistoryViewModel PaymentHistoryViewModel => _kernel.Get<IPaymentHistoryViewModel>();
        public IVoucherListViewModel VoucherListViewModel => _kernel.Get<IVoucherListViewModel>();
        public IBatchEditViewModel BatchEditViewModel => _kernel.Get<IBatchEditViewModel>();
        public IBatchListViewModel BatchListViewModel => _kernel.Get<IBatchListViewModel>();
        public IImportWorksheetViewModel ImportWorksheetViewModel => _kernel.Get<IImportWorksheetViewModel>();
        public IPayVouchersViewModel PayVouchersViewModel => _kernel.Get<IPayVouchersViewModel>();




        public IAccountViewModel AccountViewModel => _kernel.Get<IAccountViewModel>();
        public IOpenPaymentReportViewModel OpenPaymentReportViewModel  => _kernel.Get<IOpenPaymentReportViewModel>();
        public IPositivePayReportViewModel PositivePayReportViewModel  => _kernel.Get<IPositivePayReportViewModel>();
        public IAccountPaymentsViewModel AccountPaymentsViewModel => _kernel.Get<IAccountPaymentsViewModel>();
        public IBankFileViewModel BankFileViewModel => _kernel.Get<IBankFileViewModel>();
        public IBankTransactionViewModel BankTransactionViewModel => _kernel.Get<IBankTransactionViewModel>();
        public IReconcileBankViewModel ReconcileBankViewModel => _kernel.Get<IReconcileBankViewModel>();
        public IReconcileWizardViewModel ReconcileWizardViewModel => _kernel.Get<IReconcileWizardViewModel>();
        public IBatchReportViewModel BatchReportViewModel => _kernel.Get<IBatchReportViewModel>();
        public IBatchSummaryPaymentReportViewModel BatchSummaryPaymentReportViewModel => _kernel.Get<IBatchSummaryPaymentReportViewModel>();
        public IClientReportCriteriaViewModel ClientReportCriteriaViewModel => _kernel.Get<IClientReportCriteriaViewModel>();
        public IPaymentReportCriteriaViewModel PaymentReportCriteriaViewModel => _kernel.Get<IPaymentReportCriteriaViewModel>();
        public IJobReportViewModel JobReportViewModel => _kernel.Get<IJobReportViewModel>();
        public IJobSummaryPaymentReportViewModel JobSummaryPaymentReportViewModel => _kernel.Get<IJobSummaryPaymentReportViewModel>();
        public IPaymentFinderViewModel PaymentFinderViewModel => _kernel.Get<IPaymentFinderViewModel>();
        public IBatchViewModel BatchViewModel => _kernel.Get<IBatchViewModel>();
        public IClientPaymentViewModel ClientPaymentViewModel => _kernel.Get<IClientPaymentViewModel>();
        public IJobViewModel JobViewModel => _kernel.Get<IJobViewModel>();
        public IVoidedPaymentReportViewModel VoidedPaymentReportViewModel => _kernel.Get<IVoidedPaymentReportViewModel>();
        public IAboutViewModel AboutViewModel => _kernel.Get<IAboutViewModel>();
        public IAppearanceViewModel AppearanceViewModel => _kernel.Get<IAppearanceViewModel>();


    }

}
