using System.Text.Json.Serialization;

namespace GenerateJson.Classes
{
    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("catchPhrase")]
        public string CatchPhrase { get; set; }
        [JsonPropertyName("bs")]
        public string Information { get; set; }

        public override string ToString() => Name;
    }
}