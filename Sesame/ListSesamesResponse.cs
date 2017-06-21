using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ben.CandyHouse
{
    /// <summary>
    /// The response content for a List Sesames request.
    /// </summary>
    public class ListSesamesResponse
    {
        /// <summary>
        /// The Sesame device details.
        /// </summary>
        [JsonProperty("sesames")]
        public List<Sesame> Sesames { get; set; }
    }
}
