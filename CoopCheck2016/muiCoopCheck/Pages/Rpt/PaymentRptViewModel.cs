using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;

namespace muiCoopCheck.Pages.Rpt
{
    public class PaymentRptViewModel : ViewModelBase
    {
        public PaymentRptViewModel()
        {
        }

        private bool _canRefresh = true;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public ReportDateRange ReportDateRange = new ReportDateRange();
        public vwRptBatchPayment Batch;
        #endregion

        private ObservableCollection<vwPayment> _payments = new ObservableCollection<vwPayment>();

        private vwPayment _selectedPayment;
        public vwPayment SelectedPayment
        {
            get { return _selectedPayment; }
            set {
                _selectedPayment = value; 
                  NotifyPropertyChanged();
            }
        }

        private string _headingText;
        private bool _showGridData;

        public string HeadingText
        {
            get
            {
                return _headingText;
            }
            set
            {
                _headingText = value;
                NotifyPropertyChanged();
            }
        }


        public ObservableCollection<vwPayment> Payments
        {
            get
            {
                return _payments;
            }
            set
            {
                _payments = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HeadingText");

            }
        }

        public void RefreshAll ()
        {
            if (CanRefresh)
            {
                GetPayments();
            }
        }

        public async void GetPayments()
        {
            ShowGridData = false;
            HeadingText = "";
            if ((ReportDateRange != null) && (Batch != null))
            {
                HeadingText = "Loading...";
                try
                {

                    var ctx = new CoopCheckEntities();
                    Payments = new ObservableCollection<vwPayment>(
                        ctx.vwPayments.Where(x => x.batch_num == Batch.batch_num.GetValueOrDefault(-1)));

                    HeadingText = String.Format("Batch {0} Dated {1} ", Batch.batch_num, Batch.batch_date);
                }
                catch (Exception e)
                {
                    HeadingText = e.Message;
                }
            }
        }

        public bool ShowGridData    
        {
            get { return _showGridData; }
            set
            {
                _showGridData = value;
                NotifyPropertyChanged();
            }
        }
    }
}