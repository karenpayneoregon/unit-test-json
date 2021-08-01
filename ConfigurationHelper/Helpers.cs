using System.IO;
using Microsoft.Extensions.Configuration;

namespace ConfigurationHelper
{
    public class Helpers
    {
        private static string _fileName = "appsettings.json";
        public static UserSettings UserSettings()
        {
            InitConfiguration();
            
            var settings = InitOptions<UserSettings>("AppSettings");
            
            return new UserSettings()
            {
                User = settings.User,
                Pass = settings.Pass,
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
