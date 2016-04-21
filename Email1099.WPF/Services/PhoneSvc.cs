using System;
using System.Collections.Generic;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Extensibility;

namespace Reckner.WPF.Services
{
    public static class PhoneSvc
    {
        public static async void PlaceAudioCall(string phoneNumber)
        {
            var client = LyncClient.GetClient(); 

            var participantUri = new List<string> { phoneNumber };
            var modalitySettings = new Dictionary<AutomationModalitySettings, object>();
            LyncClient.GetAutomation().BeginStartConversation(AutomationModalities.Audio,participantUri,modalitySettings, ar => {},null);
        }
        public static List<String> SearchForContacts(string searchKey)
        {
            List<String> contacts = new List<string>();

            LyncClient.GetClient().ContactManager.BeginSearch(searchKey,
            (ar) =>
            {
                var searchResults = LyncClient.GetClient().ContactManager.EndSearch(ar);
                foreach (Contact contact in searchResults.Contacts)
                {
                    contacts.Add(contact.GetContactInformation(ContactInformationType.DisplayName).ToString());
                }
            },null);
            return contacts;
        }
    }
}

