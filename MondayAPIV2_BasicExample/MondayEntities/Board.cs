using System.Collections.Generic;
using Newtonsoft.Json;

namespace MondayV2API_BasicExample.MondayEntities
{
    public class Board
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "items")]
        public IList<Item> Items { get; set; }

    }
}