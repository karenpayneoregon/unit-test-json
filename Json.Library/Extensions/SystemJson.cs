using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Json.Library.Converters;


namespace Json.Library.Extensions
{
    /// <summary>
    /// Language extension methods for <see cref="System.Text.Json"/>
    /// </summary>
    public static class SystemJson
    {

        /// <summary>
        /// Convert a json string to a list of T
        /// </summary>
        /// <typeparam name="T">Type to deserialize</typeparam>
        /// <param name="jsonString">Valid json</param>
        /// <returns>List&lt;T&gt;</returns>
        public static List<T> JSonToList<T>(this string jsonString) => 
            JsonSerializer.Deserialize<List<T>>(jsonString);

        /// <summary>
        /// Save List&lt;T&gt; to file
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="sender">Type to save</param>
        /// <param name="fileName">File to save too</param>
        /// <param name="format">true to format json, false not to format json</param>
        /// <returns>
        /// name value tuple, success of operation and a exception on failure
        /// </returns>
        public static (bool result, Exception exception) JsonToFile<T>(this T sender, string fileName, bool format = true)
        {

            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                File.WriteAllText(fileName, JsonSerializer.Serialize(sender, format ? options : null));

                return (true, null);

            }
            catch (Exception exception)
            {
                return (false, exception);
            }

        }
        /// <summary>
        /// Deserialize from Json string to TModel using <see cref="UnixEpochDateTimeOffsetConverter"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize Json to</typeparam>
        /// <param name="json">Valid json for deserialize T too.</param>
        /// <returns>single instance of T</returns>
        public static T DeserializeObjectUnixEpochDateTime<T>(string json)
        {
            JsonSerializerOptions options = new();
            options.Converters.Add(new UnixEpochDateTimeConverter());
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
