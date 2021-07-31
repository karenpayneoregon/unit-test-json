using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContainerLibrary.Classes
{
    public class Posts
    {
        [JsonPropertyName("userId")]
        public int UserIdentifier { get; set; }
        [JsonPropertyName("id")]
        public int Identifier { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("body")]
        public string Contents { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
