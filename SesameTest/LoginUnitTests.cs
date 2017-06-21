using System.Threading.Tasks;
using Ben.CandyHouse;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public async Task IncorrectPassword()
        {
            try
            {
                string email = ProtectedData.Read("email");
                await this.client.LoginAsync(email, "incorrectpassword");
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (SesameException e)
            {
                Assert.AreEqual(SesameErrorCode.IncorrectEmailOrPassword, e.Code);
            }
        }

        [TestMethod]
        public async Task InvalidEmail()
        {
            try
            {
                string password = ProtectedData.Read("password");
                await this.client.LoginAsync("incorrectemail", password);
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (SesameException e)
            {
                Assert.AreEqual(SesameErrorCode.InvalidEmail, e.Code);
            }
        }

        [TestMethod]
        public async Task IncorrectEmail()
        {
            try
            {
                string password = ProtectedData.Read("password");
                await this.client.LoginAsync("test@mail.com", password);
                Assert.Fail("Expected an exception to be thrown.");
            }
            catch (SesameException e)
            {
                Assert.AreEqual(SesameErrorCode.IncorrectEmailOrPassword, e.Code);
            }
        }
    }
}