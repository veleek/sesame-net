using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SesameTest
{
    [TestClass]
    public class ControlSesameTests : SesameTestBase
    {
        private List<SesameInfo> sesames;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            this.LoginAsync().Wait();

            sesames = client.ListSesamesAsync().Result;
            if(sesames.Count == 0)
            {
                throw new InvalidOperationException("Unable to run Sesame control tests on an account without any associated Sesame devices.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task ControlSesameWithoutLoggingIn()
        {
            SesameClient loggedOutClient = new SesameClient();
            await loggedOutClient.ControlSesame(sesames[0], ControlOperation.Lock);
        }

        [TestMethod]
        public async Task LockSesame()
        {
            await client.ControlSesame(sesames[0], ControlOperation.Lock);
        }

        [TestMethod]
        public async Task UnlockSesame()
        {
            await client.ControlSesame(sesames[0], ControlOperation.Unlock);
        }
    }
}
