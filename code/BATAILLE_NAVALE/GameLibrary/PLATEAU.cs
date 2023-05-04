using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace BIBLIOTHEQUE_LOGIQUE_JEU
{
    public class PLATEAU
    {
        // Attributs

        private GRILLE _GRILLE_JOUEUR_A, _GRILLE_JOUEUR_B;
        private int[,] _GRILLE_ENEMI_DE_A, _GRILLE_ENEMI_DE_B;
        private bool _WIN;

        // Constructeurs

        public PLATEAU(GRILLE grille_joueur_a, GRILLE grille_joueur_b, int[,] grille_enemi_de_a, int[,] grille_enemi_de_b, bool win)
        {
            _GRILLE_JOUEUR_A = grille_joueur_a;
            _GRILLE_JOUEUR_B = grille_joueur_b;
            _GRILLE_ENEMI_DE_A = grille_enemi_de_a;
            _GRILLE_ENEMI_DE_B = grille_enemi_de_b;
            _WIN = win;
        }

        public PLATEAU() : this(new GRILLE(), new GRILLE(), new int[0,0], new int[0,0], false) { }

        public PLATEAU(PLATEAU P) : this(P.GRILLE_JOUEUR_A, P.GRILLE_JOUEUR_B, P.GRILLE_ENEMI_DE_A, P.GRILLE_ENEMI_DE_B, P.WIN) { }

        public PLATEAU(string json)
        {
            // Récupération des données
            int[] taille = recupererTailleGrille(json);
            List<BATEAU> bateaux= recupererListeBateaux(json);

            // GRILLE joueur A & B

            _GRILLE_JOUEUR_A = new GRILLE(taille[0], taille[1], bateaux);
            _GRILLE_JOUEUR_B = new GRILLE(_GRILLE_JOUEUR_A);

            // GRILLE enemi de A & B
            int[,] grille = new int[taille[0], taille[1]];
            for (int i = 0; i < grille.GetLength(0); i++)
            {
                for (int j = 0; j < grille.GetLength(1); j++)
                {
                    grille[i, j] = -1;
                }
            }

            _GRILLE_ENEMI_DE_A = grille;
            _GRILLE_ENEMI_DE_B = grille;

            _WIN = false;
        }

        // Getters & Setters

        public GRILLE GRILLE_JOUEUR_A
        {
            get { return _GRILLE_JOUEUR_A; }
            set { _GRILLE_JOUEUR_A = value; }
        }

        public GRILLE GRILLE_JOUEUR_B
        {
            get { return _GRILLE_JOUEUR_B; }
            set { _GRILLE_JOUEUR_B = value; }
        }

        public int[,] GRILLE_ENEMI_DE_A
        {
            get { return _GRILLE_ENEMI_DE_A; }
            set { _GRILLE_ENEMI_DE_A = value; }
        }

        public int[,] GRILLE_ENEMI_DE_B
        {
            get { return _GRILLE_ENEMI_DE_B; }
            set { _GRILLE_ENEMI_DE_B = value; }
        }

        public bool WIN
        {
            get { return _WIN; }
            set { _WIN = value; }
        }

        // Méthodes

        public static List<BATEAU> recupererListeBateaux(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            JArray bateauxArray = (JArray)jsonObject["bateaux"];

            List<BATEAU> listeBateaux = new List<BATEAU>();
            foreach (JObject bateauObject in bateauxArray)
            {
                BATEAU bateau = bateauObject.ToObject<BATEAU>();
                listeBateaux.Add(bateau);
            }

            return listeBateaux;
        }

        public static int[] recupererTailleGrille(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            int nbLignes = (int)jsonObject["nbLignes"];
            int nbColonnes = (int)jsonObject["nbColonnes"];

            return new int[2] { nbLignes, nbColonnes };
        }
    }
}
