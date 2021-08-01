using System.IO;
using Microsoft.Extensions.Configuration;

namespace ConfigurationHelper
{
    /// <summary>
    /// Basic class to read appsettings.json
    /// 
    /// If needed several code changes can allow one than one json
    /// file to be used for different environments and the path
    /// may be adjusted to permit the json file to be in another location
    /// besides the root of a project.
    /// </summary>
    public class Helpers
    {
        private static string _fileName = "appsettings.json";
        public static UserSettings UserSettings()
        {
            InitConfiguration();
            
            var settings = InitOptions<UserSettings>("AppSettings");
            
            return new UserSettings()
            {
                UserName = settings.UserName,
                Password = settings.Password,
                Server = settings.Server
            };

        }
        private static IConfigurationRoot InitConfiguration()
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_fileName, true,true);

            return builder.Build();

        }
        public static T InitOptions<T>(string section) where T : new() =>
            InitConfiguration().GetSection(section).Get<T>();
    }
}
