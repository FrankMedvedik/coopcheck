using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
{
    public class PaymentFinderViewModel : ViewModelBase
    {
        public PaymentFinderViewModel()
        {
            ResetState();
            TheVoidCheckCommand = new RelayCommand(VoidCheck, CanVoidCheck);
        }
    public RelayCommand TheVoidCheckCommand { get; private set; }

    public async void VoidCheck()
        {

            Status = new StatusInfo()
            {
                StatusMessage = "voiding check",
                IsBusy = true
            };



            await Task.Factory.StartNew(() =>
            {
                try
                {
                    VoidCheckCommand.Execute(SelectedPayment.tran_id);
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo()
                    {
                        StatusMessage = "failed to clear payments",
                        ErrorMessage = ex.Message,
                        ShowMessageBox = true
                    };
                }
            });
            Status = new StatusInfo()
            {
                StatusMessage = String.Format("check number {0} has been voided", SelectedPayment.check_num),
                ShowMessageBox = true
            };
        }

        public bool CanVoidCheck()
        {
            if (SelectedPayment == null ) return false;
            if (SelectedPayment.cleared_flag) return false;
            return true;
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

                if (Payments.Count == PaymentSvc.MAX_PAYMENT_COUNT)
                        em = String.Format("Showing only the first {0} payments add additional criteria to limit your search", PaymentSvc.MAX_PAYMENT_COUNT);
                 Status = new StatusInfo()
                {
                    ErrorMessage = em,
                    StatusMessage = String.Format("{0} Payments found", Payments.Count)
                };
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

        #region collection and selected

        private vwPayment _selectedPayment;
        public vwPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
                TheVoidCheckCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        public async void GetPayments(PaymentReportCriteria p)
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
                    var task =  RptSvc.GetPayments(p);
                    return new ObservableCollection<vwPayment>(task.Result);
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
}