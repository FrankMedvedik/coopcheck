using Reckner.WPF.ViewModel;

namespace CoopCheck.WPF.Content
{
    public sealed class UserAuth : ViewModelBase
    {
        private static volatile UserAuth _instance;
        private static readonly object SyncRoot = new object();

        private UserSecurityProfile _userSecurityProfile = new UserSecurityProfile();

        private UserAuth()
        {
        }

        public static UserAuth Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new UserAuth();
                    }
                }

                return _instance;
            }
        }

        public UserSecurityProfile UserSecurityProfile
        {
            get { return _instance._userSecurityProfile; }
            set
            {
                _instance._userSecurityProfile = value;
                NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userSecurityProfile.UserName; }
        }

        public bool CanRead
        {
            get { return _userSecurityProfile.CanRead; }
        }

        public bool CanWrite
        {
            get { return _userSecurityProfile.CanWrite; }
        }
    }
}