using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BIBLIOTHEQUE_INTERACTIONS_API
{
    public class API
    {
        private string _URL, _KEY;

        public API()
        {
            _URL= "";
            _KEY = "";
        }

        public API(string url, string key)
        {
            _URL = url;
            _KEY = key;
        }

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        public string KEY
        {
            get { return _KEY; }
            set { _KEY = value; }
        }

        public async Task<string> RecupererData()
        {
            Console.WriteLine("Récupération des données de l'API en cours...");
            string json = string.Empty;
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    wc.Headers.Add("x-functions-key", _KEY);
                    json = wc.DownloadString(_URL);
                }

                Console.WriteLine("Données récupérées avec succès.");
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la récupération des données : " + ex.Message);
                return null;
            }
        }
    }
}
