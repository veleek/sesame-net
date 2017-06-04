using Newtonsoft.Json;

namespace Ben.Sesame
{
    public class SesameInfo
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("is_unlocked")]
        public bool IsUnlocked { get; set; }

        [JsonProperty("api_enabled")]
        public bool ApiEnabled { get; set; }
    }
}
