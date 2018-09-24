using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The structure of an error message that is returned by Sesame APIs.
    /// </summary>
    public class SesameError
    {
        /// <summary>
        /// The error string.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
