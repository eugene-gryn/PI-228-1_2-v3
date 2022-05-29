using Newtonsoft.Json;
using TestTools.Enums;

namespace TestTools.Models
{
    public class AppConfiguration
    {
        [JsonProperty("Urls")]
        public Urls Url { get; set; }

        [JsonProperty("Users")]
        public Users User { get; set; }

        [JsonProperty("RemoteDriverUrl")]
        public string RemoteDriverUrl { get; set; }

        [JsonProperty("Browser")]
        public Browsers Browser { get; set; }


        public class Urls
        {
            [JsonProperty("StgMAU")]
            public string StgMAU { get; set; }
        }

        public class Users
        {
            [JsonProperty("TestUser")]
            public string TestUser { get; set; }

            [JsonProperty("TestPassword")]
            public string TestPassword { get; set; }
        }
    }
}
