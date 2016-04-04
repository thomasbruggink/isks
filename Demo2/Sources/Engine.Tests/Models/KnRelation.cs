using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Engine.Tests.Models
{
    public class KnRelation
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, double> Data { get; set; }
    }
}
