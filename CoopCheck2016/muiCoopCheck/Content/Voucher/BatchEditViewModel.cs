using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{

    public class BatchEditViewModel : ViewModelBase
    {
        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports  {             
            get { return _voucherImports ; }
            set
            {
                _voucherImports = value;
                NotifyPropertyChanged();
                if ((_voucherImports.Count > 0) )
                {
                    try
                    {
                        SelectedBatch.JobNum = int.Parse(VoucherImports.Select(x => x.JobNumber).First());
                    }
                    catch (Exception e)
                    {
                        SelectedBatch.JobNum = -1;    
                    }

                    SelectedBatch.Amount = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                    SelectedBatch.Date = DateTime.Today.ToShortDateString();
                    SelectedBatch.Save();
                }
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

        public BatchEditViewModel()
        {
            ResetState();
            //SelectedBatch = BatchEdit.NewBatchEdit();
            
            Messenger.Default.Register<NotificationMessage<OpenBatch>>(this, message =>
            {
                BatchNum = message.Content.batch_num;
            });
        }

        private void ResetState()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "fill and verify the details about the voucher batch ",
                ErrorMessage = ""
            };
            Status = s;
        }

        private BatchEdit _selectedBatch;/* = BatchEdit.NewBatchEdit();*/
        private StatusInfo _status;

        public BatchEdit  SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(HeaderText);
            }
        }

        public int BatchNum
        {
            get { return SelectedBatch != null ? SelectedBatch.Num : 0; }
            set
            {
                SelectedBatch  =  (BatchNum == -1) ? SelectedBatch = BatchEdit.NewBatchEdit(): BatchEdit.GetBatchEdit(value);
                NotifyPropertyChanged();
            }
        }
        public string HeaderText
        {
            get
            {
                if (SelectedBatch != null)
                    return string.Format("Batch Number {0} Job Number {1}  Total Amount {2}",
                        SelectedBatch.Num, SelectedBatch.JobNum, SelectedBatch.Amount);
                return "";
            }
        }
    }
}