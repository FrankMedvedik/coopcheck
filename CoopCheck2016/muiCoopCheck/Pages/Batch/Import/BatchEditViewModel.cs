using System;
using System.Collections.ObjectModel;
using System.Linq;
using muiCoopCheck.DesignData;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Batch.Import
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
                if ((_voucherImports.Count > 0) && (SelectedBatch != null))
                {
                    try
                    {
                        var n = int.Parse(VoucherImports.Select(x => x.JobNumber).First());
                    }
                    catch (Exception e)
                    {
                        SelectedBatch.JobNum = 0;    
                    }

                    SelectedBatch.Amount = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                    SelectedBatch.Date = DateTime.Today.ToShortDateString();
                }
            }
        }

        public BatchEditViewModel()
        {
            ResetState();
            StatusMsg = "fill and verify the details about the voucher batch ";
            SelectedBatch = BatchEditDesign.LoadTestData();
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