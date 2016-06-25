using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Domain.Contracts.Messages;
using CoopCheck.Domain.Contracts.Models;
using CoopCheck.Domain.Services;
using CoopCheck.Library;
using CoopCheck.Reports.Contracts.Interfaces;
using CoopCheck.Reports.Contracts.Models;
using CoopCheck.WPF.Content.Interfaces;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.PaymentReports.PaymentFinder
{
    public class PaymentFinderViewModel : ViewModelBase, IPaymentFinderViewModel
    {
        private readonly IRptSvc _rptSvc;
        private ObservableCollection<Payment> _payments = new ObservableCollection<Payment>();

        public PaymentFinderViewModel(IRptSvc rptSvc)
        {
            _rptSvc = rptSvc;
            ResetState();
            TheVoidCheckCommand = new RelayCommand(VoidCheck, CanVoidCheck);
            TheClearCheckCommand = new RelayCommand(ClearCheck, CanClearCheck);

            Messenger.Default.Register<NotificationMessage<PaymentReportCriteria>>(this, message =>
            {
                if (message.Notification == Notifications.FindChecks)
                {
                    PaymentReportCriteria = message.Content;
                    GetPayments();
                }
            });
        }

        public RelayCommand TheVoidCheckCommand { get; }
        public RelayCommand TheClearCheckCommand { get; }

        public bool CanVoidSelectedCheck
        {
            get { return CanVoidCheck(); }
        }

        public bool CanClearSelectedCheck
        {
            get { return CanClearCheck(); }
        }

        public int PaymentsCnt
        {
            get { return Payments.Count(); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Payments.Sum(x => x.tran_amount); }
        }

        public ObservableCollection<Payment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                string em = null;
                //if (Payments.Count == PaymentSvc.MAX_PAYMENT_COUNT)
                //    em =
                //        string.Format(
                //            "Showing only the first {0} payments add additional criteria to limit your search",
                //            PaymentSvc.MAX_PAYMENT_COUNT);
                Status = new StatusInfo
                {
                    ErrorMessage = em,
                    StatusMessage = string.Format("{0} Payments found", Payments.Count)
                };
                ShowGridData = true;
            }
        }

        public PaymentReportCriteria PaymentReportCriteria { get; set; }

        public async void ClearCheck()
        {
            Status = new StatusInfo
            {
                StatusMessage = "clearing check",
                IsBusy = true
            };
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    ClearCheckCommand.Execute(SelectedPayment.tran_id, DateTime.Today,
                        SelectedPayment.tran_amount.GetValueOrDefault(0));
                    GetPayments();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = string.Format("failed to clear check number {0}", SelectedPayment.check_num),
                        ErrorMessage = ex.Message
                    };
                }
            });
            Status = new StatusInfo
            {
                StatusMessage = string.Format("check number {0} has been cleared", SelectedPayment.check_num)
            };
        }

        public async void VoidCheck()
        {
            Status = new StatusInfo
            {
                StatusMessage = "voiding payment",
                IsBusy = true
            };
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    // if its a character check number then it is a swiftpayment 
                    if (SelectedPayment.IsSwiftPayment)
                        VoidSwiftPromoCodeCommand.Execute(SelectedPayment.tran_id);
                    else
                        VoidCheckCommand.Execute(SelectedPayment.tran_id);
                    GetPayments();
                }
                catch (Exception ex)
                {
                    Status = new StatusInfo
                    {
                        StatusMessage = "failed to void payment",
                        ErrorMessage = ex.Message
                        //ShowMessageBox = true
                    };
                }
            });
            Status = new StatusInfo
            {
                StatusMessage = string.Format("check number {0} has been voided", SelectedPayment.check_num)
                //ShowMessageBox = true
            };
        }

        public bool CanVoidCheck()
        {
            if (SelectedPayment == null) return false;
            if (SelectedPayment.cleared_flag && SelectedPayment.IsCheck) return false;
            return true;
        }

        public bool CanClearCheck()
        {
            if (SelectedPayment == null) return false;
            if (SelectedPayment.cleared_flag) return false;
            return true;
        }

        public async void GetPayments()
        {
            Status = new StatusInfo
            {
                ErrorMessage = "",
                IsBusy = true,
                StatusMessage = "getting payments..."
            };
            try
            {
                Payments = new ObservableCollection<Payment>(await _rptSvc.GetPayments(PaymentReportCriteria));
            }
            catch (Exception e)
            {
                Status = new StatusInfo
                {
                    StatusMessage = "Error loading payments",
                    ErrorMessage = e.Message
                };
            }
        }

        #region DisplayState

        private StatusInfo _status;

        public void ResetState()
        {
            _payments = new ObservableCollection<Payment>();
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

        private bool _showGridData;

        public bool ShowGridData
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

        private Payment _selectedPayment;

        public Payment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
                TheVoidCheckCommand.RaiseCanExecuteChanged();
                TheClearCheckCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion
    }
}