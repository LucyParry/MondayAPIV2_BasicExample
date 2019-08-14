using Newtonsoft.Json;

namespace MondayV2API_BasicExample.MondayEntities
{
    public class ItemColumnValue
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "value")]
        public dynamic Value { get; set; }        
    }
}