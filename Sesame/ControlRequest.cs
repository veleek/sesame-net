using Newtonsoft.Json;

namespace Ben.Sesame
{
    public class ControlRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
