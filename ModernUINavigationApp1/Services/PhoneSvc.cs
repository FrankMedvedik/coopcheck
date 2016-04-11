using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;

namespace Email1099.WPF.Services
{
    public static class PhoneSvc
    {
        public static async void PlaceAudioCall(string phoneNumber)
        {
            var client = LyncClient.GetClient(); 

            //if (client.State == (Microsoft.Lync.Model.ClientState) ClientState.SignedIn)
            //{
            //    MessageBox.Show("Please Sign in to Lync first ");
            //    return;
            //}

            /* setup making the call  */
            var participantUri = new List<string> { phoneNumber };
            var modalitySettings = new Dictionary<AutomationModalitySettings, object>();
            //{
            //    {AutomationModalitySettings.ApplicationId, ConditionalConfiguration.RecknerCallAppGuid},
            //    {AutomationModalitySettings.Subject, _vm.SelectedMyCallback.callbackNum},
            //    {AutomationModalitySettings.ApplicationData, _vm.SelectedAppData}
            //};

     

            LyncClient.GetAutomation().BeginStartConversation(AutomationModalities.Audio,participantUri,modalitySettings, ar =>                {
                    try
                    {
                        ConversationWindow newWindow = LyncClient.GetAutomation().EndStartConversation(ar);
                        //newWindow.BeginOpenExtensibilityWindow(
                        //    newWindow.EndOpenExtensibilityWindow,
                        //    null);
                    }
                    catch (Exception oe)
                    {
                        MessageBox.Show("Can't Open Output Windows: Operation exception on start conversation " +
                                        oe.Message);

                    }
                },
                null);
        }

    }
}
