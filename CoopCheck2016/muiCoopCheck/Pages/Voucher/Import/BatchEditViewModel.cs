using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.DesignData;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Pages.Voucher.Import
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
                        SelectedBatch.JobNum = 0;    
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
            SelectedBatch = BatchEdit.NewBatchEdit();
            HeaderText = String.Format("Batch {0}", SelectedBatch.Num);
        }

        private bool _canProceed;

        private void ResetState()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "fill and verify the details about the voucher batch ",
                ErrorMessage = ""
            };
            Status = s;
            CanProceed = false;
            //ShowErrorData = false;
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set{
                _canProceed = value;
                NotifyPropertyChanged();
            }

        }

        //private Boolean _showErrorData;

        //public Boolean ShowErrorData
        //{
        //    get { return _showErrorData; }
        //    set
        //    {
        //        _showErrorData = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        private BatchEdit _selectedBatch;
        private StatusInfo _status;

        public BatchEdit  SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        private string _headerText;
        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                NotifyPropertyChanged();
            }
        }


    }
}