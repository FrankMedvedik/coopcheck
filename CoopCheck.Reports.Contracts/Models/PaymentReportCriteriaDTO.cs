using System;
using System.Collections.Generic;
using CoopCheck.Reports.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Models
{
    public class PaymentReportCriteriaDto : IPaymentReportCriteria
    {
        private string _jobNumber;
        public string JobNumber
        {
            get { return _jobNumber; }
            set { _jobNumber = value.Trim();  }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value.AddDays(1).AddSeconds(-1);  }
        }

        private string _checkNumber;
        public string CheckNumber
        {
            get { return _checkNumber; }
            set {
                _checkNumber = value.Trim(); 
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value.Trim();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value.Trim();
            }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName  = value.Trim();
            }
        }


        private bool _isPrinted;
        public bool IsPrinted
        {
            get { return _isPrinted; }
            set
            {
                _isPrinted = value;
            }
        }

        private bool _isCleared;
        public bool IsCleared
        {
            get { return _isCleared; }
            set
            {
                _isCleared = value;
            }
        }

        private string _firstName;

        private BankAccount _account;
        private int? _batchNumber;

        public BankAccount Account
        {
            get { return _account; }
            set
            {
                _account = value;
            }
        }



        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value.Trim();
            }
        }

        public int? BatchNumber
        {
            get { return _batchNumber; }
            set
            {
                _batchNumber = value;
                
            }
        }

        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            if (Account != null)
                v.Add(new KeyValuePair<string, string>("Account", Account.account_name));
            if (BatchNumber != null)
                v.Add(new KeyValuePair<string, string>("Batch", BatchNumber.ToString()));
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
            foreach (var a  in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }

        public string ToSummaryFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;

            if (Account != null)
                v.Add(new KeyValuePair<string, string>("Account", Account.account_name));
                v.Add(new KeyValuePair<string, string>("StartDate", StartDate.ToString("yyyyMMdd")));
                v.Add(new KeyValuePair<string, string>("EndDate", EndDate.ToString("yyyyMMdd")));
            foreach (var a in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }
    }
}

