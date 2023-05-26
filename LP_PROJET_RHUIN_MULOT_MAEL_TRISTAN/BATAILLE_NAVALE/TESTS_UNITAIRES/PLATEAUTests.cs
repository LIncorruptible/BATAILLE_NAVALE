using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIBLIOTHEQUE_LOGIQUE_JEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIBLIOTHEQUE_INTERACTIONS_API;
using Newtonsoft.Json.Linq;

namespace BIBLIOTHEQUE_LOGIQUE_JEU.Tests
{
    [TestClass()]
    public class PLATEAUTests
    {
        [TestMethod()]
        public void PLATEAU_TEST_CONSTRUCTEUR_0()
        {
            // Arrange
            GRILLE grilleJoueurA = new GRILLE();
            GRILLE grilleJoueurB = new GRILLE();
            int[,] grilleEnemiDeA = new int[0, 0];
            int[,] grilleEnemiDeB = new int[0, 0];
            bool win = false;

            // Act
            PLATEAU plateau = new PLATEAU(grilleJoueurA, grilleJoueurB, grilleEnemiDeA, grilleEnemiDeB, win);

            // Assert
            Assert.AreEqual(grilleJoueurA, plateau.GRILLE_JOUEUR_A);
            Assert.AreEqual(grilleJoueurB, plateau.GRILLE_JOUEUR_B);
            Assert.AreEqual(grilleEnemiDeA, plateau.GRILLE_ENEMI_DE_A);
            Assert.AreEqual(grilleEnemiDeB, plateau.GRILLE_ENEMI_DE_B);
            Assert.AreEqual(win, plateau.WIN);
        }

        [TestMethod()]
        public void PLATEAU_TEST_CONSTRUCTEUR_1()
        {
            // Act
            PLATEAU plateau = new PLATEAU();

            // Assert
            Assert.IsNotNull(plateau.GRILLE_JOUEUR_A);
            Assert.IsNotNull(plateau.GRILLE_JOUEUR_B);
            Assert.IsNotNull(plateau.GRILLE_ENEMI_DE_A);
            Assert.IsNotNull(plateau.GRILLE_ENEMI_DE_B);
            Assert.IsFalse(plateau.WIN);
        }

        [TestMethod()]
        public void PLATEAU_TEST_CONSTRUCTEUR_2()
        {
            // Arrange
            GRILLE grilleJoueurA = new GRILLE();
            GRILLE grilleJoueurB = new GRILLE();
            int[,] grilleEnemiDeA = new int[0, 0];
            int[,] grilleEnemiDeB = new int[0, 0];
            bool win = false;
            PLATEAU originalPlateau = new PLATEAU(grilleJoueurA, grilleJoueurB, grilleEnemiDeA, grilleEnemiDeB, win);

            // Act
            PLATEAU plateau = new PLATEAU(originalPlateau);

            // Assert
            Assert.AreEqual(originalPlateau.GRILLE_JOUEUR_A, plateau.GRILLE_JOUEUR_A);
            Assert.AreEqual(originalPlateau.GRILLE_JOUEUR_B, plateau.GRILLE_JOUEUR_B);
            Assert.AreEqual(originalPlateau.GRILLE_ENEMI_DE_A, plateau.GRILLE_ENEMI_DE_A);
            Assert.AreEqual(originalPlateau.GRILLE_ENEMI_DE_B, plateau.GRILLE_ENEMI_DE_B);
            Assert.AreEqual(originalPlateau.WIN, plateau.WIN);
        }

        [TestMethod()]
        public void PLATEAU_TEST_CONSTRUCTEUR_3()
        {
            // Arrange
            string expectedURL = "https://api-lprgi.natono.biz/api/GetConfig";
            string expectedKey = "lprgi_api_key_2023";

            API api = new API(expectedURL, expectedKey);
            
            string json = api.RecupererData().Result;

            // Simuler les méthodes de récupération des données
            int[] taille = new int[] { 10, 10 }; // Exemple de taille de grille
            List<BATEAU> bateaux = new List<BATEAU>(); // Exemple de liste de bateaux

            // Création de la grille simulée pour GRILLE_JOUEUR_A et GRILLE_JOUEUR_B
            GRILLE grilleJoueurA = new GRILLE(taille[0], taille[1], bateaux);
            GRILLE grilleJoueurB = new GRILLE(grilleJoueurA);

            // Création de la grille simulée pour GRILLE_ENEMI_DE_A et GRILLE_ENEMI_DE_B
            int[,] grilleEnemiDeA = new int[taille[0], taille[1]];
            int[,] grilleEnemiDeB = new int[taille[0], taille[1]];
            for (int i = 0; i < grilleEnemiDeA.GetLength(0); i++)
            {
                for (int j = 0; j < grilleEnemiDeA.GetLength(1); j++)
                {
                    grilleEnemiDeA[i, j] = -1;
                    grilleEnemiDeB[i, j] = -1;
                }
            }

            bool win = false;

            // Act
            PLATEAU plateau = new PLATEAU(json);

            // Assert
            Assert.AreEqual(grilleJoueurA.HAUTEUR, plateau.GRILLE_JOUEUR_A.HAUTEUR);
            Assert.AreEqual(grilleJoueurA.LARGEUR, plateau.GRILLE_JOUEUR_A.LARGEUR);
            // Comparer les autres attributs de GRILLE_JOUEUR_A

            Assert.AreEqual(grilleJoueurB.HAUTEUR, plateau.GRILLE_JOUEUR_B.HAUTEUR);
            Assert.AreEqual(grilleJoueurB.LARGEUR, plateau.GRILLE_JOUEUR_B.LARGEUR);

            Assert.AreEqual(win, plateau.WIN);
        }

        [TestMethod()]
        public void recupererListeBateauxTest()
        {

        }

        [TestMethod()]
        public void recupererTailleGrilleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void coordonneesDansLaGrilleTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées et l'ID du joueur pour tester
            int[] coords = new int[] { 5, 5 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester

            // Act
            bool result = plateau.coordonneesDansLaGrille(coords, idJoueur);

            // Assert
            Assert.IsTrue(result); // Vérifiez si les coordonnées sont valides dans la grille du joueur 1
        }

        [TestMethod()]
        public void coordonneesSontLibresTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées et l'ID du joueur pour tester
            int[] coords = new int[] { 5, 5 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester

            // Act
            bool result = plateau.coordonneesSontLibres(coords, idJoueur);

            // Assert
            Assert.IsTrue(result); // Vérifiez si les coordonnées sont libres dans la grille du joueur 1
        }

        [TestMethod()]
        public void coordonneesDejaToucheesTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées, l'ID du joueur et l'ID de la pièce de jeu pour tester
            int[] coords = new int[] { 5, 5 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester
            int idPieceDeJeu = 1; // ID de la pièce de jeu à tester

            // Act
            bool result = plateau.coordonneesDejaTouchees(coords, idJoueur, idPieceDeJeu);

            // Assert
            Assert.IsFalse(result); // Vérifiez si les coordonnées ne sont pas déjà touchées pour la pièce de jeu spécifiée
        }

        [TestMethod()]
        public void pieceDejaPlaceeTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez l'ID du joueur et l'ID de la pièce de jeu pour tester
            int idJoueur = 1; // ID du joueur à tester
            int idPieceDeJeu = 1; // ID de la pièce de jeu à tester

            // Act
            bool result = plateau.pieceDejaPlacee(idJoueur, idPieceDeJeu);

            // Assert
            Assert.IsFalse(result); // Vérifiez si la pièce n'est pas déjà placée pour le joueur spécifié
        }

        [TestMethod()]
        public void emplacementLibrePourPlacerTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées, l'ID du joueur, l'ID de la pièce de jeu et l'ID de l'orientation pour tester
            int[] coords = { 0, 0 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester
            int idPieceDeJeu = 1; // ID de la pièce de jeu à tester
            int idOrientation = 0; // ID de l'orientation à tester

            // Act
            bool result = plateau.emplacementLibrePourPlacer(coords, idJoueur, idPieceDeJeu, idOrientation);

            // Assert
            Assert.IsTrue(result); // Vérifiez si l'emplacement est libre pour placer la pièce avec les coordonnées, l'ID du joueur, l'ID de la pièce de jeu et l'ID de l'orientation spécifiés
        }

        [TestMethod()]
        public void toucherPieceAuxCoordonneesTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées, l'ID du joueur et l'ID de la pièce de jeu pour tester
            int[] coords = { 0, 0 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester
            int idPieceDeJeu = 1; // ID de la pièce de jeu à tester

            // Act
            plateau.toucherPieceAuxCoordonnees(coords, idJoueur, idPieceDeJeu);

            // Assert
            // Vérifiez si la pièce aux coordonnées spécifiées a été touchée en parcourant les grilles des joueurs
            bool pieceTouchee = false;

            if (idJoueur == 1)
            {
                foreach (PIECE_DE_JEU piece in plateau.GRILLE_JOUEUR_A.PIECES_DE_JEU)
                {
                    if (piece.ID == idPieceDeJeu)
                    {
                        foreach (POINT point in piece.VECTEUR)
                        {
                            if (point.X == coords[0] && point.Y == coords[1] && point.TOUCHE)
                            {
                                pieceTouchee = true;
                                break;
                            }
                        }
                    }
                }
            }
            else if (idJoueur == 2)
            {
                foreach (PIECE_DE_JEU piece in plateau.GRILLE_JOUEUR_B.PIECES_DE_JEU)
                {
                    if (piece.ID == idPieceDeJeu)
                    {
                        foreach (POINT point in piece.VECTEUR)
                        {
                            if (point.X == coords[0] && point.Y == coords[1] && point.TOUCHE)
                            {
                                pieceTouchee = true;
                                break;
                            }
                        }
                    }
                }
            }

            Assert.IsTrue(pieceTouchee); // Vérifiez si la pièce aux coordonnées spécifiées a été touchée
        }

        [TestMethod()]
        public void placerPieceAuxCoordonneesTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez les coordonnées, l'ID du joueur, l'ID de la pièce de jeu et l'ID de l'orientation pour tester
            int[] coords = { 0, 0 }; // Coordonnées à tester
            int idJoueur = 1; // ID du joueur à tester
            int idPieceDeJeu = 1; // ID de la pièce de jeu à tester
            int idOrientation = 0; // ID de l'orientation à tester (0 pour vertical, 1 pour horizontal)

            // Act
            plateau.placerPieceAuxCoordonnees(coords, idJoueur, idPieceDeJeu, idOrientation);

            // Assert
            // Vérifiez si la pièce a été placée aux coordonnées spécifiées en parcourant les grilles des joueurs
            bool piecePlacee = false;

            if (idJoueur == 1)
            {
                foreach (PIECE_DE_JEU piece in plateau.GRILLE_JOUEUR_A.PIECES_DE_JEU)
                {
                    if (piece.ID == idPieceDeJeu)
                    {
                        bool coordonneesCorrespondent = true;
                        List<POINT> vecteur = piece.VECTEUR;

                        if (idOrientation == 0) // Vertical
                        {
                            for (int i = 0; i < vecteur.Count; i++)
                            {
                                if (vecteur[i].X != coords[0] || vecteur[i].Y != coords[1] + i)
                                {
                                    coordonneesCorrespondent = false;
                                    break;
                                }
                            }
                        }
                        else if (idOrientation == 1) // Horizontal
                        {
                            for (int i = 0; i < vecteur.Count; i++)
                            {
                                if (vecteur[i].X != coords[0] + i || vecteur[i].Y != coords[1])
                                {
                                    coordonneesCorrespondent = false;
                                    break;
                                }
                            }
                        }

                        if (coordonneesCorrespondent)
                        {
                            piecePlacee = true;
                            break;
                        }
                    }
                }
            }
            else if (idJoueur == 2)
            {
                foreach (PIECE_DE_JEU piece in plateau.GRILLE_JOUEUR_B.PIECES_DE_JEU)
                {
                    if (piece.ID == idPieceDeJeu)
                    {
                        bool coordonneesCorrespondent = true;
                        List<POINT> vecteur = piece.VECTEUR;

                        if (idOrientation == 0) // Vertical
                        {
                            for (int i = 0; i < vecteur.Count; i++)
                            {
                                if (vecteur[i].X != coords[0] || vecteur[i].Y != coords[1] + i)
                                {
                                    coordonneesCorrespondent = false;
                                    break;
                                }
                            }
                        }
                        else if (idOrientation == 1) // Horizontal
                        {
                            for (int i = 0; i < vecteur.Count; i++)
                            {
                                if (vecteur[i].X != coords[0] + i || vecteur[i].Y != coords[1])
                                {
                                    coordonneesCorrespondent = false;
                                    break;
                                }
                            }
                        }

                        if (coordonneesCorrespondent)
                        {
                            piecePlacee = true;
                            break;
                        }
                    }
                }
            }

            Assert.IsTrue(piecePlacee); // Vérifiez si la pièce a été placée aux coordonnées spécifiées
        }

        [TestMethod()]
        public void traduireGrilleTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez l'ID du joueur pour tester
            int idJoueur = 1; // ID du joueur à tester

            // Act
            int[,] grilleTraduite = plateau.traduireGrille(idJoueur);

            // Assert
            // Vérifiez si la traduction de la grille est correcte en comparant les valeurs de la grille traduite avec les valeurs attendues
            GRILLE grilleJoueur = (idJoueur == 1) ? plateau.GRILLE_JOUEUR_B : plateau.GRILLE_JOUEUR_A;
            int[,] grilleAttendue = grilleJoueur.POSITONS_IDS;

            for (int i = 0; i < grilleJoueur.LARGEUR; i++)
            {
                for (int j = 0; j < grilleJoueur.HAUTEUR; j++)
                {
                    if (grilleAttendue[i, j] != 0)
                    {
                        foreach (PIECE_DE_JEU piece in grilleJoueur.PIECES_DE_JEU)
                        {
                            if (piece.ID == grilleAttendue[i, j])
                            {
                                foreach (POINT point in piece.VECTEUR)
                                {
                                    if (point.X == i && point.Y == j)
                                    {
                                        if (point.TOUCHE)
                                        {
                                            grilleAttendue[i, j] *= -1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            CollectionAssert.AreEqual(grilleAttendue, grilleTraduite); // Vérifiez si les grilles sont équivalentes
        }

        [TestMethod()]
        public void partieEstGagneeTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez l'ID du joueur pour tester
            int idJoueur = 1; // ID du joueur à tester

            // Modifiez la grille du joueur pour simuler une situation gagnante
            GRILLE grilleJoueur = (idJoueur == 1) ? plateau.GRILLE_JOUEUR_B : plateau.GRILLE_JOUEUR_A;
            foreach (PIECE_DE_JEU piece in grilleJoueur.PIECES_DE_JEU)
            {
                foreach (POINT point in piece.VECTEUR)
                {
                    point.TOUCHE = true; // Marquez toutes les pièces comme touchées
                }
            }

            // Act
            bool resultat = plateau.partieEstGagnee(idJoueur);

            // Assert
            Assert.IsTrue(resultat); // Vérifiez si la partie est gagnée pour le joueur donné
        }

        [TestMethod()]
        public void traiterReponseTest()
        {
            // Arrange
            PLATEAU plateau = new PLATEAU(); // Créez une instance de la classe PLATEAU avec les valeurs appropriées

            // Définissez une réponse d'affichage à traiter
            int[] reponseAffichage = new int[] { 1, 0, 0, 1, 1, 0 }; // Exemple de réponse d'affichage à traiter

            // Act
            List<int[,]> reponseLogique = plateau.traiterReponse(reponseAffichage);

            // Assert à définir
        }
    }
}