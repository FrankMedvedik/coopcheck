using System;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace CoopCheck.WPF.Services
{
    public static class EmailSvc
    {
        public static void SendEMailThroughOutlook()
        {
            try
            {
                // Create the Outlook application.
                var oApp = new Application();
                // Create a new mail item.
                var oMsg = (MailItem) oApp.CreateItem(OlItemType.olMailItem);
                // Set HTMLBody. 
                //add the body of the email
                oMsg.HTMLBody = "Hello, Jawed your message body will go here!!";
                //Add an attachment.
                var sDisplayName = "MyAttachment";
                var iPosition = oMsg.Body.Length + 1;
                //int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                //now attached the file
                //Outlook.Attachment oAttach = oMsg.Attachments.Add(@"C:\\fileName.jpg", iAttachType, iPosition, sDisplayName);
                //Subject line
                oMsg.Subject = "Your Subject will go here.";
                // Add a recipient.
                var oRecips = oMsg.Recipients;
                // Change the recipient in the next line if necessary.
                var oRecip = oRecips.Add("fmedvedik+coopcheck@gmail.com");
                oRecip.Resolve();
                // Send.
                oMsg.Send();
                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oApp = null;
            } //end of try block
            catch (Exception ex)
            {
            } //end of catch
        } //end of Email Method

        public static void CreateOutlookEmail()
        {
            try
            {
                var outlookApp = new Application();
                var mailItem = (MailItem) outlookApp.CreateItem(OlItemType.olMailItem);
                mailItem.Subject = "This is the subject";
                mailItem.To = "fmedvedik@gmail.com";
                mailItem.Body = "This is the message.";
                mailItem.Display(true);
            }
            catch (Exception eX)
            {
                throw new Exception("cDocument: Error occurred trying to Create an Outlook Email"
                                    + Environment.NewLine + eX.Message);
            }
        }
    }
}