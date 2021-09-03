using Newtonsoft.Json;
using System.Collections.Generic;

namespace SwApiClient.Model
{
    public class Film: BaseSwEntity
    {
        [JsonProperty]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "episode_id")]
        public string EpisodeId { get; set; }
        
        [JsonProperty(PropertyName = "opening_crawl")]
        public string OpeningCrawl { get; set; }

        [JsonProperty]
        public string Director { get; set; }

        [JsonProperty]
        public string Producer { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string Released { get; set; }

        [JsonProperty]
        public IEnumerable<string> Species { get; set; }

        [JsonProperty]
        public IEnumerable<string> Starships { get; set; }

        [JsonProperty]
        public IEnumerable<string> Vehicles { get; set; }

        [JsonProperty]
        public IEnumerable<string> Characters { get; set; }

        [JsonProperty]
        public IEnumerable<string> Planets { get; set; }
    }
}