using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SesameTest
{
    [TestClass]
    public class ControlSesameTests
    {
        private SesameClient client;
        private List<SesameInfo> sesames;

        [TestInitialize]
        public void Initialize()
        {
            client = new SesameClient();

            string email = ProtectedData.Read("email");
            string password = ProtectedData.Read("password");
            client.LoginAsync(email, password).Wait();

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
            await loggedOutClient.ControlSesame(sesames[0], ControlType.Lock);
        }

        [TestMethod]
        public async Task LockSesame()
        {
            await client.ControlSesame(sesames[0], ControlType.Lock);
        }

        [TestMethod]
        public async Task UnlockSesame()
        {
            await client.ControlSesame(sesames[0], ControlType.Unlock);
        }
    }
}
