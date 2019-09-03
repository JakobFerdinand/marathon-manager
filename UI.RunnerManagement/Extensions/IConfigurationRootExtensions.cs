namespace Microsoft.Extensions.Configuration
{
    internal static class IConfigurationRootExtensions
    {
        public static (string accessToken, string environment) GetRoolbarSettings(this IConfigurationRoot @this)
        {
            var rollbar = @this.GetSection("Rollbar");
            return (
                rollbar.GetSection("AccessToken").Value,
                rollbar.GetSection("Environment").Value
                );
        }
    }
}
