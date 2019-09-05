namespace Microsoft.Extensions.Configuration
{
    internal static class IConfigurationRootExtensions
    {
        public static (string instrumentationKey, string environment) GetRoolbarSettings(this IConfigurationRoot @this)
        {
            var rollbar = @this.GetSection("ApplicationInsights");
            return (
                rollbar.GetSection("InstrumentationKey").Value,
                rollbar.GetSection("Environment").Value
                );
        }
    }
}
