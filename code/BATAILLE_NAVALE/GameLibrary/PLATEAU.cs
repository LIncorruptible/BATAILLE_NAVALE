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

        public bool validationCoordonnees(int[] coords)
        {
            // Les coordonnées sont-elles dans la grille ?
            if (coords[0] < 0 || coords[0] >= _GRILLE_JOUEUR_A.HAUTEUR || coords[1] < 0 || coords[1] >= _GRILLE_JOUEUR_A.LARGEUR)
            {
                return false;
            }

            // Les coordonnées sont-elles déjà utilisées ?
            if (_GRILLE_ENEMI_DE_A[coords[0], coords[1]] != -1)
            {
                return false;
            }

            return true;
        }

        public bool estCoulee(PIECE_DE_JEU piece_de_jeu)
        {
            foreach(POINT point in piece_de_jeu.VECTEUR) 
            {
                if(point.TOUCHE = false) return false; 
            }

            return true;
        }
        
        public bool etatPieceDeJeu(POINT point, PIECE_DE_JEU piece_de_jeu)
        {
            if (point.TOUCHE)
            {
                return false;
            } 
            else
            {
                // On met le point à TOUCHE dans la pièce de jeu
                piece_de_jeu.VECTEUR.Find(p => p.X == point.X && p.Y == point.Y).TOUCHE = true;

                return (estCoulee(piece_de_jeu)) ? true : false;
            }
        }

        public bool jouerLeTour(int joueur_id, int[] coords)
        {
            bool valide = validationCoordonnees(coords);

            if(valide)
            {
                int piece_id;

                switch (joueur_id)
                {
                    case 1:
                        piece_id = _GRILLE_JOUEUR_B.POSITONS_IDS[coords[0], coords[1]];
                        break;
                    case 2:
                        piece_id = _GRILLE_JOUEUR_A.POSITONS_IDS[coords[0], coords[1]];
                        break;
                    default:
                        piece_id = -1;
                        break;
                }

                if (piece_id > 0)
                {
                    // On récupère le POINT de la pièce correspondant aux coordonnées
                    POINT point = new POINT();

                    switch (joueur_id)
                    {
                        case 1:
                            point = _GRILLE_JOUEUR_B.PIECES_DE_JEU[piece_id].VECTEUR.Find(p => p.X == coords[0] && p.Y == coords[1]);
                            break;
                        case 2:
                            point = _GRILLE_JOUEUR_A.PIECES_DE_JEU[piece_id].VECTEUR.Find(p => p.X == coords[0] && p.Y == coords[1]);
                            break;
                        default:
                            break;
                    }

                    // On met à jour l'état de la pièce de jeu et on retourne le résultat
                    bool etat = etatPieceDeJeu(point, _GRILLE_JOUEUR_A.PIECES_DE_JEU[piece_id]);

                    return etat;
                }

                return false;
            }

            return valide;
        }
    }
}
