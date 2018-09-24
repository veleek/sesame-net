using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The details of the result of a previous control request.
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        /// The status of the execution of the control request.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Indicates whether or not the control request was successful.
        /// </summary>
        [JsonProperty("successful")]
        public bool? Successful { get; set; }

        /// <summary>
        /// If the control request was not successful, indicates the error condition.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}