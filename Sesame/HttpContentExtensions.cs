using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ben.Sesame
{
    public static class HttpContentExtensions
    {
        public static async Task<TContent> ReadAsJsonAsync<TContent>(this HttpContent httpContent)
        {
            string rawContent = await httpContent.ReadAsStringAsync();
            TContent content = JsonConvert.DeserializeObject<TContent>(rawContent);
            return content;
        }
    }
}
