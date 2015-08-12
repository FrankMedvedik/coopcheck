using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LinqToExcel;
using Microsoft.Office.Interop.Excel;
using muiCoopCheck.DesignData;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Vouchers
{

    public class ValidateViewModel : ViewModelBase
    {

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
                }

            }
        }

        public ValidateViewModel()
        {
            ResetState();
            StatusMsg = "fill and verify the details about the voucher batch ";
        }

        #region DisplayState
        private bool _canProceed;
        private string _errorMsg;
        private string _statusMsg;

        private void ResetState()
        {
            StatusMsg = "";
            ErrorMsg = "";
            CanProceed = false;
            ShowGridData = false;
        }
        public string StatusMsg
        {
            get { return _statusMsg; }
            set
            {
                _statusMsg = value;
                NotifyPropertyChanged();
            }
        }
        public bool CanProceed
        {
            get { return _canProceed; }
            set{
                _canProceed = value;
                NotifyPropertyChanged();
            }

        }

        public string ErrorMsg
        {
            get { return _errorMsg; }
            set
            {
                _errorMsg = value;
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

        private BatchEditImport _selectedBatch;
        public BatchEditImport SelectedBatch
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