using System.Text.Json;
using Json.Library.Converters;

namespace Json.Library.Classes
{
    public class JSonHelper
    {

        /// <summary>
        /// Deserialize from Json string to TModel using <see cref="UnixEpochDateTimeConverter"/>
        /// </summary>
        /// <typeparam name="TModel">Type to deserialize Json to</typeparam>
        /// <param name="json">Valid json for deserialize TModel too.</param>
        /// <returns>single instance of TModel</returns>
        public static TModel DeserializeObjectUnixEpochDateTime<TModel>(string json)
        {

            JsonSerializerOptions options = new();
            options.Converters.Add(new UnixEpochDateTimeConverter());

            return JsonSerializer.Deserialize<TModel>(json, options);

        }
    }
}
