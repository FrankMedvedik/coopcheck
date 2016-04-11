using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Email1099.WPF.Content.Design;
using Email1099.WPF.Converters;
using Email1099.WPF.Models;
using Email1099.WPF.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;


namespace Email1099.WPF.Content.PaymentFinder
{
    public class PaymentFinderViewModel : ViewModelBase
    {
        public PaymentFinderViewModel()
        {
            SelectedPayments = new List<vwPayment>();
            SelectionChangedCommand = new RelayCommand<IList>(
                items =>
                {
                    SelectedPayments = new List<vwPayment>();
                    if (items == null)
                    {
                        return;
                    }
                    SelectedPayments = new List<vwPayment>();
                    foreach (var i in items)
                        SelectedPayments.Add((vwPayment)i);
                    SelectedPaymentsCount = SelectedPayments.Count;
                }
            );

            EmailPayeeCommand = new RelayCommand(EmailPayee, CanEmailPayee);
            PhonePayeeCommand = new RelayCommand(PhonePayee, CanPhonePayee);
            EmailJobMgrCommand = new RelayCommand(EmailJobMgr, CanEmailJobMgr);
            PhoneJobMgrCommand = new RelayCommand(PhoneJobMgr, CanPhoneJobMgr);

            ResetState();
            GetPayments();
        }



        public RelayCommand<IList> SelectionChangedCommand
        {
            get;
            private set;
        }
        public RelayCommand EmailPayeeCommand { get; private set; }
        public RelayCommand PhonePayeeCommand { get; private set; }
        public RelayCommand EmailJobMgrCommand { get; private set; }
        public RelayCommand PhoneJobMgrCommand { get; private set; }
        public bool CanEmailJobMgr()
        {
            return SelectedPayments.Count > 0;
        }
        public bool CanPhoneJobMgr()
        {
            return SelectedPayments.Count > 0;
        }

        public bool CanEmailPayee()
        {
            return SelectedPayments.Count > 0;
        }
        public bool CanPhonePayee()
        {
            return SelectedPayments.Count > 0;
        }

        public async void PhonePayee()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "placing call...",
            };

            PhoneSvc.PlaceAudioCall(SelectedPayment.phone_number);
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("call has been placed ")
            };
        }
        public async void PhoneJobMgr()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "placing call...",
            };

            var j = JobSvc.GetJob(SelectedPayment.job_num.GetValueOrDefault());
            PhoneSvc.PlaceAudioCall(j.projmgr);
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("call has been placed ")
            };
        }
        public async void EmailPayee()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "creating email",
                IsBusy = true
            };
            
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var h = ListToHTMLTable.GetTable<vwPayment>(SelectedPayments, x => x.first_name, x => x.last_name);
                    EmailSvc.SendEMailThroughOutlook(new List<string>() {SelectedPayment.email},
                        string.Format("payment info {0} check number {1}", SelectedPayment.pay_date,
                            SelectedPayment.check_num), h);
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = String.Format("failed to create email "),
                        ErrorMessage = ex.Message
                    };
                }
            });
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("check number {0} has been emailed ", SelectedPayment.check_num)
            };
        }
        public async void EmailJobMgr()
        {
            Status = new StatusInfo()
            {
                StatusMessage = "creating email",
                IsBusy = true
            };

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var j = JobSvc.GetJob(SelectedPayment.job_num.GetValueOrDefault());
                    var h = ListToHTMLTable.GetTable<vwPayment>(SelectedPayments, x => x.first_name, x => x.last_name);
                    EmailSvc.SendEMailThroughOutlook(new List<string>() {j.projmgr },
                        string.Format("Job manager payment info {0} - {1}", j.job_num, j.jobname), h);
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = String.Format("failed to create email "),
                        ErrorMessage = ex.Message
                    };
                }
            });
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("check number {0} has been emailed ", SelectedPayment.check_num)
            };
        }

        private List<vwPayment> SelectedPayments
        {
            get { return _selectedPayments; }
            set
            {
                _selectedPayments = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("SelectedPaymentsCount");
            }
            
        }

        public int SelectedPaymentsCount
        {
            get { return _selectedPaymentsCount; }
            set { _selectedPaymentsCount = value;
                NotifyPropertyChanged();
            }
        }



        private ObservableCollection<vwPayment> _payments = new ObservableCollection<vwPayment>();

        public ObservableCollection<vwPayment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                string em = null;

                Status = new StatusInfo()
                {
                    ErrorMessage = em,
                    StatusMessage = String.Format("{0} Payments found", Payments.Count)
                };
            }
        }


        private StatusInfo _status;

        public void ResetState()
        {

            _payments = new ObservableCollection<vwPayment>();

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

        #region collection and selected

        private vwPayment _selectedPayment;
        private string _myString;
        private List<vwPayment> _selectedPayments;
        private int _selectedPaymentsCount;

        public vwPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
            }
        }

        #endregion


        public async void GetPayments()
        {
            Status = new StatusInfo()
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };
            try
            {
                Payments = await Task<ObservableCollection<vwPayment>>.Factory.StartNew(() =>
                {
                    return new ObservableCollection<vwPayment>(Notifications.RptSvc.GetPayments());
                });

            }
            catch (Exception e)
            {
                Status = new StatusInfo()
                {
                    StatusMessage = "Error loading payments",
                    ErrorMessage = e.Message
                };

            }
        }
    }

    public class Notifications
    {
        public static string StatusInfoChanged
        {
            get { return "StatusInfoChanged"; }
        }

        public class RptSvc
        {
            public static List<vwPayment> GetPayments()
            {
                return MockPayments.GetPayments();
            }
        }
    }
}