using System;

namespace Email1099.WPF.Models
{
    [Serializable]
    public class StatusInfo
    {
        private string _errorMessage;
        private string _statusMessage;
        private bool _isBusy = false; // set to false by default to cancel previous busy...
//        private bool _showMessageBox = false;
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
            get { return "GET CURRENT USER"; }
        }
    }
}
