
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using CoopCheck.WPF.Services;

namespace CoopCheck.WPF.Models
{
    public class UserSecurityProfile
    {
        private bool _canRead = false;
        private bool _canWrite = false;

        public UserSecurityProfile()
        {
            // move these to gets? add some logic so we get the full username and domain to the ui for testing

            CanRead = UserAuthSvc.CanRead(UserName);
            CanWrite = UserAuthSvc.CanWrite(UserName);
        }

        public bool CanRead
        {
            get
            {
                return _canRead;
            }
            set { _canRead = value; }
        }

        public bool CanWrite
        {
            get { return _canWrite; }
            set { _canWrite = value; }
        }

        public string UserName
        {
            get 
            {

#if DEBUG
                //  return @"reckner\fmedvedik";
                // return @Environment.UserDomainName + @"\" + Environment.UserName;
                return Environment.UserName;
#else
                return Environment.UserDomainName  + @"\" + Environment.UserName;
#endif
            }
        }



}
}
