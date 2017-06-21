using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.CandyHouse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SesameTest
{
    [TestClass]
    public class ControlSesameTests : SesameTestBase
    {
        private List<Sesame> sesames;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            this.LoginAsync().Wait();

            this.sesames = this.client.ListSesamesAsync().Result;
            if(this.sesames.Count == 0)
            {
                throw new InvalidOperationException("Unable to run Sesame control tests on an account without any associated Sesame devices.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task ControlSesameWithoutLoggingIn()
        {
            SesameClient loggedOutClient = new SesameClient();
            await loggedOutClient.ControlSesameAsync(this.sesames[0], ControlOperation.Lock);
        }

        [TestMethod]
        public async Task LockSesame()
        {
            Sesame s = this.sesames[0];
            await s.LockAsync();
            Assert.IsFalse(s.IsUnlocked);

            // Also make sure it's actually locked by querying the device.
            await s.RefreshAsync();
            Assert.IsFalse(s.IsUnlocked);
        }

        [TestMethod]
        public async Task UnlockSesame()
        {
            Sesame s = this.sesames[0];
            await s.LockAsync();
            Assert.IsTrue(s.IsUnlocked);

            // Also make sure it's actually unlocked by querying the device.
            await s.RefreshAsync();
            Assert.IsTrue(s.IsUnlocked);
        }
    }
}
