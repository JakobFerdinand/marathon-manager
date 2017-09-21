using Microsoft.Extensions.Configuration;

namespace Logic.Common.Extensions
{
    public static class IConfigurationExtentions 
    {
        /// <summary>
        /// Short for Configuration.GetSection("Logging")[name];
        /// </summary>
        public static string GetLoggingPath(this IConfiguration configuration, string name) => configuration.GetSection("Logging")[name];
    }
}
