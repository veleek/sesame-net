using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ben.Sesame
{
    public class ListSesamesResponse
    {
        [JsonProperty("sesames")]
        public List<SesameInfo> Sesames { get; set; }
    }
}
