using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Library;
using GalaSoft.MvvmLight.Messaging;
using LinqToExcel;
using Microsoft.Office.Interop.Excel;
using muiCoopCheck.DesignData;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Vouchers
{

    public class ValidateViewModel : ViewModelBase
    {

        private void FilterVouchers()
        {
            
        }

        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports  {             
            get { return _voucherImports ; }
            set
            {
                _voucherImports = value;
                NotifyPropertyChanged();

                if (SelectedBatch != null)
                {
                    SelectedBatch.JobNum = int.Parse(VoucherImports.Select(x => x.JobNumber).First()); 
                    SelectedBatch.Amount = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                    SelectedBatch.Date = DateTime.Today.ToShortDateString();
                    foreach (var v in VoucherImports.ToList())
                    {
                        
                        SelectedBatch.Vouchers.Add(v.ToVoucherEdit());
                    }
                    //SelectedBatch.Save();
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

        public ValidateViewModel()
        {
            ResetState();
            
        }

        #region DisplayState
        private bool _canProceed;

        private void ResetState()
        {
            CanProceed = false;
            ShowGridData = false;
            Status = new StatusInfo()
            {
                StatusMessage = "verify vouchers",
                ErrorMessage = ""
            };

            SelectedBatch = BatchEdit.NewBatchEdit();
        }

        public bool CanProceed
        {
            get { return _canProceed; }
            set{
                _canProceed = value;
                NotifyPropertyChanged();
            }

        }

        
        private Boolean _showGridData;

        public Boolean ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #region BatchEdit

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

        #endregion

    }
}