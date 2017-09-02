﻿using System;
using System.Collections.Generic;
using Exception = System.Exception;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace Reckner.WPF.Services
{
    public static class EmailSvc
    {
        public static bool CheckConnection()
        {

            return true;
        }

        public static void SendEMailThroughOutlook(List<string> toList, string subject, string msg, string attachementFileName = null  )
        {
                // Create the Outlook application.
                Outlook.Application oApp = new Outlook.Application();
                // Create a new mail item.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                // Set HTMLBody. 
                //add the body of the email
                oMsg.HTMLBody += msg;
                //Add an attachment.
                 String sDisplayName = "Coopcheck Log";
                int iPosition = (int)oMsg.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                // now attached the file
                Outlook.Attachment oAttach = oMsg.Attachments.Add(attachementFileName, iAttachType, iPosition, sDisplayName);
                //Subject line
                oMsg.Subject = subject;
                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // Change the recipient in the next line if necessary.
                foreach (var i in toList)
                {
                    Outlook.Recipient oRecip = (Outlook.Recipient) oRecips.Add(i);
                    oRecip.Resolve();
                }
                oMsg.Send();
            }
        public static void CreateOutlookEmail(List<string> toList, string subject, string msg, string attachementFileName = null)
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.Subject = subject;
                Outlook.Recipients oRecips = mailItem.Recipients;
                if (toList != null)
                {
                    foreach (var i in toList)
                    {
                        Outlook.Recipient oRecip = oRecips.Add(i);
                        oRecip.Resolve();
                    }
                }
                
                if (attachementFileName != null)
                {
                    //int iPosition = (int) mailItem.Body.Length + 1;
                    //int iAttachType = (int) Outlook.OlAttachmentType.olByValue;
                    //now attached the file
                    mailItem.Attachments.Add(attachementFileName);
                }
                mailItem.HTMLBody += msg;
                mailItem.Send();
            }
            catch (Exception eX)
            {
                throw new Exception("cDocument: Error occurred trying to Create an Outlook Email"
                                    + Environment.NewLine + eX.Message);
            }
        }
    }
}
