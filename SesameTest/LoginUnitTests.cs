using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ben.Sesame;
using System.Threading.Tasks;

namespace SesameTest
{
    [TestClass]
    public class LoginUnitTests : SesameTestBase
    {
        [TestMethod]
        public async Task ValidLogin()
        {
            await this.LoginAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task IncorrectPassword()
        {
            string email = ProtectedData.Read("email");
            await client.LoginAsync(email, "incorrectpassword");
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task InvalidEmail()
        {
            string password = ProtectedData.Read("password");
            await client.LoginAsync("incorrectemail", password);
        }

        [TestMethod]
        [ExpectedException(typeof(SesameException))]
        public async Task IncorrectEmail()
        {
            string password = ProtectedData.Read("password");
            await client.LoginAsync("test@mail.com", password);
        }
    }
}
