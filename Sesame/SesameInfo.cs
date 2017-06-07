using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Ben.Sesame
{
    /// <summary>
    /// Contains the detailed device information for a single Sesame device.
    /// </summary>
    public class SesameInfo
    {
        /// <summary>
        /// Gets or sets the unique device ID for the Sesame.
        /// </summary>
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the configured friendly name for the Sesame.
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Sesame is unlocked.
        /// </summary>
        [JsonProperty("is_unlocked")]
        public bool IsUnlocked { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Sesame is API-enabled.  It must be paired with Virtual Station or Wi-Fi Access Point for this functionality.
        /// </summary>
        [JsonProperty("api_enabled")]
        public bool ApiEnabled { get; set; }

        /// <summary>
        /// Gets or sets the percentage of battery life left on the Sesame.
        /// </summary>
        [JsonProperty("battery")]
        public int Battery { get; set; }

        /// <summary>
        /// Gets or sets any additional properties in the Sesame info object that were provided.
        /// </summary>
        /// <remarks>
        /// If the Sesame API is expanded with additional detail.  Those values will be available here without needing to update the API.
        /// </remarks>
        [JsonExtensionData]
        public Dictionary<string, JToken> ExtraProperties { get; set; }
    }
}
