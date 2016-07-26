using System;
using System.DirectoryServices.AccountManagement;
using System.Net.Mail;
using System.Reflection;
using log4net;

namespace CoopCheck.Web.Services
{
    public static class SendMailSvc
    {
        private static readonly ILog log
            = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendEmail(string to, string subject, string body)
        {
            try
            {
                var from = "fmedvedik@reckner.com";
                var _ms = "exch1.reckner.com";
                var msg = new MailMessage(from, to, subject, body);
                var sc = new SmtpClient(_ms);
                sc.Send(msg);
            }
            catch (Exception e)
            {
                log.Error(string.Format("EMAIL FAILED TO SEND Error: {0}", e.Message));
            }
        }

        public static string uEmail(string username)
        {
            //PrincipalContext pctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
            var pctx = new PrincipalContext(ContextType.Domain);
            using (var up = UserPrincipal.FindByIdentity(pctx, username))
            {
                return up != null && !string.IsNullOrEmpty(up.EmailAddress) ? up.EmailAddress : string.Empty;
            }
        }
    }
}