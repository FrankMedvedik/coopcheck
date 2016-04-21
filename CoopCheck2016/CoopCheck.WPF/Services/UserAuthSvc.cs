﻿using System.Threading;
using CoopCheck.WPF.Properties;

namespace CoopCheck.WPF.Services
{
    public class UserAuthSvc
    {
        public static bool CanRead(string userName)
        {
            //return true;
            return IsGroupMember(userName, Settings.Default.ReadAuth);
        }

        public static bool CanWrite(string userName)
        {
            // return true;
            return IsGroupMember(userName, Settings.Default.WriteAuth);
        }

        private static bool IsGroupMember(string userName, string Group)
        {
            return true;
            return Thread.CurrentPrincipal.IsInRole(Group);

//#if DEBUG
//            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
//            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "10.0.0.2", "fmedvedik", "(manos)3k");
//       //   PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
//#else
//        //    PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
//#endif

//            var findByIdentity = UserPrincipal.FindByIdentity(ctx, userName);
//            bool retVal = false;
//            if (findByIdentity != null)
//            {
//                List<string> result;
//                using (var src = findByIdentity.GetGroups(ctx))
//                {
//                    result = new List<string>();
//                    src.ToList().ForEach(sr => result.Add(sr.SamAccountName));
//                }
//                var l = result.FirstOrDefault(s => s.Equals(Group));
//                retVal = (l != null);
//            }
//            ctx.Dispose();
//            return retVal;
        }
    }
}