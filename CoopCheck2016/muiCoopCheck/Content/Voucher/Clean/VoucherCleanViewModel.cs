using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using CoopCheck.WPF.Wrappers;
using DataClean.Models;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class VoucherCleanViewModel : ViewModelBase
    {
        private StatusInfo _status;
        private DataCleanCriteria  _dataCleanCriteria = null;
        private List<VoucherImport>  _vouchers = null;

        public VoucherCleanViewModel()
        {
            Messenger.Default.Register<NotificationMessage<DataCleanCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.DataCleanCriteriaUpdated )
                {
                    CanDataClean = true;
                    _dataCleanCriteria = message.Content;
                }
            });

            Messenger.Default.Register<NotificationMessage<ExcelVouchersMessage>>(this, message =>
            {
                if (message.Notification == Notifications.ImportWorksheetReady && message.Content.VoucherImports.Any())
                {
                    _excelFileInfo = message.Content.ExcelFileInfo;
                    _dataCleanCriteria = _dataCleanCriteria ?? new DataCleanCriteria()
                    {
                        AutoFixAddressLine1 = true,
                        AutoFixCity = true,
                        AutoFixPostalCode = true,
                        AutoFixState = true,
                        ForceValidation = false
                    };
                    _vouchers = message.Content.VoucherImports;
                    CleanTheVouchers();
                }
            });

        }

        private ExcelFileInfoMessage _excelFileInfo;
        public bool CanDataClean
        {
            get { return _canDataClean; }
            set { _canDataClean = value;
                NotifyPropertyChanged(); }
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

        public async void CleanTheVouchers()
        {
            throw new NotImplementedException();
            //CanDataClean = false;
            //try
            //{
            //    CleanVouchers = new ObservableCollection<VoucherImportWrapper>(
            //        await DataCleanVoucherImportSvc.CleanVouchers(_vouchers, _dataCleanCriteria, _excelFileInfo));
            //}
            //catch (Exception e)
            //{
            //    Status = new StatusInfo()
            //    {
            //        StatusMessage = String.Format("Error during validation"),
            //        ErrorMessage = e.Message
            //    };
            //}

        }


        private ObservableCollection<VoucherImportWrapper> _cleanVouchers  = new ObservableCollection<VoucherImportWrapper>();
        private bool _canDataClean;

        public ObservableCollection<VoucherImportWrapper> CleanVouchers
        {
            get { return _cleanVouchers; }
            set
            {
                _cleanVouchers = value;
                NotifyPropertyChanged();
            }
        }
       

    }
}
