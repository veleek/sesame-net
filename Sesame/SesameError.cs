using System.Diagnostics.Tracing;
using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The structure of an error message that is returned by Sesame APIs.
    /// </summary>
    public class SesameError
    {
        /// <summary>
        /// Gets or sets the code for the error that occurred.
        /// </summary>
        [JsonProperty("code")]
        public SesameErrorCode Code { get; set; }

        /// <summary>
        /// Gets or sets the message indicating the error that occurred.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
