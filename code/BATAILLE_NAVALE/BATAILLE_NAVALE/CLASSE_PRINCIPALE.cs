using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using BIBLIOTHEQUE_INTERACTIONS_API;
using BIBLIOTHEQUE_LOGIQUE_JEU;


namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE
{
    internal class CLASSE_PRINCIPALE
    {

        static void Main(string[] args)
        {
            string url = "https://api-lprgi.natono.biz/api/GetConfig";
            string key = "lprgi_api_key_2023";

            API api = new API(url, key);

            string json = api.RecupererData().Result;

            // Afficher les données du JsonArray
            Console.WriteLine("Affichage des données du JsonArray");
            Console.WriteLine(json);
        }
    }
}
