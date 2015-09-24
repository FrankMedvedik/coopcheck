﻿using System;
using System.Collections.Generic;
using CoopCheck.Repository;
using CoopCheck.WPF.Content;
using CoopCheck.WPF.ViewModel;

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


        private bool _isPrinted;
        public bool IsPrinted
        {
            get { return _isPrinted; }
            set
            {
                _isPrinted = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isCleared;
        public bool IsCleared
        {
            get { return _isCleared; }
            set
            {
                _isCleared = value;
                NotifyPropertyChanged();
            }
        }

        private string _firstName;

        private bank_account _account;
        public bank_account Account
        {
            get { return _account; }
            set
            {
                _account = value;
                NotifyPropertyChanged();
            }
        }



        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;

            if (Account != null)
                v.Add(new KeyValuePair<string, string>("Account", Account.account_name));
            if (JobNumber != null)
                v.Add(new KeyValuePair<string, string>("JobNum", JobNumber.ToString()));
            v.Add(new KeyValuePair<string, string>("StartDate", StartDate.ToString("yyyyMMdd")));
            v.Add(new KeyValuePair<string, string>("EndDate", EndDate.ToString("yyyyMMdd")));
            if (CheckNumber != null)
                v.Add(new KeyValuePair<string, string>("CheckNumber", CheckNumber));
            if (PhoneNumber != null)
                v.Add(new KeyValuePair<string, string>("PhoneNumber", CheckNumber));
            if (Email != null)
                v.Add(new KeyValuePair<string, string>("Email", Email));
            if (LastName != null)
                v.Add(new KeyValuePair<string, string>("LastName", LastName));
            if (FirstName != null)
                v.Add(new KeyValuePair<string, string>("FirstName", FirstName));
            if (IsCleared)
                v.Add(new KeyValuePair<string, string>("IsCleared", "Cleared"));
            if (IsPrinted)
                v.Add(new KeyValuePair<string, string>("IsPrinted", "Printed"));
            foreach (var a  in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }
    }
}