using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher
{

    public class BatchEditViewModel : ViewModelBase , IDataErrorInfo
    {
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyPropertyChanged();
            }

        }
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                NotifyPropertyChanged();
            }

        }
        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();
        public ObservableCollection<VoucherImport> VoucherImports  {             
            get { return _voucherImports ; }
            set
            {
                IsBusy = true;
                _voucherImports = value;
                NotifyPropertyChanged();
                StatusInfo s = new StatusInfo()
                {
                    StatusMessage = "Importing",
                    ErrorMessage =""
                };

                Status = s;

                if ((_voucherImports.Count > 0) )
                {
                    SelectedBatch = BatchEdit.NewBatchEdit();
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
                    try
                    {
                        foreach (var v in VoucherImports)
                            SelectedBatch.Vouchers.Add(v.ToVoucherEdit());
                        s.ErrorMessage = "";
                        s.StatusMessage = string.Format("Imported {0} Vouchers", SelectedBatch.Vouchers.Count);
                        Status = s;
                    }
                    catch (Exception e)
                    {
                        s.ErrorMessage = e.Message;
                        s.StatusMessage = "Error Importing Vouchers";
                        Status = s;
                    }
                    
                }
                IsBusy = false;
                IsDirty = true;
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
                IsBusy = true;
            });
        }

        public void ResetState()
        {
            var s = new StatusInfo()
            {
                StatusMessage = "fill and verify the details about the voucher batch ",
                ErrorMessage = ""
            };
            Status = s;
            IsDirty = false;
        }

        private BatchEdit _selectedBatch;/* = BatchEdit.NewBatchEdit();*/
        private StatusInfo _status;
        private bool _isBusy;
        private bool _isDirty;
        private VoucherEdit _selectedVoucher;

        public BatchEdit  SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(HeaderText);
                IsBusy = false;
            }
        }
        public VoucherEdit SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(HeaderText);
                IsBusy = false;
            }
        }
        public int BatchNum
        {
            get { return SelectedBatch != null ? SelectedBatch.Num : 0; }
            set
            {
                SelectedBatch  =  (BatchNum == -1) ? SelectedBatch = BatchEdit.NewBatchEdit(): BatchEdit.GetBatchEdit(value);
                IsDirty = true;
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

        public string this[string columnName] { 
            get
            {
                if (columnName == "SelectedBatch")
                {
                    return "The vouchers haev errors";
                }
                return null;
            }

        }
        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                StatusInfo s = new StatusInfo()
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

        public async void  Save()
        {
            await SelectedBatch.SaveAsync();
        }
    }
}