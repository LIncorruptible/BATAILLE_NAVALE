using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using BIBLIOTHEQUE_INTERACTIONS_API;
using BIBLIOTHEQUE_LOGIQUE_JEU;


namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE
{
    internal class CLASSE_PRINCIPALE
    {
        public static void afficherTourPositionnementPiece(int joueurEnCours, int[,] grille_joueur, List<int[,]> reponse_logique, PLATEAU plateau)
        {
            int[,]
                grille = reponse_logique[0],
                liste_pieces = reponse_logique[1];

            char[] choix = { 'o', 'O' };

            while (choix.Contains('o') || choix.Contains('O') || CONTROLE.piecesToutesPlacees(liste_pieces))
            {
                Console.Clear();
                DISPLAY.AffichageTitre("Positionnement des pièces du joueur " + joueurEnCours);

                DISPLAY.AfficherPlateau(grille_joueur);

                Console.Write("Voulez-vous positionner une pièce ? (O/N) : ");
                choix = Console.ReadLine().ToCharArray();
                if (!choix.Contains('o') && !choix.Contains('O')) break;

                // Afficher la liste des pièces
                DISPLAY.afficherListePieces(liste_pieces);

                int[] reponse_affichage = DISPLAY.reponseAffichage(true, joueurEnCours, reponse_logique);

                reponse_logique = plateau.traiterReponse(reponse_affichage);

                grille_joueur = reponse_logique[0];
                liste_pieces = reponse_logique[1];

                if (CONTROLE.piecesToutesPlacees(liste_pieces))
                {
                    Console.WriteLine("Toutes les pièces ont été placées"); break;
                }
                else
                {
                    Console.WriteLine("Il reste des pièces à placer");
                }

                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
        }

        public static bool afficherTourJeu(int joueurEnCours, int[,] grille_joueur, List<int[,]> reponse_logique, PLATEAU plateau)
        {
            bool partieGagnee = false;

            Console.Clear();
            DISPLAY.AffichageTitre("Tour du joueur " + joueurEnCours);

            DISPLAY.AfficherPlateau(grille_joueur);

            int[] reponse_affichage = DISPLAY.reponseAffichage(false, joueurEnCours, reponse_logique);

            reponse_logique = plateau.traiterReponse(reponse_affichage);

            grille_joueur = reponse_logique[0];
            
            // Si reponse_logique[1] ne contient que des 1 alors le joueur a gagné
            if (CONTROLE.piecesToutesCoulees(reponse_logique[1]))
            {
                Console.WriteLine("Le joueur " + joueurEnCours + " a gagné !");
                partieGagnee = true;
            }

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();

            return partieGagnee;
        }

        static void Main(string[] args)
        {
            // Affichage de départ
            DISPLAY.AffichageTitre("BATAILLE NAVALE");

            Console.WriteLine("\n===== Lancement =====\n");
            
            // Récupération des données de l'API
            string url = "https://api-lprgi.natono.biz/api/GetConfig";
            string key = "lprgi_api_key_2023";

            API api = new API(url, key);
            string json = api.RecupererData().Result;

            // Initialisation du plateau
            Console.Write("Création du plateau...");
            PLATEAU plateau = new PLATEAU(json);
            Console.Write("[Terminé]\n");

            Console.WriteLine("\n====== Terminé ======\n");

            // Attente de la saisie d'une touche pour lancer la partie
            Console.WriteLine("Appuyez sur une touche pour lancer la partie...");
            Console.ReadKey();

            // Effacement de l'écran
            Console.Clear();

            // Positionnement des pièces
            DISPLAY.AffichageTitre("Qui commence ?");

            // Sélection entre 1 et 2 
            Console.Write("Sélectionnez le joueur qui va commencer (1 ou 2) : ");
            string reponse = Console.ReadLine();
            int joueurEnCours = (string.IsNullOrEmpty(reponse)) ? 0 : int.Parse(reponse);

            while (joueurEnCours != 1 && joueurEnCours != 2)
            {
                Console.Write("1 ou 2 est attendu : ");
                reponse = Console.ReadLine();
                joueurEnCours = (string.IsNullOrEmpty(reponse)) ? 0 : int.Parse(reponse);
            }

            // Effacement de l'écran
            Console.Clear();

            DISPLAY.AffichageTitre("EXPLICATIONS");

            // Informations
            Console.WriteLine("La première phase de la partie va commencer. Chaque joueur va devoir positionner ses pièces sur le plateau.\nPour cela, il devra saisir le numéro de la pièce puis ses coordonnées LETTRE + CHIFFRE (ex: A5) de la pièce à placer,\nainsi que son orientation (H pour horizontal, V pour vertical).");

            // Attente de la saisie d'une touche pour lancer la partie
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();

            // Mémoire des grilles des joueurs
            int[,]
                grille_joueur_1,
                grille_joueur_2;

            List<int[,]> reponse_logique = plateau.traiterReponse(new int[] { 0, -1, -1, -1, -1 });

            int[,]
                grille = reponse_logique[0],
                liste_pieces = reponse_logique[1];

            grille_joueur_1 = (int[,])grille.Clone();
            grille_joueur_2 = (int[,])grille.Clone();


            // Position des pièces par joueur

            // joueur 1/2
            afficherTourPositionnementPiece(joueurEnCours, (joueurEnCours == 1) ? grille_joueur_1 : grille_joueur_2, reponse_logique, plateau);

            // Changement de joueur
            joueurEnCours = DISPLAY.ChangementDeJoueur(joueurEnCours);

            // joueur 2/1
            afficherTourPositionnementPiece(joueurEnCours, (joueurEnCours == 1) ? grille_joueur_1 : grille_joueur_2, reponse_logique, plateau);

            // Changement de joueur
            joueurEnCours = DISPLAY.ChangementDeJoueur(joueurEnCours);

            Console.Clear();
            DISPLAY.AffichageTitre("EXPLICATIONS");

            // Informations
            Console.WriteLine("La deuxième phase de la partie va commencer. Chaque joueur va devoir tirer sur le plateau de l'autre joueur.\nPour cela, il devra saisir les coordonnées LETTRE + CHIFFRE (ex: A5) de la case à viser.");

            // Attente de la saisie d'une touche pour lancer la partie
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();

            bool PartieGagnee = false;

            int[,] 
                grilleAdverse_1 = (int[,])plateau.traiterReponse(new int[] { 1, -1, -1, -1, -1 })[0].Clone(),
                grilleAdverse_2 = (int[,])plateau.traiterReponse(new int[] { 1, -1, -1, -1, -1 })[0].Clone();

            Console.ReadKey();

            while (PartieGagnee == false)
            {
                // Joueur 1 sur le joueur 2 et inversement
                if (afficherTourJeu(joueurEnCours, (joueurEnCours == 1) ? grilleAdverse_2 : grilleAdverse_1, reponse_logique, plateau) == true)
                {
                    PartieGagnee = true;
                }

                Console.Clear();
                DISPLAY.AffichageTitre("Tour du joueur " + joueurEnCours);

                DISPLAY.AfficherGrille(grilleAdverse_1);
                Console.WriteLine();
                DISPLAY.AfficherGrille(grilleAdverse_2);

                Console.ReadKey();

                DISPLAY.AfficherPlateau((joueurEnCours == 1) ? grilleAdverse_2 : grilleAdverse_1);

                // Attente de la saisie d'une touche pour lancer la partie
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey();

                joueurEnCours = DISPLAY.ChangementDeJoueur(joueurEnCours);

            }
        }
    }
}
