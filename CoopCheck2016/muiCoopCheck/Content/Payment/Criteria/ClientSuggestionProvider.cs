using System.Collections.Generic;
using System.Linq;
using CoopCheck.Repository;
using CoopCheck.WPF.Services;
using WpfControls.Editors;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public class ClientSuggestionProvider : ISuggestionProvider
    {

        public ClientSuggestionProvider()
        {
            Clients = new List<CoopCheckClient>(ClientSvc.GetClients().Result);
        }
        private List<CoopCheckClient> _clients;
        public List<CoopCheckClient> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
            }
        }


        public System.Collections.IEnumerable GetSuggestions(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return null;
            }
            if (filter.Length < 2)
            {
                return null;
            }
            return Clients.Where((a => a.ClientID.Contains(filter) || (a.ClientName.Contains(filter))));
        }

    }

}

