using Microsoft.Extensions.Configuration;
using System.IO;

namespace SnakeGame.Source.Common
{
    public class ConfigurationHelper
    {
        public static IConfigurationSection GetSection(string sectionName)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");
            return builder.Build().GetSection(sectionName);
        }
    }
}
