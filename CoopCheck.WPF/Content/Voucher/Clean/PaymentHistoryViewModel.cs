using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Voucher.Clean
{
    public class PaymentHistoryViewModel : ViewModelBase
    {
        public PaymentHistoryViewModel()
        {
            ResetState();
       
            Messenger.Default.Register<NotificationMessage<PaymentReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.FindPayeePayments)
                {
                    LastName = message.Content.LastName;
                    FirstName = message.Content.FirstName;
                    GetPayments();
                }
            });

        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; NotifyPropertyChanged(); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; NotifyPropertyChanged(); }
        }

        public int PaymentsCnt
        {
            get { return Payments.Count(); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Payments.Sum(x => x.tran_amount); }
        }

        private ObservableCollection<vwPayment> _payments = new ObservableCollection<vwPayment>();
        public ObservableCollection<vwPayment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                ShowGridData = true;
            }
        }


        #region DisplayState
        private StatusInfo _status;

        public void ResetState()
        {

            _payments = new ObservableCollection<vwPayment>();
            ShowGridData = false;

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
        #endregion

        #region collection and selected

        private vwPayment _selectedPayment;
        private string _lastName;
        private string _firstName;

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
            try
            {
           
                    var pay = await RptSvc.GetPayeePayments( LastName, FirstName);
                    Payments = new ObservableCollection<vwPayment>(pay);
                
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
}