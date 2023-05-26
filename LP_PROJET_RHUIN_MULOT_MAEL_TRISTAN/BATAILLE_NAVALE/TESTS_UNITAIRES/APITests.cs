using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIBLIOTHEQUE_INTERACTIONS_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBLIOTHEQUE_INTERACTIONS_API.Tests
{
    [TestClass()]
    public class APITests
    {
        [TestMethod()]
        public void API_TEST_CONSTRUCTEUR_0()
        {
            // Arrange
            string expectedURL = "";
            string expectedKey = "";

            // Act
            API api = new API();

            // Assert
            Assert.AreEqual(expectedURL, api.URL);
            Assert.AreEqual(expectedKey, api.KEY);
        }

        [TestMethod()]
        public void API_TEST_CONSTRUCTEUR_1()
        {
            // Arrange
            string expectedURL = "https://api-lprgi.natono.biz/api/GetConfig";
            string expectedKey = "lprgi_api_key_2023";

            // Act
            API api = new API(expectedURL, expectedKey);

            // Assert
            Assert.AreEqual(expectedURL, api.URL);
            Assert.AreEqual(expectedKey, api.KEY);
        }


        [TestMethod()]
        public async Task RecupererDataTest()
        {
            // Arrange
            string expectedURL = "https://api-lprgi.natono.biz/api/GetConfig";
            string expectedKey = "lprgi_api_key_2023";

            API api = new API(expectedURL, expectedKey);

            // Act
            string data = await api.RecupererData();

            // Assert
            Assert.IsNotNull(data);
        }
    }
}