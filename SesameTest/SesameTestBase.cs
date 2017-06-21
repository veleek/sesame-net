using Ben.CandyHouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SesameTest
{
    public class SesameTestBase
    {
        protected SesameClient client;

        [TestInitialize]
        public virtual void Initialize()
        {
            this.client = new SesameClient();
        }

        protected async Task LoginAsync()
        {
            string email = ProtectedData.Read("email");
            string password = ProtectedData.Read("password");
            await this.client.LoginAsync(email, password);
        }
    }
}
