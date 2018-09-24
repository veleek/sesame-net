using Newtonsoft.Json;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The response from a Control request.
    /// </summary>
    public class ControlResponse
    {
        /// <summary>
        /// A unique identifier for the task which can be used to query the execution result.
        /// </summary>
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
    }
}