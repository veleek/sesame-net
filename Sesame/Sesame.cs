using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ben.CandyHouse
{
    /// <summary>
    /// Contains the detailed device information for a single Sesame device.
    /// </summary>
    public class Sesame
    {
        /// <summary>
        /// Create a new <see cref="Sesame"/>
        /// </summary>
        public Sesame() : this(null)
        {
        }

        /// <summary>
        /// Create a new <see cref="Sesame"/>
        /// </summary>
        /// <param name="client">The client used to make control request for this sesame.</param>
        public Sesame(SesameClient client)
        {
            this.Client = client;
        }

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

        /// <summary>
        /// Gets or sets the SesameClient which is used to make control requests for this sesame.
        /// </summary>
        public SesameClient Client { get; set; }

        /// <summary>
        /// Unlock this sesame.
        /// </summary>
        public async Task UnlockAsync()
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }

            await this.Client.ControlSesameAsync(this, ControlOperation.Unlock);
            this.IsUnlocked = true;
        }

        /// <summary>
        /// Lock this sesame.
        /// </summary>
        public async Task LockAsync()
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }

            await this.Client.ControlSesameAsync(this, ControlOperation.Lock);
            this.IsUnlocked = false;
        }

        /// <summary>
        /// Refresh the state of this sesame.
        /// </summary>
        public Task RefreshAsync()
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }

            return this.Client.RefreshSesameAsync(this);
        }


    }
}
