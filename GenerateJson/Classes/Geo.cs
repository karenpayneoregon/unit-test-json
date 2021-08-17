using System.Text.Json.Serialization;

namespace GenerateJson.Classes
{
    public class Geo
    {
        [JsonPropertyName("lat")]
        public string Longitude { get; set; }
        [JsonPropertyName("lng")]
        public string Latitude { get; set; }

        public override string ToString() => $"{Latitude}, {Longitude}";
    }
}