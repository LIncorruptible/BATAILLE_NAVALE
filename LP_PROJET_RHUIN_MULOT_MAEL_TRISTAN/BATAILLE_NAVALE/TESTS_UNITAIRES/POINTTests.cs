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
    public class POINTTests
    {
        [TestMethod()]
        public void POINT_TEST_CONSTRUCTEUR_0()
        {
            // Arrange
            int expectedX = 3;
            int expectedY = 5;
            bool expectedTouche = true;

            // Act
            POINT point = new POINT(expectedX, expectedY, expectedTouche);

            // Assert
            Assert.AreEqual(expectedX, point.X);
            Assert.AreEqual(expectedY, point.Y);
            Assert.AreEqual(expectedTouche, point.TOUCHE);
        }

        [TestMethod()]
        public void POINT_TEST_CONSTRUCTEUR_1()
        {
            // Act
            POINT point = new POINT();

            // Assert
            Assert.AreEqual(-1, point.X);
            Assert.AreEqual(-1, point.Y);
            Assert.IsFalse(point.TOUCHE);
        }

        [TestMethod()]
        public void POINT_TEST_CONSTRUCTEUR_2()
        {
            // Arrange
            int expectedX = 3;
            int expectedY = 5;

            // Act
            POINT point = new POINT(expectedX, expectedY);

            // Assert
            Assert.AreEqual(expectedX, point.X);
            Assert.AreEqual(expectedY, point.Y);
            Assert.IsFalse(point.TOUCHE);
        }

        [TestMethod()]
        public void POINT_TEST_CONSTRUCTEUR_3()
        {
            // Arrange
            POINT originalPoint = new POINT(3, 5, true);

            // Act
            POINT point = new POINT(originalPoint);

            // Assert
            Assert.AreEqual(originalPoint.X, point.X);
            Assert.AreEqual(originalPoint.Y, point.Y);
            Assert.AreEqual(originalPoint.TOUCHE, point.TOUCHE);
        }
    }
}