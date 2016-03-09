using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.Repository;
using CoopCheck.WPF.Services;
using FirstFloor.ModernUI.Presentation;
using WpfControls.Editors;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    public class ClientSuggestionProvider : ISuggestionProvider
    {

        public ClientSuggestionProvider()
        {
            Clients = new List<client>(ClientSvc.GetClients().Result);
        }
        private List<client> _clients;
        public List<client> Clients
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

