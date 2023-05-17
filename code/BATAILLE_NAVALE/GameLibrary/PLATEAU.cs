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

        public List<BATEAU> recupererListeBateaux(string json)
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

        public int[] recupererTailleGrille(string json)
        {
            JObject jsonObject = JObject.Parse(json);
            int nbLignes = (int)jsonObject["nbLignes"];
            int nbColonnes = (int)jsonObject["nbColonnes"];

            return new int[2] { nbLignes, nbColonnes };
        }

        // Méthodes décrites dans le pseudo code

        public bool coordonneesDansLaGrille(int[] coords, int id_joueur)
        {
            bool valide = false;

            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            // Les coordonnées sont-elles dans la grille ?
            // Pour x
            if (coords[0] >= 0 && coords[0] < grille.LARGEUR)
            {
                // Pour y
                if (coords[1] >= 0 && coords[1] < grille.HAUTEUR)
                {
                    valide = true;
                }
            }

            return valide;
        }

        public bool coordonneesSontLibres(int[] coords, int id_joueur)
        {
            bool libre = false;

            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            // Les coordonnées sont-elles libres ?
            if (grille.POSITONS_IDS[coords[0], coords[1]] == 0)
            {
                libre = true;
            }

            return libre;
        }

        public bool coordonneesDejaTouchees(int[] coords, int id_joueur, int id_piece_de_jeu)
        {
            bool dejaTouche = false;

            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            // Les coordonnées ont-elles déjà touchées une pièce ?
            foreach(PIECE_DE_JEU piece in grille.PIECES_DE_JEU) {
                if(piece.ID == id_piece_de_jeu)
                {
                    foreach(POINT point in piece.VECTEUR)
                    {
                        if(point.X == coords[0] && point.Y == coords[1])
                        {
                            dejaTouche = true;
                        }
                    }
                }
            }

            return dejaTouche;
        }

        public void toucherPieceAuxCoordonnees(int[] coords, int id_joueur, int id_piece_de_jeu)
        {
            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;
            
            // On touche la pièce aux coordonnées
            foreach (PIECE_DE_JEU piece in grille.PIECES_DE_JEU)
            {
                if (piece.ID == id_piece_de_jeu)
                {
                    foreach (POINT point in piece.VECTEUR)
                    {
                        if (point.X == coords[0] && point.Y == coords[1])
                        {
                            point.TOUCHE = true;
                        }
                    }
                }
            }
        }

        public int[,] traduireGrille()
        {

        }

        public List<int[,]> traiterReponse(int[] reponse_affichage)
        {
            // Réponse reçu dans reponse affichage : id_init | x | y | id_joueur | id_piece_de_jeu
            // Réponse à envoyer dans reponse logique : grille_a | grille_b | valid

            List<int[,]> reponse_logique = new List<int[,]>();
            int[,] partieGagne = new int[1, 1] { { 0 } };

            // Retraduction de la réponse
            int 
                id_init = reponse_affichage[0], // 0 si c'est l'initialisation de départ
                id_joueur = reponse_affichage[3], // 1 si c'est le joueur A, 2 si c'est le joueur B
                id_piece_de_jeu = reponse_affichage[4]; // numéro de la pièce de jeu concernée

            int[]
                coords = new int[2] { reponse_affichage[1], reponse_affichage[2] };

            if(reponse_affichage.Length < 1) // Si c'est l'initialisation de départ
            {
                
            }
            else // Sinon, c'est une réponse à l'affichage
            {
                if (id_init == 0) // Si la réponse est 0, c'est la partie positions des pièces du jeu
                {

                }
                else // Sinon, c'est la partie de jeu
                {
                    id_joueur = (id_joueur == 1) ? 2 : 1; // On inverse le joueur

                    if (coordonneesDansLaGrille(coords, id_joueur) == true)
                    {
                        if (coordonneesDejaTouchees(coords, id_joueur, id_piece_de_jeu) == false)
                        {
                            toucherPieceAuxCoordonnees(coords, id_joueur, id_piece_de_jeu);

                            reponse_logique.Add(traduireGrille());
                        }
                    }
                }
            }

            return reponse_logique;
        }
    }
}
