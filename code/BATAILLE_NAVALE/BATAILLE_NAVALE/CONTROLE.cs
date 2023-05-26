using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE
{
    public static class CONTROLE
    {
        public static bool memeGrille(int[,] grille_A, int[,] grille_B)
        {
            // On vérifie que les deux grilles sont les mêmes
            if (grille_A.GetLength(0) != grille_B.GetLength(0) || grille_A.GetLength(1) != grille_B.GetLength(1))
            {
                // Les tableaux n'ont pas les mêmes dimensions, ils ne peuvent pas être identiques
                return false;
            }

            int largeur = grille_A.GetLength(0);
            int hauteur = grille_A.GetLength(1);

            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (grille_A[i, j] != grille_B[i, j])
                    {
                        // Les valeurs des éléments sont différentes, les tableaux ne sont pas identiques
                        return false;
                    }
                }
            }

            // Toutes les valeurs des éléments sont identiques, les tableaux sont identiques
            return true;
        }

        public static bool piecesToutesPlacees(int[,] liste_pieces)
        {
            for (int i = 0; i < liste_pieces.GetLength(0); i++)
            {
                if (liste_pieces[i, 2] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool positionPiece(string position, int largeur, int hauteur)
        {
            if (position.Length == 2)
            {
                char lettre_depart = 'A';
                char lettre_fin = (char)('A' + largeur - 1);

                int chiffre_depart = 0;
                int chiffre_fin = hauteur;

                char lettre = char.ToUpper(position[0]);
                int chiffre;

                if (int.TryParse(position[1].ToString(), out chiffre))
                {
                    if (lettre >= lettre_depart && lettre <= lettre_fin && chiffre >= chiffre_depart && chiffre <= chiffre_fin)
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public static bool piecesToutesCoulees(int[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    if (matrice[i, j] != 1) return false;
                }
            }
            return true;
        }

    }
}
