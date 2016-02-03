using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher
{
    public class VoucherImportWizardViewModel : ViewModelBase
    {
        public VoucherImportWizardViewModel()
        {
            ResetAll();
            Messenger.Default.Register<NotificationMessage<ExcelVouchersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.ImportWorksheetReady)
                {
                    HaveValidWorkbook = true;
                }
            });

            Messenger.Default.Register<NotificationMessage<VoucherWrappersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.VouchersDataCleaned)
                {
                    HaveCleanedVouchers = true; HaveReviewedVouchers = true;
                }

                //if (message.Notification == Notifications.HaveReviewedVouchers)
                //{
                //    HaveReviewedVouchers = true;
                //}
            });

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (message.Notification == Notifications.HaveCommittedVouchers)
                {
                    HaveCommittedVouchers = true;
                }

                if (message.Notification == Notifications.HaveDirtyVouchers)
                {
                    HaveCleanedVouchers = false;
                }
                //if (message.Notification == Notifications.HaveReviewedVouchers)
                //{
                //    HaveReviewedVouchers = true;
                //}
            });


        }

        private void ResetAll()
        {
            HaveValidWorkbook = false;
            HaveCleanedVouchers = false;
            HaveReviewedVouchers = false;
            HaveCommittedVouchers = false;
        }
        
        private StatusInfo _status;
        
        private bool _haveValidWorkbook;

        public bool HaveValidWorkbook
        {
            get { return _haveValidWorkbook; }
            set { _haveValidWorkbook = value; NotifyPropertyChanged(); }
        }
        private bool _haveCleanedVouchers;

        public bool HaveCleanedVouchers
        {
            get { return _haveCleanedVouchers; }
            set { _haveCleanedVouchers = value;
                NotifyPropertyChanged();
            }
        }

        private bool _haveReviewedVouchers;
        public bool HaveReviewedVouchers
        {
            get { return _haveReviewedVouchers; }
            set
            {
                _haveReviewedVouchers = value;
                NotifyPropertyChanged();
            }
        }

        private bool _haveCommittedVouchers;
        public bool HaveCommittedVouchers
        {
            get { return _haveCommittedVouchers; }
            set
            {
                _haveCommittedVouchers = value;
                NotifyPropertyChanged();
            }
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }


    }

 
}
