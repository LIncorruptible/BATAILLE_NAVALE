using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                    grille[i, j] = 0;
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

        // Méthodes décrites dans le pseudo code : LOGIQUE

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

        public bool pieceDejaPlacee(int id_joueur, int id_piece_de_jeu)
        {
            GRILLE G = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            // La pièce a-t-elle déjà été placée ?
            bool placee = false;

            foreach (PIECE_DE_JEU piece in G.PIECES_DE_JEU)
            {
                if (piece.ID == id_piece_de_jeu)
                {
                    foreach(POINT point in piece.VECTEUR)
                    {
                        if(point.X != -1 && point.Y != -1)
                        {
                            placee = true;
                            break;
                        }
                    }
                    break;
                }
            }

            return placee;
        }

        public bool emplacementLibrePourPlacer(int[] coords, int id_joueur, int id_piece_de_jeu, int id_orientation)
        {
            GRILLE G = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            int taille = 0;
            // On récupère la taille de la pièce
            foreach (PIECE_DE_JEU piece in G.PIECES_DE_JEU)
            {
                if (piece.ID == id_piece_de_jeu)
                {
                    taille = piece.BATEAU.TAILLE;
                    break;
                }
            }
            
            List<POINT> vecteur = new List<POINT>();
            switch (id_orientation)
            {
                case 0: // Vertical
                    for (int i = 0; i < taille; i++)
                    {
                        vecteur.Add(new POINT(coords[0], coords[1] + i));
                    }
                    break;
                default: // Horizontal
                    for (int i = 0; i < taille; i++)
                    {
                        vecteur.Add(new POINT(coords[0] + i, coords[1]));
                    }
                    break;
            }

            bool libre = true;

            // On vérifie que les coordonnées sont dans la grille
            foreach (POINT point in vecteur)
            {
                if (!coordonneesDansLaGrille(new int[2] { point.X, point.Y }, id_joueur))
                {
                    libre = false;
                    break;
                }
            }

            // On vérifie que les coordonnées sont libres, qu'il n'y a pas de pièce déjà placée dessus
            if (libre) // "Libre" d'après la vérification précédente
            {
                foreach (POINT point in vecteur)
                {
                    if (!coordonneesSontLibres(new int[2] { point.X, point.Y }, id_joueur))
                    {
                        libre = false;
                        break;
                    }
                }
            }

            return libre;
        }

        public void toucherPieceAuxCoordonnees(int[] coords, int id_joueur, int id_piece_de_jeu)
        {
            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            // On touche la pièce aux coordonnées
            for (int i = 0; i < grille.PIECES_DE_JEU.Count; i++)
            {
                PIECE_DE_JEU piece = grille.PIECES_DE_JEU[i];
                if (piece.ID == id_piece_de_jeu)
                {
                    for (int j = 0; j < piece.VECTEUR.Count; j++)
                    {
                        POINT point = piece.VECTEUR[j];
                        if (point.X == coords[0] && point.Y == coords[1])
                        {
                            point.TOUCHE = true;
                        }
                    }

                    // On met à jour la pièce dans la grille
                    grille.PIECES_DE_JEU[i] = piece;
                }
            }

            // Mise à jour de la grille
            if (id_joueur == 1)
            {
                _GRILLE_JOUEUR_A = new GRILLE(grille);
            }
            else
            {
                _GRILLE_JOUEUR_B = new GRILLE(grille);
            }
        }

        public void placerPieceAuxCoordonnees(int[] coords, int id_joueur, int id_piece_de_jeu, int id_orientation)
        {
            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;

            int taille = 0;
            // On récupère la taille de la pièce
            for (int i = 0; i < grille.PIECES_DE_JEU.Count; i++)
            {
                PIECE_DE_JEU piece = grille.PIECES_DE_JEU[i];
                if (piece.ID == id_piece_de_jeu)
                {
                    taille = piece.BATEAU.TAILLE;
                    break;
                }
            }

            List<POINT> vecteur = new List<POINT>();
            switch (id_orientation)
            {
                case 0: // Vertical
                    for (int i = 0; i < taille; i++)
                    {
                        vecteur.Add(new POINT(coords[0], coords[1] + i)); // Vertical vers le haut
                    }
                    break;
                default: // Horizontal
                    for (int i = 0; i < taille; i++)
                    {
                        vecteur.Add(new POINT(coords[0] + i, coords[1])); // Horizontal vers la droite
                    }
                    break;
            }

            // On place la pièce aux coordonnées
            for (int i = 0; i < grille.PIECES_DE_JEU.Count; i++)
            {
                PIECE_DE_JEU piece = grille.PIECES_DE_JEU[i];
                if (piece.ID == id_piece_de_jeu)
                {
                    if (pieceDejaPlacee(id_joueur, id_piece_de_jeu))
                    {

                        // on place de l'eau : 0 dans la grille aux coordonnées de la pièce
                        foreach (POINT point in piece.VECTEUR)
                        {
                            grille.POSITONS_IDS[point.X, point.Y] = 0;
                        }

                    }

                    piece.VECTEUR = vecteur;
                }
            }

            // On met à jour la grille
            foreach (POINT point in vecteur)
            {
                grille.POSITONS_IDS[point.X, point.Y] = id_piece_de_jeu;
            }

            // On met à jour les pièces de jeu
            if (id_joueur == 1)
            {
                _GRILLE_JOUEUR_A = new GRILLE(grille);
            }
            else
            {
                _GRILLE_JOUEUR_B = new GRILLE(grille);
            }

        }

        public int[,] traduireGrilleCachee(int id_joueur)
        {
            GRILLE G = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;
            int[,] grille = G.POSITONS_IDS;

            // Parcourir chaque case de la grille
            for (int i = 0; i < G.LARGEUR; i++)
            {
                for (int j = 0; j < G.HAUTEUR; j++)
                {
                    bool estTouche = false;

                    // Vérifier si la case correspond à une pièce touchée
                    foreach (PIECE_DE_JEU piece in G.PIECES_DE_JEU)
                    {
                        if (piece.ID == grille[i, j])
                        {
                            foreach (POINT point in piece.VECTEUR)
                            {
                                if (point.X == i && point.Y == j && point.TOUCHE)
                                {
                                    estTouche = true;
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    // Affecter les valeurs en fonction de l'état de la case
                    if (estTouche)
                    {
                        grille[i, j] = -1;
                    }
                    else
                    {
                        grille[i, j] = 0;
                    }
                }
            }

            return grille;
        }



        public int[,] traduireGrille(int id_joueur)
        {
            GRILLE G = (id_joueur == 1) ? _GRILLE_JOUEUR_A: _GRILLE_JOUEUR_B;
            int[,] grille = G.POSITONS_IDS;

            // Pour chaque case de la grille si la pièce correspondante est touchée on fait fois -1 
            for (int i = 0; i < G.LARGEUR; i++)
            {
                for (int j = 0; j < G.HAUTEUR; j++)
                {
                    if (grille[i, j] != 0)
                    {
                        foreach (PIECE_DE_JEU piece in G.PIECES_DE_JEU)
                        {
                            if (piece.ID == grille[i, j])
                            {
                                foreach (POINT point in piece.VECTEUR)
                                {
                                    if (point.X == i && point.Y == j)
                                    {
                                        if (point.TOUCHE)
                                        {
                                            grille[i, j] *= -1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return grille;
        }

        public bool partieEstGagnee(int id_joueur)
        {
            bool gagne = true;
            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;
            // On vérifie si toutes les pièces sont touchées
            foreach (PIECE_DE_JEU piece in grille.PIECES_DE_JEU)
            {
                foreach (POINT point in piece.VECTEUR)
                {
                    if (point.TOUCHE == false)
                    {
                        gagne = false;
                    }
                }
            }
            return gagne;
        }

        public int[,] envoiListeBateau(int id_joueur)
        {
            GRILLE grille = (id_joueur == 1) ? _GRILLE_JOUEUR_A : _GRILLE_JOUEUR_B;
            int[,] listeBateau = new int[grille.PIECES_DE_JEU.Count, 3];
            int i = 0;
            foreach (PIECE_DE_JEU piece in grille.PIECES_DE_JEU)
            {
                listeBateau[i, 0] = piece.ID;
                listeBateau[i, 1] = piece.BATEAU.TAILLE;
                listeBateau[i, 2] = (pieceDejaPlacee(id_joueur, piece.ID) == true) ? 1 : 0;
                i++;
            }
            return listeBateau;
        }

        public List<int[,]> traiterReponse(int[] reponse_affichage)
        {
            // Réponse reçu dans reponse affichage : id_init | x | y | id_joueur | id_piece_de_jeu | id_orientation
            // Réponse à envoyer dans reponse logique : grille | partieGagnee matrice de 1 ou 0 

            List<int[,]> reponse_logique = new List<int[,]>();
            int[,] partieGagnee = new int[1, 1] { { 0 } };

            // Retraduction de la réponse affichage
            int id_init, x, y, id_joueur, id_piece_de_jeu, id_orientation;

            id_init = (reponse_affichage.Length > 0) ? reponse_affichage[0] : -1;
            x = (reponse_affichage.Length > 1) ? reponse_affichage[1] : -1;
            y = (reponse_affichage.Length > 2) ? reponse_affichage[2] : -1;
            id_joueur = (reponse_affichage.Length > 3) ? reponse_affichage[3] : -1;
            id_piece_de_jeu = (reponse_affichage.Length > 4) ? reponse_affichage[4] : -1;
            id_orientation = (reponse_affichage.Length > 5) ? reponse_affichage[5] : -1;


            if(id_init != 0) // Partie jeu
            {
                // On inverse id_joueur pour avoir le joueur adverse (1 -> 2 et 2 -> 1) : Le joueur 1 joue sur la grille du joueur 2
                id_joueur = (id_joueur == 1) ? 2 : 1;

                // Réponse par défaut
                reponse_logique.Add(traduireGrilleCachee(id_joueur));
                partieGagnee[0, 0] = 0;
                reponse_logique.Add(partieGagnee);

                if (coordonneesDansLaGrille(new int[2] {x, y}, id_joueur) == true)
                {
                    if (coordonneesDejaTouchees(new int[2] {x, y}, id_joueur, id_piece_de_jeu) == false)
                    {
                        toucherPieceAuxCoordonnees(new int[2] { x, y }, id_joueur, id_piece_de_jeu);
                        // remplacer la réponse par défaut par la grille mise à jour
                        reponse_logique[0] = traduireGrilleCachee(id_joueur);

                        if (partieEstGagnee(id_joueur))
                        {
                            partieGagnee[0, 0] = 1;
                            reponse_logique[1] = partieGagnee; // Partie gagnée
                        }
                    }    
                }
            }
            else // Partie positionnement des pièces de jeu
            {
                if(id_joueur == -1) // On initialise la grille commune aux deux joueurs
                {
                    reponse_logique.Add(traduireGrille(1)); // Grille du joueur 1 : les 2 grilles étant initialement identiques
                    reponse_logique.Add(envoiListeBateau(1));
                }  
                else
                {
                    if(x == -1 && y == -1) // Code -1 -1 on veuxjuste la liste des bateaux
                    {
                        reponse_logique.Add(traduireGrille(id_joueur));
                        reponse_logique.Add(envoiListeBateau(id_joueur));
                    }
                    else // On place la pièce de jeu 
                    {
                        if (emplacementLibrePourPlacer(new int[2] { x, y }, id_joueur, id_piece_de_jeu, id_orientation))
                        {

                            placerPieceAuxCoordonnees(new int[2] { x, y }, id_joueur, id_piece_de_jeu, id_orientation);

                            reponse_logique.Add(traduireGrille(id_joueur));
                            reponse_logique.Add(envoiListeBateau(id_joueur));
                        } 
                        else
                        {
                            reponse_logique.Add(traduireGrille(id_joueur));
                            reponse_logique.Add(envoiListeBateau(id_joueur));
                        }
                    }
                }
            }

            return reponse_logique;
        }
    }
}
