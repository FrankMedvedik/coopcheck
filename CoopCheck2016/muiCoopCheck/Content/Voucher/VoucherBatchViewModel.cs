using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{
    public class VoucherBatchViewModel : Csla.Xaml.ViewModel<BatchEdit>
    {
        public VoucherBatchViewModel()
        {
            VoucherBatch = BatchEdit.GetBatchEdit(891);
            var s = new StatusInfo()
            {
                StatusMessage = "click select file to choose a new excel file to import voucher from",
                ErrorMessage = "No Errors"
            };

            Status = s;
            Messenger.Default.Register<NotificationMessage>(this, HandleNotification);
            Messenger.Default.Register<NotificationMessage<StatusInfo>>(this, message =>
            {
                Status = message.Content;
            });
        }

        private int _selectedBatchNum;
        public int SelectedBatchNum
        {
            get { return _selectedBatchNum; }
            set
            {
                _selectedBatchNum = value;
                //NotifyPropertyChanged();
                VoucherBatch = BatchEdit.GetBatchEdit(SelectedBatchNum);
            }
        }

        private void HandleNotification(NotificationMessage message)
        {
            if (message.Notification == Notifications.ImportCannotProceed)
            {
                CanProceed = false;
            }
            if (message.Notification == Notifications.ImportCanProceed)
            {
                CanProceed = true;
            }
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set
            {
                _canProceed = value;
                //NotifyPropertyChanged();
            }
        }


        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value; 
                //NotifyPropertyChanged();
            }
        }

        private StatusInfo _status;
        private bool _canProceed;

        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports
        {
            get { return _voucherImports; }
            set
            {
                _voucherImports = value;
                //NotifyPropertyChanged();
                if ((_voucherImports.Count > 0))
                {
                    try
                    {
                        VoucherBatch = BatchEdit.NewBatchEdit();
                        VoucherBatch.JobNum = int.Parse(VoucherImports.Select(x => x.JobNumber).First());
                    }
                    catch (Exception e)
                    {
                        VoucherBatch.JobNum = -1;
                    }

                    VoucherBatch.Amount = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                    VoucherBatch.Date = DateTime.Today.ToShortDateString();
                    VoucherBatch.Save();
                }
            }
        }

        public BatchEdit VoucherBatch;

        public void Load()
        {
            VoucherBatch = BatchEdit.GetBatchEdit(SelectedBatchNum);
        }
    }
}