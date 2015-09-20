using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Voucher.Edit
{

    public class BatchEditViewModel : ViewModelBase , IDataErrorInfo
    {

        private Boolean _showSelectedBatch;

        public Boolean ShowSelectedBatch
        {
            get { return _showSelectedBatch; }
            set
            {
                _showSelectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return Status.IsBusy; }
        }
        public bool IsDirty
        {
            get { return (SelectedBatch != null) ? SelectedBatch.IsDirty : false; }
        }
        private ObservableCollection<VoucherImport> _voucherImports = new ObservableCollection<VoucherImport>();

        public ObservableCollection<VoucherImport> VoucherImports
        {
            get { return _voucherImports; }
            set
            {
                _voucherImports = value;
                NotifyPropertyChanged();
                Status = new StatusInfo()
                {
                    StatusMessage = "Importing vouchers please wait",
                    IsBusy = true
                };

                if ((_voucherImports.Count > 0))
                {
                    ImportVouchers();
                }
            }
        }

        private async void ImportVouchers()
        {
            var z =  Task.Factory.StartNew(async () =>
            {
                var sb  = await BatchSvc.GetNewBatchEditAsync();
                try
                {
                    sb.JobNum = int.Parse(VoucherImports.Select(x => x.JobNumber).First());
                }
                catch (Exception e)
                {
                    sb.JobNum = -1;
                }
                sb.Amount = VoucherImports.Select(x => x.Amount).Sum().GetValueOrDefault(0);
                sb.Date = DateTime.Today.ToShortDateString();
                sb.Save();

                try
                {
                    foreach (var v in VoucherImports)
                        sb.Vouchers.Add(v.ToVoucherEdit());
                    Status = new StatusInfo()
                    {
                        StatusMessage = string.Format("Imported {0} Vouchers",
                            SelectedBatch.Vouchers.Count)
                    };

                }
                catch (Exception e)
                {
                    Status = new StatusInfo()
                    {
                        ErrorMessage = e.Message,
                        StatusMessage = "Error Importing Vouchers"
                    };
                }
                SelectedBatch = sb;
            });
        }

        public StatusInfo Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HeaderText");
                NotifyPropertyChanged("IsBusy");
                NotifyPropertyChanged("IsDirty");
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

        public BatchEditViewModel()
        {
            ResetState();
            Messenger.Default.Register<NotificationMessage<OpenBatch>>(this, message =>
            {
                BatchNum = message.Content.batch_num;
            });
        }

        public void ResetState()
        {
            ShowSelectedBatch = false;
            Status = new StatusInfo()
            {
                StatusMessage = "fill and verify the details about the voucher batch ",
                ErrorMessage = ""
            };
        }

        private StatusInfo _status;
        private BatchEdit _selectedBatch;
        private VoucherEdit _selectedVoucher;

        public BatchEdit SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                if (SelectedBatch != null)
                {
                    ShowSelectedBatch = true;
                    Status = new StatusInfo()
                    {
                        StatusMessage =
                            string.Format("Batch Number {0} Job Number {1}  Total Amount {2:C}", SelectedBatch.Num,
                                SelectedBatch.JobNum, SelectedBatch.Amount.GetValueOrDefault(0))
                    };
                }
                else
                    ShowSelectedBatch = false;
            }
        }

        public VoucherEdit SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                NotifyPropertyChanged();
                Status = new StatusInfo()
                {
                    StatusMessage = ""
                };
            }

        }
        public int BatchNum
        {
            get { return SelectedBatch != null ? SelectedBatch.Num : 0; }
            set
            {
                Status = new StatusInfo()
                { StatusMessage = "loading.." , IsBusy=true };
                if (BatchNum == -1) // shorthand for make a  new one ! 
                    SelectedBatch = BatchEdit.NewBatchEdit();
                else
                    GetBatch(value);
                NotifyPropertyChanged();
            }
        }

        private async void GetBatch(int batchNum)
        {
            SelectedBatch =  await BatchSvc.GetBatchEditAsync(batchNum);
        }
        public string HeaderText
        {
            get
            {
                return Status.StatusMessage;
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
            if (SelectedBatch.IsSavable)
            {
                Status = new StatusInfo()
                { StatusMessage = "saving...", IsBusy = true };
                await SelectedBatch.SaveAsync();
                Status = new StatusInfo() { StatusMessage = "saved" };
            }

            else
                Status = new StatusInfo()
                {
                    StatusMessage = "Batch cannot be saved until all vouchers are valid",
                    ErrorMessage = "cannot save"
                };
        }
    }
}