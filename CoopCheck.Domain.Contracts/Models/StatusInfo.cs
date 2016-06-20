using System;
using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    [Serializable]
    public class StatusInfo : IStatusInfo
    {
        private string _errorMessage;
        private string _statusMessage;
        private bool _isBusy = false; // set to false by default to cancel previous busy...
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }
        //public bool ShowMessageBox
        //{
        //    get { return _showMessageBox; }
        //    set { _showMessageBox = value; }
        //}

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

        public string whoami
        {
            get { return Environment.UserName; }
        }
    }
}
