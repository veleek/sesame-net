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
        /// Gets or sets the serial number for the Sesame.
        /// </summary>
        [JsonProperty("serial")]
        public string Serial { get; set; }

        /// <summary>
        /// Gets or sets the model of the Sesame.
        /// </summary>
        [JsonProperty("model")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the configured friendly name for the Sesame.
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Sesame is locked.
        /// </summary>
        [JsonProperty("locked")]
        public bool? IsLocked { get; set; }

        /// <summary>
        /// Gets or sets the percentage of battery life left on the Sesame.
        /// </summary>
        [JsonProperty("battery")]
        public int Battery { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Sesame is currently responding.
        /// </summary>
        [JsonProperty("responsive")]
        public bool IsResponsive { get; set; }

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
        public async Task<string> UnlockAsync(bool waitForComplete = false)
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }

            string taskId = await this.Client.ControlSesameAsync(this, ControlOperation.Unlock);

            if (waitForComplete)
            {
                var executionResult = await this.Client.WaitForOperationAsync(taskId);
                if (executionResult.Successful == true)
                {
                    this.IsLocked = false;
                }
                else
                {
                    throw new SesameException(executionResult.Error);
                }
            }

            return taskId;
        }

        /// <summary>
        /// Lock this sesame.
        /// </summary>
        public async Task<string> LockAsync(bool waitForComplete = false)
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }

            string taskId = await this.Client.ControlSesameAsync(this, ControlOperation.Lock);

            if (waitForComplete)
            {
                var executionResult = await this.Client.WaitForOperationAsync(taskId);
                if (executionResult.Successful == true)
                {
                    this.IsLocked = true;
                }
                else
                {
                    throw new SesameException(executionResult.Error);
                }
            }

            return taskId;
        }

        /// <summary>
        /// Force the server to update the device status for this device.
        /// </summary>
        /// <returns></returns>
        public async Task<string> SyncAsync()
        {
            this.EnsureClient();

            return await this.Client.ControlSesameAsync(this, ControlOperation.Sync);
        }

        /// <summary>
        /// Refresh the state of this sesame.
        /// </summary>
        public Task RefreshAsync()
        {
            this.EnsureClient();

            return this.Client.RefreshSesameAsync(this);
        }

        private void EnsureClient()
        {
            if (this.Client == null)
            {
                throw new InvalidOperationException("You must initialize Client before making any calls.");
            }
        }
    }
}
