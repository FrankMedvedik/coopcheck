using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    public class OpenPaymentReportViewModel : ViewModelBase
    {
        public OpenPaymentReportViewModel()
        {
            ResetState();

        }

        public int PaymentsCnt
        {
            get { return Payments.Count(); }
        }

        public decimal? PaymentsTotalDollars
        {
            get { return Payments.Sum(x => x.tran_amount ); }
        }

        private ObservableCollection<vwBasicPayment> _payments = new ObservableCollection<vwBasicPayment>();
        public ObservableCollection<vwBasicPayment> Payments
        {
            get { return _payments; }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PaymentsTotalDollars");
                NotifyPropertyChanged("PaymentsCnt");
                Status = new StatusInfo()
                {
                    StatusMessage = $"{PaymentsCnt} Payments found, Total Dollars {PaymentsTotalDollars:c}"
                };
                ShowGridData = true;
            }
        }


        #region DisplayState
        private StatusInfo _status;

        public void ResetState()
        {

            _payments = new ObservableCollection<vwBasicPayment>();
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

        private vwBasicPayment _selectedPayment;
        public vwBasicPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set
            {
                _selectedPayment = value;
                NotifyPropertyChanged();
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
                Payments = await Task<ObservableCollection<vwBasicPayment>>.Factory.StartNew(() =>
                {
                    var task =  RptSvc.GetOpenPayments(p);
                    var v = new ObservableCollection<vwBasicPayment>(task.Result);
                    return v;
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

        private List<vwBasicPayment> p;

        public  async void  ExportToExcel()
        {
            await Task.Factory.StartNew(() =>
            {
                p = Payments.ToList();
                ExportToExcel<vwBasicPayment, List<vwBasicPayment>> s = new ExportToExcel<vwBasicPayment, List<vwBasicPayment>>();
                s.dataToPrint = p;
                s.GenerateReport();
            });
        }
    }
}