using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Services
{
    public class UserAuthSvc : IUserAuthSvc

    {
        private static NameValueCollection _settings;
        private static PrincipalContext _principalContext;

        public UserAuthSvc()
        {
            _settings = ConfigurationManager.AppSettings;
        }
        public  bool CanRead(string userName)
        {
            return IsGroupMember(userName, _settings["ReadAuth"]);
            //return true;

        }


        public PrincipalContext PrincipalContext
        {
            get { return _principalContext ?? (_principalContext = new PrincipalContext(ContextType.Domain)); }
            set { _principalContext = value; }
        }

        public bool CanWrite(string userName)
        {
            return IsGroupMember(userName, _settings["WriteAuth"]);
            //return true;
           
        }

        private bool IsGroupMember(string userName, string Group)
        {
//#if DEBUG
//            return true;
//            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
//            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "10.0.0.2", "fmedvedik", "(manos)3k");
//            //  PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
//            //  PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com");
//#else
//            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com");
            


            var findByIdentity = UserPrincipal.FindByIdentity(_principalContext, userName);
            bool retVal = false;
            if (findByIdentity != null)
            {
                List<string> result;
                using (var src = findByIdentity.GetGroups(_principalContext))
                {
                    result = new List<string>();
                    src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
                }
                var l = result.FirstOrDefault(s => s.Equals(Group));
                retVal = (l != null);
            }
            return retVal;
        }
    }
}