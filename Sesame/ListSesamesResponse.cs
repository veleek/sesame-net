using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ben.Sesame
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
        public List<SesameInfo> Sesames { get; set; }
    }
}
