using System;
using System.DirectoryServices.AccountManagement;
using System.Net.Mail;

namespace CoopCheck.Web.Services
{
    public static class SendMailSvc
    {
        private static readonly log4net.ILog log
         = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendEmail(string to, string subject, string body)
        {
            try
            {

                string from = "fmedvedik@reckner.com";
                string _ms = "email.reckner.com";
                MailMessage msg = new MailMessage(from, to, subject, body);
                SmtpClient sc = new SmtpClient(_ms);
                sc.Send(msg);
            }
            catch (Exception e)
            {
                log.Error(String.Format("EMAIL FAILED TO SEND Error: {0}", e.Message));
            }
        }

        public static string uEmail(string username)
        {
                //PrincipalContext pctx = new PrincipalContext(ContextType.Domain, "reckner.com", "fmedvedik", "(manos)3k");
                PrincipalContext pctx = new PrincipalContext(ContextType.Domain);
                using (UserPrincipal up = UserPrincipal.FindByIdentity(pctx, username))
                {
                    return up != null && !String.IsNullOrEmpty(up.EmailAddress) ? up.EmailAddress : string.Empty;
                }
            
        }


    }
}