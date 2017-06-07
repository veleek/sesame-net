using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SesameTest
{
    [TestClass]
    public class SesameInfoUnitTests : SesameTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.LoginAsync().Wait();
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

        [TestMethod]
        public async Task GetSesame()
        {
            List<SesameInfo> sesames = await client.ListSesamesAsync();
            SesameInfo sesame = await client.GetSesameAsync(sesames[0].DeviceId);

            Assert.AreEqual(sesames[0].DeviceId, sesame.DeviceId, "DeviceId");
            Assert.AreEqual(sesames[0].Nickname, sesame.Nickname, "Nickname");
        }
    }
}
