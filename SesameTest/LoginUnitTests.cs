using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;

namespace SesameTest
{
    [TestClass]
    public class LoginUnitTests
    {
        private SesameClient client;

        [TestInitialize]
        public void Initialize()
        {
            client = new SesameClient();
        }

        [TestMethod]
        public async Task ValidLogin()
        {
            string email = ProtectedData.Read("email");
            string password = ProtectedData.Read("password");
            await client.LoginAsync(email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task IncorrectPassword()
        {
            string email = ProtectedData.Read("email");
            await client.LoginAsync(email, "incorrectpassword");
        }
    }
}
