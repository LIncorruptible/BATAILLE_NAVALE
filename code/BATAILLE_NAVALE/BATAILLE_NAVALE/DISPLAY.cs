using BIBLIOTHEQUE_LOGIQUE_JEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE
{
    public static class DISPLAY
    {
        public static void AffichageTitre(string titre)
        {
            // Affichage du cadre supérieur
            Console.WriteLine("#" + new string('=', titre.Length + 4) + "#");

            // Affichage du texte centré
            Console.WriteLine("|  " + titre + "  |");

            // Affichage du cadre inférieur
            Console.WriteLine("#" + new string('=', titre.Length + 4) + "#");
        }

        public static void AfficherGrille(int[,] plateau)
        {
            for(int i = 0; i < plateau.GetLength(0); i++)
            {
                for(int j = 0; j < plateau.GetLength(1); j++)
                {
                    Console.Write(plateau[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void AfficherPlateau(int[,] plateau)
        {
            int largeur = plateau.GetLength(0);
            int hauteur = plateau.GetLength(1);

            // Affichage des lettres pour les colonnes
            Console.Write("     ");
            for (int x = 0; x < largeur; x++)
            {
                Console.Write((char)('A' + x) + "  ");
            }
            Console.WriteLine();

            // Affichage du cadre supérieur
            Console.WriteLine("  +" + new string('-', largeur * 3 + 2) + "+");

            for (int y = 0; y < hauteur; y++)
            {
                Console.Write(y + " |");

                for (int x = 0; x < largeur; x++)
                {
                    int cellule = plateau[x, y];
                    string couleur;
                    string caractere;

                    // Détermination de la couleur et du caractère à afficher
                    if (cellule == 0)
                    {
                        couleur = "Blue";
                        caractere = "~";
                    }
                    else if (cellule > 0)
                    {
                        couleur = "White";
                        caractere = "#";
                    }
                    else
                    {
                        couleur = "Red";
                        caractere = "#";
                    }

                    // Affichage de la cellule
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), couleur);
                    Console.Write(caractere.PadLeft(3));
                    Console.ResetColor();
                }

                Console.WriteLine("|".PadLeft(3)); // Nouvelle ligne pour passer à la ligne suivante
            }

            // Affichage du cadre inférieur
            Console.WriteLine("  +" + new string('-', largeur * 3 + 2) + "+");
        }

        public static void afficherListePieces(int[,] liste_pieces)
        {
            Console.WriteLine("Liste des pièces :");
            for (int i = 0; i < liste_pieces.GetLength(0); i++)
            {
                string ligne = "\t- Pièce n°" + (i+1) + " : ";
                for (int j = 0; j < liste_pieces[i, 1]; j++) ligne += "#";
                ligne += (liste_pieces[i, 2] == 1) ? " (placée)" : " (non placée)";

                Console.WriteLine(ligne);
            }
        }

        public static void AfficherTerminerTour()
        {
            // Attente de la saisie d'une touche
            Console.WriteLine("\nAppuyez sur une touche pour terminer votre tour...");
            Console.ReadKey();
        }

        public static void AfficherTourDuJoueur(int joueur)
        {
            Console.Clear();
            AffichageTitre("TOUR DU JOUEUR " + joueur);

            AfficherTerminerTour();
        }

        public static int ChangementDeJoueur(int joueur)
        {
            Console.Clear();
            return joueur == 1 ? 2 : 1;
        }

        public static int[] reponseAffichage(bool init, int joueur, List<int[,]> reponse_logique)
        {
            // Réponse reçu dans reponse affichage : id_init | x | y | id_joueur | id_piece_de_jeu | id_orientation
            int[] reponse_affichage = new int[6];

            int[,]
                grille = reponse_logique[0],
                liste_pieces = reponse_logique[1];

            string reponse;

            int
                id_piece = -1,
                x = -1,
                y = -1,
                orientation = 1;

            char[] choix = { 'o', 'O' };

            if (init)
            {
                // Saisie de la pièce
                int nb_total_pieces = liste_pieces.GetLength(0);
                do
                {
                    Console.Write("Saisissez le numéro de la pièce à placer : ");
                    reponse = Console.ReadLine();

                    if (string.IsNullOrEmpty(reponse))
                    {
                        id_piece = -1;
                    } 
                    else
                    {
                        try
                        {
                            id_piece = int.Parse(reponse);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Le numéro de la pièce doit être un nombre entier");
                            id_piece = -1;
                        }
                    }

                    if (id_piece < 1 || id_piece > nb_total_pieces)
                    {
                        Console.WriteLine("Le numéro de la pièce doit être compris entre 1 et " + nb_total_pieces);
                    }
                } while (id_piece < 1 || id_piece > nb_total_pieces);

                int
                    largeur = grille.GetLength(0),
                    hauteur = grille.GetLength(1);

                // Saisie des coordonnées
                int[] coords = new int[2];
                do
                {
                    Console.Write("Saisissez les coordonnées de la pièce (ex: A5) : ");
                    reponse = Console.ReadLine();

                    if (CONTROLE.positionPiece(reponse, largeur, hauteur) == false)
                    {
                        Console.WriteLine("Les coordonnées doivent être comprises entre A0 et J" + grille.GetLength(0));
                    } 
                    else
                    {
                        coords[0] = (string.IsNullOrEmpty(reponse)) ? -1 : reponse[0] - 65;
                        coords[1] = (string.IsNullOrEmpty(reponse)) ? -1 : int.Parse(reponse.Substring(1));
                    }
                } while (CONTROLE.positionPiece(reponse, largeur, hauteur) == false);

                // Saisie de l'orientation
                char[] reponses_attendues = { 'h', 'H', 'v', 'V' };
                char c_reponse = 'q';
                do
                {
                    Console.Write("Saisissez l'orientation de la pièce (H pour horizontal, V pour vertical) : ");
                    c_reponse = Console.ReadLine().ToCharArray()[0];

                    orientation = (c_reponse == 'h' || c_reponse == 'H') ? 1 : 0;

                    if (!reponses_attendues.Contains(c_reponse))
                    {
                        Console.WriteLine("L'orientation doit être H ou V");
                    }
                } while (!reponses_attendues.Contains(c_reponse));

                // Résumé la saisie
                Console.Write("Placer la pièce " + id_piece + " aux coordonnées " + reponse + " avec une orientation " + ((orientation == 1) ? "horizontale" : "verticale") + " ? (O/N) : ");
                choix = Console.ReadLine().ToCharArray();
                if (!choix.Contains('o') && !choix.Contains('O')) return null;

                Console.WriteLine();

                reponse_affichage[0] = 0;
                reponse_affichage[1] = coords[0];
                reponse_affichage[2] = coords[1];
                reponse_affichage[3] = joueur;
                reponse_affichage[4] = id_piece;
                reponse_affichage[5] = orientation;
            } 
            else
            {
                int
                    largeur = grille.GetLength(0),
                    hauteur = grille.GetLength(1);

                // Saisie des coordonnées
                int[] coords = new int[2];
                do
                {
                    Console.Write("Saisissez les coordonnées de la pièce (ex: A5) : ");
                    reponse = Console.ReadLine();

                    if (CONTROLE.positionPiece(reponse, largeur, hauteur) == false)
                    {
                        Console.WriteLine("Les coordonnées doivent être comprises entre A0 et J" + grille.GetLength(0));
                    }
                    else
                    {
                        coords[0] = (string.IsNullOrEmpty(reponse)) ? -1 : reponse[0] - 65;
                        coords[1] = (string.IsNullOrEmpty(reponse)) ? -1 : int.Parse(reponse.Substring(1));
                    }
                } while (CONTROLE.positionPiece(reponse, largeur, hauteur) == false);

                reponse_affichage[0] = 1;
                reponse_affichage[1] = coords[0];
                reponse_affichage[2] = coords[1];
                reponse_affichage[3] = joueur;
                reponse_affichage[4] = -1;
                reponse_affichage[5] = -1;
            }

            return reponse_affichage;
        }

    }
}
