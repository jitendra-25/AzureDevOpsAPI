using Newtonsoft.Json;
using System.Collections.Generic;

namespace AzureDevOpsAPI.Models
{
    [JsonObject]
    public class SprintEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("value")]
        public List<Sprint> Sprints { get; set; }
    }

    public class Sprint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Attributes Attributes { get; set; }
        public string Url { get; set; }
    }

    public class Attributes
    {
        public object StartDate { get; set; }
        public object FinishDate { get; set; }
        public string TimeFrame { get; set; }
    }

}
