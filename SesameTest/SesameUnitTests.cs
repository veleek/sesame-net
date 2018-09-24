using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.CandyHouse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SesameTest
{
    [TestClass]
    public class SesameUnitTests : SesameTestBase
    {
        private List<Sesame> sesames;
        private Sesame sesame;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            this.sesames = this.client.ListSesamesAsync().Result;
            if(this.sesames.Count == 0)
            {
                throw new InvalidOperationException("Unable to run Sesame control tests on an account without any associated Sesame devices.");
            }

            this.sesame = this.sesames[0];
        }

        [TestMethod]
        public async Task LockSesame()
        {
            string taskId = await this.sesame.LockAsync(true);
            Assert.IsNotNull(taskId);

            await this.sesame.RefreshAsync();
            Assert.IsNotNull(this.sesame.IsLocked);
            Assert.IsTrue(this.sesame.IsLocked.Value);
        }

        [TestMethod]
        public async Task UnlockSesame()
        {
            string taskId = await this.sesame.UnlockAsync(true);
            Assert.IsNotNull(taskId);

            await this.sesame.RefreshAsync();
            Assert.IsNotNull(this.sesame.IsLocked);
            Assert.IsFalse(this.sesame.IsLocked.Value);
        }

        [TestMethod]
        public async Task SyncSesame()
        {
            string taskId = await this.sesame.SyncAsync();

            ExecutionResult result = await this.sesame.Client.WaitForOperationAsync(taskId);
            Assert.IsNotNull(result.Successful);
        }
    }
}
