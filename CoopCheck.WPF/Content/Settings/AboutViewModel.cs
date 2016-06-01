using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    ///     A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        private string _userName;

        private ObservableCollection<string> _userRights;

        public AboutViewModel()
        {
#if DEBUG

            UserName = UserAuth.Instance.UserName;
#else
            UserName = Environment.UserDomainName  + @"\" + Environment.UserName;
#endif
            var rights = new List<string>();

            rights.Add(UserAuth.Instance.CanRead ? "Can Read" : "No Read");
            rights.Add(UserAuth.Instance.CanWrite ? "Can Write" : "No Write");
            UserRights = new ObservableCollection<string>(rights);
        }


        public ObservableCollection<string> UserRights
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

        public string PublishedVersion
        {
            get { return GetPublishedVersion(); }
        }

        public string UserName
        {
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


        private string GetPublishedVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                return ApplicationDeployment.CurrentDeployment.
                    CurrentVersion.ToString();
            }
            return "Not network deployed";
        }
    }
}