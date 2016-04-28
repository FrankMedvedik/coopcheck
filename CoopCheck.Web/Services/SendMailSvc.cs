using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Web;

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

                string from = "Fmedvedik@reckner.com";
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


    }
}