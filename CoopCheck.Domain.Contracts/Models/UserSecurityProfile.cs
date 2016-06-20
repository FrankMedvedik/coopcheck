using System;
using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class UserSecurityProfile : IUserSecurityProfile
    {
        private bool _canRead = false;
        private bool _canWrite = false;
        private readonly IUserAuthSvc _userAuthSvc;
        public UserSecurityProfile(IUserAuthSvc userAuthSvc)
        {
            _userAuthSvc = userAuthSvc;
        }
        public UserSecurityProfile()
        {
            // move these to gets? add some logic so we get the full username and domain to the ui for testing

            CanRead = _userAuthSvc.CanRead(UserName);
            CanWrite = _userAuthSvc.CanWrite(UserName);
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
                return Environment.UserName;
            }
        }

}

 
}
