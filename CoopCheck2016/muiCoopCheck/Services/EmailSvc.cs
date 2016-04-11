using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace CoopCheck.WPF.Services
{
    public static class EmailSvc
    {
        public static void SendEMailThroughOutlook()
        {
            try
            {
                // Create the Outlook application.
                Outlook.Application oApp = new Outlook.Application();
                // Create a new mail item.
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                // Set HTMLBody. 
                //add the body of the email
                oMsg.HTMLBody = "Hello, Jawed your message body will go here!!";
                //Add an attachment.
                String sDisplayName = "MyAttachment";
                int iPosition = (int)oMsg.Body.Length + 1;
                //int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                //now attached the file
                //Outlook.Attachment oAttach = oMsg.Attachments.Add(@"C:\\fileName.jpg", iAttachType, iPosition, sDisplayName);
                //Subject line
                oMsg.Subject = "Your Subject will go here.";
                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // Change the recipient in the next line if necessary.
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add("fmedvedik+coopcheck@gmail.com");
                oRecip.Resolve();
                // Send.
                oMsg.Send();
                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oApp = null;
            }//end of try block
            catch (Exception ex)
            {
            }//end of catch
        }//end of Email Method
        public static void CreateOutlookEmail()
        {
            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
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
