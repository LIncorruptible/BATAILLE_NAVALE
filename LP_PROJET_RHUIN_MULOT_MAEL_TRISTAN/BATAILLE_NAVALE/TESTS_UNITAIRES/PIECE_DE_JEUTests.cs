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
    public class PIECE_DE_JEUTests
    {
        [TestMethod()]
        public void PIECE_DE_JEU_TEST_CONSTRUCTEUR_0()
        {
            // Act
            PIECE_DE_JEU piece = new PIECE_DE_JEU();

            // Assert
            Assert.AreEqual(0, piece.ID);
            Assert.IsNotNull(piece.BATEAU);
            Assert.AreEqual(0, piece.BATEAU.TAILLE);
            Assert.AreEqual("EAU", piece.BATEAU.NOM);
            Assert.IsNotNull(piece.VECTEUR);
            Assert.AreEqual(0, piece.VECTEUR.Count);
            Assert.IsFalse(piece.EST_COULE);
        }

        [TestMethod()]
        public void PIECE_DE_JEU_TEST_CONSTRUCTEUR_1()
        {
            // Arrange
            int expectedId = 1;
            BATEAU expectedBateau = new BATEAU(3, "Bateau1");
            List<POINT> expectedVecteur = new List<POINT>()
            {
                new POINT(0, 0),
                new POINT(0, 1),
                new POINT(0, 2)
            };

            // Act
            PIECE_DE_JEU piece = new PIECE_DE_JEU(expectedId, expectedBateau, expectedVecteur);

            // Assert
            Assert.AreEqual(expectedId, piece.ID);
            Assert.AreSame(expectedBateau, piece.BATEAU);
            Assert.AreSame(expectedVecteur, piece.VECTEUR);
            Assert.IsFalse(piece.EST_COULE);
        }
    }
}