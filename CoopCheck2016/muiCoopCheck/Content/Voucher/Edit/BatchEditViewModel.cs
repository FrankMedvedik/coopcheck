using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            get { return SelectedBatch != null && UserCanWrite && SelectedBatch.IsDirty; }
        }

        public bool CanDeleteVoucher
        {
            get { return SelectedVoucher != null && UserCanWrite; }
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
                if(message.Content != null)
                    BatchNum = message.Content.batch_num;
            });
        }

        public void ResetState()
        {
            ShowSelectedBatch = false;
            Status = new StatusInfo();
            //{
            //    StatusMessage = "fill and verify the details about the voucher batch ",
            //    ErrorMessage = ""
            //};

            UserCanRead = UserAuth.Instance.CanRead;
            UserCanWrite = UserAuth.Instance.CanWrite;
        }

        private StatusInfo _status;
        private BatchEdit _selectedBatch;
        private VoucherEdit _selectedVoucher;
        private bool _userCanWrite;
        private bool _userCanRead;

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
                    HeaderText = string.Format("Batch Number {0} Job Number {1}  Voucher Cnt {2} Total Amount {3:C}",
                        SelectedBatch.Num, SelectedBatch.JobNum, SelectedBatch.Vouchers.Count, SelectedBatch.Amount.GetValueOrDefault(0));
                    Status = new StatusInfo()
                    {
                        StatusMessage = HeaderText
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

        private string _headerText;
        public string HeaderText
        {
            get
            {
                return _headerText;
            }
            set
            {
                _headerText = value;
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
            if ((SelectedBatch.IsSavable) && UserCanWrite)
            {
                Status = new StatusInfo()
                { StatusMessage = "saving...", IsBusy = true };
                await SelectedBatch.SaveAsync();
                Status = new StatusInfo() { StatusMessage = "saved" };
            }
            else
                if(UserCanWrite)
                    Status = new StatusInfo()
                    {
                        StatusMessage = "Batch cannot be saved until all vouchers are valid",
                        ErrorMessage = "cannot save"
                    };
        }
        public bool UserCanWrite
        {
            get { return _userCanWrite; }
            set
            {
                _userCanWrite = value;
            }
        }

        public bool UserCanRead
        {
            get { return _userCanRead; }
            set
            {
                _userCanRead = value;
            }
        }

        public void DeleteSelectedVoucher()
        {
            if (SelectedBatch != null && UserCanWrite)
            {
                SelectedBatch.Vouchers.Remove(SelectedVoucher.Id);
            }
        }
        public void AddVoucher()
        {
            if (SelectedBatch != null)
            {
                VoucherEditChildWindow cw = new VoucherEditChildWindow();
                cw.ShowInTaskbar = false;
                cw.Owner = Application.Current.MainWindow;
                cw.Show();
                // need to display form with elements and 
                // 
                //SelectedBatch.Vouchers.Add(  );
            }
        }

        public async void Delete()
        {
            if (SelectedBatch != null)
            {
                if (UserCanWrite)
                {
                    await BatchSvc.DeleteBatchEditAsync(SelectedBatch.Num);
                    Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
                    Status = new StatusInfo()
                    {
                        StatusMessage = "Batch has been deleted",
                    };
                    ResetState();
                }
            };
        }
    }
}