using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media;
using CoopCheck.Repository;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using Reckner.WPF.ViewModel;
using System.Linq;
using System.Configuration;

namespace CoopCheck.WPF.Content.Voucher.Edit
{
    public class BatchEditViewModel : ViewModelBase, IDataErrorInfo
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private bool _canPayBatch;
        

        private bool _isNewVoucher;
        private string _jobName;
        private batch _selectedBatch;
        private check_tran _selectedVoucher;

        private bool _showSelectedBatch;


        private bool _showSelectedVoucher;

        private StatusInfo _status;

        private ObservableCollection<check_tran> _vouchers = new ObservableCollection<check_tran>();
        private ObservableCollection<check_tran> _vouchersWithErrors = new ObservableCollection<check_tran>();
        private VoucherImport _workVoucherImport;
        
        
        public ObservableCollection<check_tran> Vouchers
        {
            get { return _vouchers; }
            set
            {
                _vouchers = value;
                NotifyPropertyChanged();  
            }
        }

        public BatchEditViewModel()
        {
            RefreshBatchListCommand = new RelayCommand(RefreshBatchList, CanRefreshBatchList);
            ResetState();
            Messenger.Default.Register<NotificationMessage<vwOpenBatch>>(this, async message =>
            {
                if (message.Content != null)
                {
                    using (CoopCheckEntities ctx = new CoopCheckEntities())
                    {
                        SelectedBatch = await ctx.batches.FindAsync(message.Content.batch_num);

                        Vouchers =  new ObservableCollection<check_tran>(ctx.check_tran.Where(x=> x.batch_num == message.Content.batch_num).ToList());

                        SetJobName();
                    }
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
            get { return SelectedBatch != null && UserCanWrite; }
        }

        public bool CanDeleteVoucher
        {
            get { return SelectedVoucher != null && UserCanWrite; }
        }

        public ObservableCollection<check_tran> VouchersWithErrors
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
                var appSettings = ConfigurationManager.AppSettings;
                if (string.IsNullOrEmpty(SelectedBatch.study_topic) ||
                   (SelectedBatch.study_topic) == appSettings["StudyTopic"])
                {
                    SelectedBatch.study_topic = _jobName;
                }
            }
        }

        public int? JobNum
        {
            get { return SelectedBatch?.job_num; }
            set
            {
                SelectedBatch.job_num= value;
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

        public batch SelectedBatch
        {
            get { return _selectedBatch; }
            set
            {
                _selectedBatch = value;
                NotifyPropertyChanged();
                if (SelectedBatch != null)
                {
                    ShowSelectedBatch = true;
                    Status = new StatusInfo
                    {
                        StatusMessage = HeaderText
                    };
                }
                else
                    ShowSelectedBatch = false;
            }
        }

        public check_tran SelectedVoucher
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
                            string.Format("{0} amount {1:C}", SelectedVoucher.email,
                                SelectedVoucher.tran_amount.GetValueOrDefault(0))
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
            get { return SelectedBatch != null ? SelectedBatch.batch_num : 0; }
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

            UserCanRead = UserAuth.Instance.CanRead;
            UserCanWrite = UserAuth.Instance.CanWrite;
        }

        private async Task<batch> GetBatch(int batchNum)
        {
                using (CoopCheckEntities ctx = new CoopCheckEntities())
                {
                    return  await ctx.batches.FindAsync(batchNum);
                }
        }
        
        public async void SaveSelectedBatch()  
        {
            if (UserCanWrite)
            {
                Status = new StatusInfo {StatusMessage = "saving...", IsBusy = true};

                try
                {
                    using (CoopCheckEntities ctx = new CoopCheckEntities())
                    {

                        batch b = await ctx.batches.FindAsync(SelectedBatch.batch_num);
                        b.job_num = SelectedBatch.job_num;
                        b.batch_dscr = SelectedBatch.batch_dscr;
                        b.marketing_research_message = SelectedBatch.marketing_research_message;
                        b.study_topic = SelectedBatch.study_topic;
                        b.thank_you_1 = SelectedBatch.thank_you_1;
                        b.thank_you_2 = SelectedBatch.thank_you_2;
                        b.updated = DateTime.Now;
                        b.usr = Environment.UserName;
                        await ctx.SaveChangesAsync();
                        SelectedBatch = b;
                        Status = new StatusInfo { StatusMessage = "saved" };
                        RefreshBatchList();
                    }
                }
                catch (Exception ex)
                {
                    log.Error(string.Format("batch save failed  {0}", ex.Message));
                    Status = new StatusInfo {StatusMessage = "error during save", ErrorMessage = ex.Message};
                }
            }
        }

        public async void DeleteSelectedVoucher()
        {
            if (SelectedBatch != null && UserCanWrite)
            {
                using (CoopCheckEntities ctx = new CoopCheckEntities())
                {
                    ctx.check_tran.Remove(SelectedVoucher);
                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async void SaveNewVoucher()
        {
            if ((SelectedBatch != null) && (WorkVoucherImport != null))
            {
                using (CoopCheckEntities ctx = new CoopCheckEntities())
                {

                    ctx.vouchers.Add(VoucherImportConverter.toVoucher(WorkVoucherImport));
                    await ctx.SaveChangesAsync();
                }
            }
        }


        public void CreateNewVoucher()
        {
            if (SelectedBatch != null)
            {
                var v = new VoucherImport();
                v.Amount = 0;
                v.JobNumber = SelectedBatch.job_num.ToString();
                WorkVoucherImport = v;
            }
        }

        public async void DeleteSelectedBatch()
        {
            if (SelectedBatch != null)
            {
                var v = SelectedBatch.batch_num;
                IQueryable<check_tran> batchvouchers;
                

                if (UserCanWrite)
                { 
                    try
                    {
                        using (CoopCheckEntities ctx = new CoopCheckEntities())
                        {
                            batchvouchers = ctx.check_tran.Where(x => x.batch_num == v);

                            ctx.check_tran.RemoveRange(batchvouchers);

                            var b =  ctx.batches.First(x => x.batch_num == v);
                            ctx.batches.Remove(b);
                            ctx.SaveChanges();
                        }
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
                if (SelectedBatch?.job_num.GetValueOrDefault(0).ToString().Length == 8)
                {
                    var b = SelectedBatch.job_num.GetValueOrDefault(0);
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