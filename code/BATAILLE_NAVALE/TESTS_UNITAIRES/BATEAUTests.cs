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
    public class BATEAUTests
    {
        [TestMethod()]
        public void BATEAU_TEST_CONSTRUCTEUR_0()
        {
            // Arrange
            int expectedTaille = 0;
            string expectedNom = "EAU";

            // Act
            BATEAU bateau = new BATEAU();

            // Assert
            Assert.AreEqual(expectedTaille, bateau.TAILLE);
            Assert.AreEqual(expectedNom, bateau.NOM);
        }

        [TestMethod()]
        public void BATEAU_TEST_CONSTRUCTEUR_1()
        {
            // Arrange
            int expectedTaille = 5;
            string expectedNom = "Mon Bateau";

            // Act
            BATEAU bateau = new BATEAU(expectedTaille, expectedNom);

            // Assert
            Assert.AreEqual(expectedTaille, bateau.TAILLE);
            Assert.AreEqual(expectedNom, bateau.NOM);
        }

    }
}