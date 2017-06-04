using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SesameTest
{
    [TestClass]
    public class ListSesamesUnitTests
    {
        private SesameClient client;

        [TestInitialize]
        public void Initialize()
        {
            client = new SesameClient();

            string email = ProtectedData.Read("email");
            string password = ProtectedData.Read("password");
            client.LoginAsync(email, password).Wait();
        }

        [TestMethod]
        public async Task ListSesames()
        {
            List<SesameInfo> sesames = await client.ListSesamesAsync();
            Assert.AreEqual(1, sesames.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task ListSesamesWithoutLoggingIn()
        {
            SesameClient loggedOutClient = new SesameClient();
            List<SesameInfo> sesames = await loggedOutClient.ListSesamesAsync();
            Assert.AreEqual(1, sesames.Count);
        }
    }
}
