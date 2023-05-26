using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIBLIOTHEQUE_LOGIQUE_JEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_LOGIQUE_JEU.Tests
{
    [TestClass()]
    public class GRILLETests
    {
        [TestMethod()]
        public void GRILLE_TEST_CONSTRUCTEUR_0()
        {
            // Arrange
            int expectedHauteur = 5;
            int expectedLargeur = 10;
            List<BATEAU> expectedBateaux = new List<BATEAU>()
            {
                new BATEAU(3, "Bateau1"),
                new BATEAU(4, "Bateau2"),
                new BATEAU(2, "Bateau3")
            };

            // Act
            GRILLE grille = new GRILLE(expectedHauteur, expectedLargeur, expectedBateaux);

            // Assert
            Assert.AreEqual(expectedHauteur, grille.HAUTEUR);
            Assert.AreEqual(expectedLargeur, grille.LARGEUR);
            Assert.AreEqual(expectedHauteur * expectedLargeur, grille.TAILLE);
            Assert.IsNotNull(grille.DISPONIBILITES);
            Assert.AreEqual(expectedHauteur, grille.DISPONIBILITES.GetLength(0));
            Assert.AreEqual(expectedLargeur, grille.DISPONIBILITES.GetLength(1));
            Assert.IsNotNull(grille.POSITONS_IDS);
            Assert.AreEqual(expectedHauteur, grille.POSITONS_IDS.GetLength(0));
            Assert.AreEqual(expectedLargeur, grille.POSITONS_IDS.GetLength(1));
            Assert.IsNotNull(grille.PIECES_DE_JEU);
            Assert.AreEqual(expectedBateaux.Count, grille.PIECES_DE_JEU.Count);
        }

        [TestMethod()]
        public void GRILLE_TEST_CONSTRUCTEUR_1()
        {
            // Arrange
            int expectedHauteur = 5;
            int expectedLargeur = 10;
            int expectedTaille = expectedHauteur * expectedLargeur;
            bool[,] expectedDisponibilites = new bool[expectedHauteur, expectedLargeur];
            int[,] expectedPositionsIds = new int[expectedHauteur, expectedLargeur];
            List<PIECE_DE_JEU> expectedPiecesDeJeu = new List<PIECE_DE_JEU>()
            {
                new PIECE_DE_JEU(1, new BATEAU(3, "Bateau1"), new List<POINT>()),
                new PIECE_DE_JEU(2, new BATEAU(4, "Bateau2"), new List<POINT>()),
                new PIECE_DE_JEU(3, new BATEAU(2, "Bateau3"), new List<POINT>())
            };

            // Act
            GRILLE grille = new GRILLE(expectedHauteur, expectedLargeur, expectedTaille,
                expectedDisponibilites, expectedPositionsIds, expectedPiecesDeJeu);

            // Assert
            Assert.AreEqual(expectedHauteur, grille.HAUTEUR);
            Assert.AreEqual(expectedLargeur, grille.LARGEUR);
            Assert.AreEqual(expectedTaille, grille.TAILLE);
            Assert.AreSame(expectedDisponibilites, grille.DISPONIBILITES);
            Assert.AreSame(expectedPositionsIds, grille.POSITONS_IDS);
            Assert.AreSame(expectedPiecesDeJeu, grille.PIECES_DE_JEU);
        }

        [TestMethod()]
        public void GRILLE_TEST_CONSTRUCTEUR_2()
        {
            // Act
            GRILLE grille = new GRILLE();

            // Assert
            Assert.AreEqual(0, grille.HAUTEUR);
            Assert.AreEqual(0, grille.LARGEUR);
            Assert.AreEqual(0, grille.TAILLE);
            Assert.IsNotNull(grille.DISPONIBILITES);
            Assert.AreEqual(0, grille.DISPONIBILITES.GetLength(0));
            Assert.AreEqual(0, grille.DISPONIBILITES.GetLength(1));
            Assert.IsNotNull(grille.POSITONS_IDS);
            Assert.AreEqual(0, grille.POSITONS_IDS.GetLength(0));
            Assert.AreEqual(0, grille.POSITONS_IDS.GetLength(1));
            Assert.IsNotNull(grille.PIECES_DE_JEU);
            Assert.AreEqual(0, grille.PIECES_DE_JEU.Count);
        }

        [TestMethod()]
        public void GRILLE_TEST_CONSTRUCTEUR_3()
        {
            // Arrange
            int expectedHauteur = 5;
            int expectedLargeur = 10;
            int expectedTaille = expectedHauteur * expectedLargeur;
            bool[,] expectedDisponibilites = new bool[expectedHauteur, expectedLargeur];
            int[,] expectedPositionsIds = new int[expectedHauteur, expectedLargeur];
            List<PIECE_DE_JEU> expectedPiecesDeJeu = new List<PIECE_DE_JEU>()
            {
                new PIECE_DE_JEU(1, new BATEAU(3, "Bateau1"), new List<POINT>()),
                new PIECE_DE_JEU(2, new BATEAU(4, "Bateau2"), new List<POINT>()),
                new PIECE_DE_JEU(3, new BATEAU(2, "Bateau3"), new List<POINT>())
            };

            GRILLE originalGrille = new GRILLE(expectedHauteur, expectedLargeur, expectedTaille,
                expectedDisponibilites, expectedPositionsIds, expectedPiecesDeJeu);

            // Act
            GRILLE grille = new GRILLE(originalGrille);

            // Assert
            Assert.AreEqual(expectedHauteur, grille.HAUTEUR);
            Assert.AreEqual(expectedLargeur, grille.LARGEUR);
            Assert.AreEqual(expectedTaille, grille.TAILLE);
            Assert.AreSame(expectedDisponibilites, grille.DISPONIBILITES);
            Assert.AreSame(expectedPositionsIds, grille.POSITONS_IDS);
            Assert.AreSame(expectedPiecesDeJeu, grille.PIECES_DE_JEU);
        }
    }
}