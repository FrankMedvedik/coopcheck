using System;
using System.Collections.ObjectModel;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Rpt
{
    public class PaymentRptViewModel : ViewModelBase
    {
        public PaymentRptViewModel()
        {
            ResetState();
        }

         public void ResetState()
        {

            ReportDates = new ReportDateRange();
            ReportDates.StartRptDate = DateTime.Today.Add(new TimeSpan(-365, 0, 0, 0));
            ReportDates.EndRptDate = DateTime.Today;
            ShowGridData = false;

        }


        private bool _canRefresh = true;
        public Boolean CanRefresh
        {
            get { return _canRefresh; }
            set { _canRefresh = value; }
        }

        #region reporting variables
        public ReportDateRange ReportDates;
        public vwJobRpt Job;
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
                GetJobPayments();
            }
        }

        //public async void GetJobPayments()
        //{
        //    ShowGridData = false;
        //    HeadingText = "";
        //    if ((ReportDates != null) && (Job != null))
        //    {
        //        HeadingText = "Loading...";
        //        try
        //        {
        //            var ctx = new CoopCheckEntities();
        //            Payments = new ObservableCollection<vwPayment>(ctx.vwPayments.Where(x => x.batch_num == Batch.batch_num));
        //            HeadingText = String.Format("batch {0} dated {1:ddd, MMM d, yyyy} has {2} payments", Batch.batch_num, Batch.batch_date, Payments.Count());
        //            ShowGridData = true;
        //        }
        //        catch (Exception e)
        //        {
        //            HeadingText = e.Message;
        //        }
        //    }
        //}

        public async void GetJobPayments()
        {
            ShowGridData = false;
            HeadingText = "";
            if ((Job!= null))
            {
                HeadingText = "Loading...";
                try
                {
                    using (var ctx = new CoopCheckEntities())
                    {
                        var p = await PaymentSvc.GetPayments(Job.job_num);
                        Payments = new ObservableCollection<vwPayment>(p);
                    }
                    
                    HeadingText = String.Format("job {0} first check dated {1:ddd, MMM d, yyyy} has {2} payments", Job.job_num, Job.last_check_date, Payments.Count());
                    ShowGridData = true;
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