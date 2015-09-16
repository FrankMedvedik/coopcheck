﻿
namespace CoopCheck.WPF.Models
{
    public class StatusInfo
    {
        private string _errorMessage;
        private string _statusMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set { _statusMessage = value; }
        }
    }
}
