using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIBLIOTHEQUE_AFFICHAGE_CONSOLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_AFFICHAGE_CONSOLE.Tests
{
    [TestClass()]
    public class DISPLAYTests
    {
        [TestMethod()]
        public void AfficherPlateauTest()
        {
            // Arrange
            int[,] plateau = new int[,]
            {
                { 0, 0, 1, 1 },
                { 2, -2, 0, 0 },
                { 0, 0, -3, -3 },
                { 4, 4, 4, 0 }
            };

            // Act 
            DISPLAY.AfficherPlateau(plateau);

            // Assert
            Assert.IsTrue(true);

        }
    }
}