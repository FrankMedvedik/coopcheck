using System;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Models
{
    public class PaymentFinderCriteria : ViewModelBase
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
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
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