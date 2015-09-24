using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoopCheck.Repository;
using GalaSoft.MvvmLight.Messaging;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Report
{
    public class SummaryPaymentReportViewModel : ViewModelBase
    {
        public bool ShowGridData
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }

        public SummaryPaymentReportViewModel()
        {
            ShowGridData = false;
      
        }

        private PaymentReportCriteria _paymentReportCriteria = new PaymentReportCriteria();
        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _paymentReportCriteria; }
            set
            {
                _paymentReportCriteria = value;
                NotifyPropertyChanged();
            }
        }

        public  async void RefreshAll()
        {
            WorkPayments = await RptSvc.GetPayments(PaymentReportCriteria);

        }
        private bool _showGridData;
        private StatusInfo _status;
        private ObservableCollection<vwPayment> _allPayments;
        private List<vwPayment> _workPayments;

        public List<vwPayment> WorkPayments
        {
            get { return _workPayments; }
            set
            {
                _workPayments = value;
                AllPayments = new ObservableCollection<vwPayment>(WorkPayments);
            }
        }


        public string HeadingText => PaymentReportCriteria.ToFormattedString(' ');

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

        public ObservableCollection<vwPayment> AllPayments
        {
            get { return _allPayments; }
            set
            {
                _allPayments = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("ClosedPayments");
                NotifyPropertyChanged("OpenPayments");
                ShowGridData = true;
            }
            
        }

        public ObservableCollection<vwPayment> ClosedPayments => 
            new ObservableCollection<vwPayment>((
                        from l in _workPayments
                        where l.cleared_flag 
                        orderby l.check_date
                        select l).ToList());

        public ObservableCollection<vwPayment> OpenPayments =>
            new ObservableCollection<vwPayment>((
                        from l in _workPayments
                        where !l.cleared_flag
                        orderby l.check_date
                        select l).ToList());

    }
    }
