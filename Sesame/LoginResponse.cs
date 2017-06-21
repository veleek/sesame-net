using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The content return by a login request.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// The Sesame authorization token which should be used by all authenticated requests.
        /// </summary>
        [JsonProperty("authorization")]
        public string Authorization { get; set; }
    }
}
