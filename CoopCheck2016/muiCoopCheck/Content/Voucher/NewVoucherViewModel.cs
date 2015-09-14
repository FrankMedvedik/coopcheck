using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{

    public class NewVoucherViewModel : ViewModelBase
    {
        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports  {             
            get { return _voucherImports ; }
            set
            {
                _voucherImports = value;
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

        public NewVoucherViewModel()
        {
            ResetState();
            
        }

        #region DisplayState
        private bool _canSave;

        private void ResetState()
        {
            ShowGridData = false;
            Status = new StatusInfo()
            {
                StatusMessage = "fill in the voucher",
                ErrorMessage = ""
            };

            // load test data 
            var v = new List<VoucherImport>();
            for (var i = 0; i < 10; i++)
                v.Add(VoucherImport.GetTestData());

            VoucherImports = new ObservableCollection<VoucherImport>(v);
            SelectedVoucher = null;
            //SelectedVoucher = new VoucherImport();
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


        private VoucherImport _selectedVoucher;
        private StatusInfo _status;

        public VoucherImport SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                NotifyPropertyChanged();
            }
        }
        
        public void DeleteSelectedVoucher()
        {

            if(SelectedVoucher != null ) VoucherImports.Remove(SelectedVoucher);
        }

        public void CreateNewVoucher()
        {
            var a = new VoucherImport().GetNewWithDefaults();
            VoucherImports.Add(a);
            SelectedVoucher = a;
        }

        public void SaveSelectedVoucher()
        {
            
        }
    }
}
