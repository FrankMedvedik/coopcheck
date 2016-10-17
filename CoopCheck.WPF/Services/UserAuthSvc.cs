using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading;
using CoopCheck.WPF.Properties;

namespace CoopCheck.WPF.Services
{
    public static class UserAuthSvc
    {
        public static bool CanRead(string userName)
        {
            return IsGroupMember(userName, Settings.Default.WriteAuth);
            //return true;
            
        }
        public static bool CheckConnection()
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com"))
                {
                    var stream = ctx.UserName;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

          public static bool CanWrite(string userName)
        {
            return IsGroupMember(userName, Settings.Default.WriteAuth);
            //return true;
           
        }

        private static bool IsGroupMember(string userName, string Group)
        {
#if DEBUG
            return true;
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "10.0.0.2", "fmedvedik", "(manos)3k");
            //  PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            //  PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com");
#else
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com");
            


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
                var l = result.FirstOrDefault(s => s.Equals(Group));
                retVal = (l != null);
            }
            ctx.Dispose();
            return retVal;
#endif
        }
    }
}