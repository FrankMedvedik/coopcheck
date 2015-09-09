using System;
using CoopCheck.WPF.Pages;

namespace CoopCheck.WPF.Models
{
    public class PaymentReportCriteria : ViewModelBase
    {
        private string _jobNumber;
        public string JobNumber
        {
            get { return _jobNumber; }
            set { _jobNumber = value; NotifyPropertyChanged(); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; NotifyPropertyChanged(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; NotifyPropertyChanged(); }
        }

        private string _checkNumber;
        public string CheckNumber
        {
            get { return _checkNumber; }
            set {
                _checkNumber = value; 
                NotifyPropertyChanged(); 
            }
        }
        private int _batchNumber;
        public int BatchNumber
        {
            get { return _batchNumber; }
            set
            {
                _batchNumber = value;
                NotifyPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName  = value;
                NotifyPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

    }
}