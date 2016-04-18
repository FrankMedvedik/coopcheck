using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {

        private ObservableCollection<string> _userRights;
        private string _userName;
        public AboutViewModel()
        {
#if DEBUG

            UserName = UserAuth.Instance.UserName;
#else
            UserName = Environment.UserDomainName  + @"\" + Environment.UserName;
#endif
            List<string> rights = new List<string>();
             
            rights.Add(UserAuth.Instance.CanRead ? "Can Read" : "No Read");
            rights.Add(UserAuth.Instance.CanWrite ? "Can Write" : "No Write");
            UserRights = new ObservableCollection<string>(rights);
        }

        
        public ObservableCollection<string >   UserRights
        {
            get { return _userRights; }
            set
            {
                if (_userRights != value)
                {
                    _userRights = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String PublishedVersion
        {
            get { return GetPublishedVersion(); }
        }


        private string GetPublishedVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.
                    CurrentVersion.ToString();
            }
            return "Not network deployed";
        }
        public String UserName        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
