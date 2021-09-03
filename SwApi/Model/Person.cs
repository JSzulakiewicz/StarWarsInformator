using Newtonsoft.Json;
using System.Collections.Generic;

namespace SwApiClient.Model
{
    public class Person :BaseSwEntity
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "birth_year")]
        public string Birth { get; set; }

        [JsonProperty(PropertyName = "eye_color")]
        public string Eyes { get; set; }

        [JsonProperty]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "hair_color")]
        public string Hair { get; set; }

        [JsonProperty]
        public string Height { get; set; }

        [JsonProperty]
        public string Mass { get; set; }

        [JsonProperty(PropertyName = "skin_color")]
        public string Skin { get; set; }

        [JsonProperty]
        public string Homeworld { get; set; }

        [JsonProperty]
        public IEnumerable<string> Films { get; set; }

        [JsonProperty]
        public IEnumerable<string> Species { get; set; }

        [JsonProperty]
        public IEnumerable<string> Starships { get; set; }

        [JsonProperty]
        public IEnumerable<string> Vehicles { get; set; }
    }
}
