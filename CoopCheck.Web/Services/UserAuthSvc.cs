using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace CoopCheck.Web.Services
{
    public static class UserAuthSvc
    {
        public static bool CanRead(string userName)
        {
            //return true;
            return IsGroupMember(userName, "CoopCheckread");
        }

        public static bool CanWrite(string userName)
        {
            // return true;
            return IsGroupMember(userName, "CoopCheckAdmin");
        }

        private static bool IsGroupMember(string userName, string Group)
        {
            var ctx = new PrincipalContext(ContextType.Domain, "reckner.com");
            var findByIdentity = UserPrincipal.FindByIdentity(ctx, userName);
            var retVal = false;
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
        }
    }
}