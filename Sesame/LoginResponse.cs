using Newtonsoft.Json;

namespace Ben.Sesame
{
    public class LoginResponse
    {
        [JsonProperty("authorization")]
        public string Authorization { get; set; }
    }
}
