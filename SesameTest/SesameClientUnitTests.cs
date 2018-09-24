using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.CandyHouse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SesameTest
{
    [TestClass]
    public class SesameClientUnitTests : SesameTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        [TestMethod]
        public async Task ListSesames()
        {
            List<Sesame> sesames = await this.client.ListSesamesAsync();
            Assert.AreEqual(1, sesames.Count);
        }

        [TestMethod]
        public async Task GetSesameState()
        {
            List<Sesame> sesames = await this.client.ListSesamesAsync();
            Sesame sesame = sesames.First();

            sesame = await this.client.GetSesameStateAsync(sesame.DeviceId);

            Assert.IsNotNull(sesame);
        }

        [TestMethod]
        public async Task GetSesameStateUnknownDevice()
        {
            try
            {
                await this.client.GetSesameStateAsync(Guid.NewGuid().ToString());
                Assert.Fail("Expected call to fail.");
            }
            catch (SesameException e)
            {
                Assert.AreEqual(HttpStatusCode.Unauthorized, e.StatusCode);
                Assert.AreEqual("UNAUTHORIZED", e.Message);
            }
        }

        [TestMethod]
        public async Task GetSesameStateInvalidDeviceId()
        {
            try
            {
                await this.client.GetSesameStateAsync("Invalid_Device_Id");
                Assert.Fail("Expected call to fail.");
            }
            catch (SesameException e)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, e.StatusCode);
                Assert.AreEqual("BAD_PARAMS", e.Message);
            }
        }

        [TestMethod]
        public async Task RefreshSesameState()
        {
            List<Sesame> sesames = await this.client.ListSesamesAsync();
            Sesame sesame = sesames.First();

            await this.client.RefreshSesameAsync(sesame);
        }
    }
}
