using System.Collections.Generic;
using Newtonsoft.Json;

namespace MondayV2API_BasicExample.MondayEntities
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "group")]
        public ItemGroup Group { get; set; }

        [JsonProperty(PropertyName = "column_values")]
        public List<ItemColumnValue> ItemColumnValues { get; set; }
    }
}