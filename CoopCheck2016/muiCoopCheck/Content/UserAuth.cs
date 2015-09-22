using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using CoopCheck.WPF.ViewModel;

namespace CoopCheck.WPF.Content
{
        public sealed class UserAuth : ViewModelBase
        {
            private static volatile UserAuth instance;
            private static object syncRoot = new Object();
            private UserAuth() { }

            public static UserAuth Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (syncRoot)
                        {
                            if (instance == null)
                                instance = new UserAuth();
                        }
                    }

                    return instance;
                }
            }

            public UserSecurityProfile UserSecurityProfile
        {
                get { return instance._userSecurityProfile; }
                set
                {
                    instance._userSecurityProfile = value;
                    NotifyPropertyChanged();
                }
            }

        private UserSecurityProfile _userSecurityProfile = new UserSecurityProfile();
   
        public String UserName
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
