using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE
{
    public static class DISPLAY
    {
        public static void AfficherPlateau(int[,] plateau)
        {
            int largeur = plateau.GetLength(0);
            int hauteur = plateau.GetLength(1);

            // Affichage du cadre supérieur
            Console.WriteLine("+" + new string('-', largeur * 3 + 2) + "+");

            for (int y = 0; y < hauteur; y++)
            {
                Console.Write("|");

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
            Console.WriteLine("+" + new string('-', largeur * 3 + 2) + "+");
        }
    }
}
