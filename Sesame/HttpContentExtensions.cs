using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ben.Sesame
{
    /// <summary>
    /// Helper methods for interacting with HttpContent objects.
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Deserialize the content as JSON object into a new instance of the given type.
        /// </summary>
        /// <typeparam name="TContent">The type to deserialize the content as.</typeparam>
        /// <param name="httpContent">The content object containing the JSON to deserialize.</param>
        /// <returns>A new instance of <typeparamref name="TContent"/> represented by the JSON.</returns>
        public static async Task<TContent> ReadAsJsonAsync<TContent>(this HttpContent httpContent)
        {
            string rawContent = await httpContent.ReadAsStringAsync();
            TContent content = JsonConvert.DeserializeObject<TContent>(rawContent);
            return content;
        }

        /// <summary>
        /// Deserialize the content as JSON data.
        /// </summary>
        /// <param name="httpContent">The content object containing the JSON to deserialize.</param>
        /// <returns>A new object represented by the JSON.</returns>
        public static async Task<object> ReadAsJsonAsync(this HttpContent httpContent)
        {
            string rawContent = await httpContent.ReadAsStringAsync();
            object content = JsonConvert.DeserializeObject(rawContent);
            return content;
        }
    }
}
