using Newtonsoft.Json;

namespace MondayV2API_BasicExample.MondayEntities
{
    public class ItemGroup
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}