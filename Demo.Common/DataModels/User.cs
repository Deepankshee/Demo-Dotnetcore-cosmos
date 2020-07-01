using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace Demo.Common.DataModels
{
    public class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("loginName")]
        public string LoginName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("occupation")]
        public string Occupation { get; set; }
        [JsonProperty("basePartitionKey")]
        public string BasePartitionKey { get; set; }
        [JsonProperty("documentType")]
        public string DocumentType { get; set; }
    }
}
