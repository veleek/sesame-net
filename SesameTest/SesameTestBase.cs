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
            string apiKey = ProtectedData.Read("apiKey");
            this.client = new SesameClient(apiKey);
        }
    }
}
