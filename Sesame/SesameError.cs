using Newtonsoft.Json;

namespace Ben.Sesame
{
    public class SesameError
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
