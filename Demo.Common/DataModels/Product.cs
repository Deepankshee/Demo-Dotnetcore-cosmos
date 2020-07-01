using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Common.DataModels
{
    public class Product
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("basePartitionKey")]
        public string BasePartitionKey { get; set; }
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }
    }
}
