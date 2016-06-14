using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using CoopCheck.Library;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private bool _canPayBatch;


        private bool _isNewVoucher;
        private string _jobName;
        private BatchEdit _selectedBatch;
        private VoucherEdit _selectedVoucher;

        private bool _showSelectedBatch;


        private bool _showSelectedVoucher;

        private StatusInfo _status;


        private ObservableCollection<VoucherEdit> _vouchersWithErrors = new ObservableCollection<VoucherEdit>();
        private VoucherImport _workVoucherImport;

        public BatchEditViewModel()
        {
            RefreshBatchListCommand = new RelayCommand(RefreshBatchList, CanRefreshBatchList);
            ResetState();
            Messenger.Default.Register<NotificationMessage<OpenBatch>>(this, async message =>
            {
                if (message.Content != null)
                {
                    //BatchNum = message.Content.batch_num;
                    SelectedBatch = await GetBatch(message.Content.batch_num);
                    SetJobName();
                }
                else
                    ShowSelectedBatch = false;
            });
        }

        public RelayCommand RefreshBatchListCommand { get; private set; }

        public bool ShowSelectedBatch
        {
            get { return _showSelectedBatch; }
            set
            {
                _showSelectedBatch = value;
                NotifyPropertyChanged();
            }
        }

        public bool CanPayBatch
        {
            get { return _canPayBatch; }
            set
            {
                _canPayBatch = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("JobNameBrush");
            }
        }

        public bool IsNewVoucher
        {
            get { return _isNewVoucher; }
            set
            {
                _isNewVoucher = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowSelectedVoucher
        {
            get { return _showSelectedVoucher; }
            set
            {
                _showSelectedVoucher = value;
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

        public ObservableCollection<VoucherEdit> VouchersWithErrors
        {
            get { return _vouchersWithErrors; }
            set
            {
                _vouchersWithErrors = value;
                NotifyPropertyChanged();
            }
        }

        public string JobName
        {
            get { return _jobName; }
            set
            {
                _jobName = value;
                NotifyPropertyChanged();
                if (string.IsNullOrEmpty(SelectedBatch.StudyTopic))
                    SelectedBatch.StudyTopic = _jobName;
            }
        }

        public int? JobNum
        {
            get { return SelectedBatch?.JobNum; }
            set
            {
                SelectedBatch.JobNum = value;
                SetJobName();
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
                NotifyPropertyChanged("HeaderText");
                NotifyPropertyChanged("IsBusy");
                NotifyPropertyChanged("IsDirty");
                Messenger.Default.Send(new NotificationMessage<StatusInfo>(_status, Notifications.StatusInfoChanged));
            }
        }

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
                    //HeaderText = string.Format("Batch Number {0} Job Number {1}  Voucher Cnt {2} Total Amount {3:C}",
                    //    SelectedBatch?.Num, SelectedBatch?.JobNum, SelectedBatch?.Vouchers.Count, SelectedBatch?.Amount.GetValueOrDefault(0));

                    Status = new StatusInfo
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
                if (SelectedVoucher != null)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage =
                            string.Format("{0} amount {1:C}", SelectedVoucher.EmailAddress,
                                SelectedVoucher.Amount.GetValueOrDefault(0))
                    };
                    ShowSelectedVoucher = true;
                }
            }
        }

        public VoucherImport WorkVoucherImport
        {
            get { return _workVoucherImport; }
            set
            {
                _workVoucherImport = value;
                NotifyPropertyChanged();
            }
        }


        public int BatchNum
        {
            get { return SelectedBatch != null ? SelectedBatch.Num : 0; }
            //set
            //{
            //    Status = new StatusInfo()
            //    //{ StatusMessage = "loading.." , IsBusy=true };
            //    //try
            //    //{
            //    //    if (BatchNum == -1) // shorthand for make a  new one ! 
            //    //        SelectedBatch = BatchEdit.NewBatchEdit();
            //    //    else
            //    //        await GetBatch(value);
            //    //    NotifyPropertyChanged();

            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Status = new StatusInfo()
            //    //    { StatusMessage = "error loading..", ErrorMessage=ex.Message};
            //    //}
            //}
        }

        public string HeaderText { get; set; }

        public bool UserCanWrite { get; set; }

        public bool UserCanRead { get; set; }

        public bool HaveSelectedVoucher
        {
            get { return SelectedBatch != null; }
        }

        public Brush JobNameBrush
        {
            get { return CanPayBatch ? Brushes.Black : Brushes.Red; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "SelectedBatch")
                {
                    return "The vouchers have errors";
                }
                return null;
            }
        }

        public string Error
        {
            get { return Status.ErrorMessage; }
            set
            {
                var s = new StatusInfo
                {
                    ErrorMessage = value,
                    StatusMessage = "Error"
                };
                Status = s;
            }
        }

        private bool CanRefreshBatchList()
        {
            return true;
        }


        private void RefreshBatchList()
        {
            Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
        }

        public void ResetState()
        {
            ShowSelectedBatch = false;
            ShowSelectedVoucher = false;
            Status = new StatusInfo();
            //{
            //    StatusMessage = "fill and verify the details about the voucher batch ",
            //    ErrorMessage = ""
            //};

            UserCanRead = UserAuth.Instance.CanRead;
            UserCanWrite = UserAuth.Instance.CanWrite;
        }

        private async Task<BatchEdit> GetBatch(int batchNum)
        {
            return await BatchSvc.GetBatchEditAsync(batchNum);
            //  return BatchSvc.GetBatchEdit(batchNum);
        }

        public async void AutoSaveSelectedBatch()
        {
            if ((SelectedBatch.IsSavable) && UserCanWrite)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "saving...",
                    IsBusy = true
                };
                try
                {
                    SelectedBatch = await SelectedBatch.SaveAsync();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = "error saving",
                        ErrorMessage = ex.Message,
                        IsBusy = false
                    };
                }
            }
        }

        public async void SaveSelectedBatch()
        {
            //if ((SelectedBatch.IsSavable) && UserCanWrite)
            if (UserCanWrite)
            {
                Status = new StatusInfo {StatusMessage = "saving...", IsBusy = true};

                try
                {
                    SelectedBatch = await SelectedBatch.SaveAsync();
                    Status = new StatusInfo {StatusMessage = "saved"};
                    RefreshBatchList();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo {StatusMessage = "error during save", ErrorMessage = ex.Message};
                }
            }
            else if (UserCanWrite)
            {
                var msgs = new List<string>();
                var a = SelectedBatch.GetBrokenRules();
                msgs.Add(a.ToString());
                foreach (var v in SelectedBatch.Vouchers)
                {
                    msgs.Add(v.BrokenRulesCollection.ToString());
                }
                Status = new StatusInfo
                {
                    StatusMessage = "Batch cannot be saved until all vouchers are valid",
                    ErrorMessage = msgs.ToString()
                };
            }
        }

        public async void DeleteSelectedVoucher()
        {
            if (SelectedBatch != null && UserCanWrite)
            {
                SelectedBatch.Vouchers.Remove(SelectedVoucher.Id);
                SelectedBatch = await SelectedBatch.SaveAsync();
            }
        }

        public async void SaveNewVoucher()
        {
            if ((SelectedBatch != null) && (WorkVoucherImport != null))
            {
                SelectedBatch.Vouchers.Add(VoucherImportConverter.ToVoucherEdit(WorkVoucherImport));
                if (SelectedBatch.IsValid)
                    SelectedBatch = await SelectedBatch.SaveAsync();
            }
        }


        public void CreateNewVoucher()
        {
            if (SelectedBatch != null)
            {
                var v = new VoucherImport();
                v.Amount = SelectedBatch.Amount;
                v.JobNumber = SelectedBatch.JobNum.ToString();
                WorkVoucherImport = v;
            }
        }

        public async void DeleteSelectedBatch()
        {
            if (SelectedBatch != null)
            {
                var v = SelectedBatch.Num;
                if (UserCanWrite)
                {
                    try
                    {
                        await BatchSvc.DeleteBatchEditAsync(v);
                        Messenger.Default.Send(new NotificationMessage(Notifications.RefreshOpenBatchList));
                        Status = new StatusInfo
                        {
                            StatusMessage = string.Format("{0} Batch has been deleted", v)
                        };
                        ResetState();
                    }
                    catch (Exception e)
                    {
                        Status = new StatusInfo
                        {
                            StatusMessage = string.Format("{0} could not delete batch", v),
                            ErrorMessage = e.Message
                        };
                    }
                }
            }
            ;
        }

        public void CancelNewVoucher()
        {
            WorkVoucherImport = null;
            ;
        }

        public async void SetJobName()
        {
            try
            {
                if (SelectedBatch?.JobNum.GetValueOrDefault(0).ToString().Length == 8)
                {
                    var b = SelectedBatch.JobNum.GetValueOrDefault(0);
                    JobName = await JobLogSvc.GetJobName(b);
                }
                else
                {
                    JobName = JobLogSvc.BadJobName;
                }
                CanPayBatch = (JobName != JobLogSvc.BadJobName);
            }
            catch (Exception)
            {
                JobName = JobLogSvc.BadJobName;
                CanPayBatch = false;
            }
        }
    }
}