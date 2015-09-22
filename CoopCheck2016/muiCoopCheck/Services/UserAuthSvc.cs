using System;
using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Windows;

namespace CoopCheck.WPF.Services
{
    public  class UserAuthSvc
    {
           
        public static Boolean CanRead(string userName)
        {
            return IsGroupMember(userName, Properties.Settings.Default.ReadAuth);
        }

        public static Boolean CanWrite(string userName)
        {
            return IsGroupMember(userName, Properties.Settings.Default.WriteAuth);
        }

        private static Boolean IsGroupMember(string userName, string Group)
        {
#if DEBUG
             PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
           //PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
#else
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
#endif

            var findByIdentity = UserPrincipal.FindByIdentity(ctx, userName);
            bool retVal = false;
            if (findByIdentity != null)
            {
                List<string> result;
                using (var src = findByIdentity.GetGroups(ctx))
                {
                    result = new List<string>();
                    src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
                }
                var l = result.FirstOrDefault(s => s.Equals(Properties.Settings.Default.WriteAuth));
                retVal = (l != "");
            }
            ctx.Dispose();
            return retVal;
        }
    }
}

