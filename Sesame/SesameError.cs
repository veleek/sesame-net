using Newtonsoft.Json;

namespace Ben.Sesame
{
    /// <summary>
    /// The structure of an error message that is returned by Sesame APIs.
    /// </summary>
    public class SesameError
    {
        /// <summary>
        /// Gets or sets the message indicating the error that occurred.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
