using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The content for a Control request.
    /// </summary>
    public class ControlRequest
    {
        /// <summary>
        /// The operation to execute on the Sesame.
        /// </summary>
        [JsonProperty("command")]
        public string Command { get; set; }
    }
}
