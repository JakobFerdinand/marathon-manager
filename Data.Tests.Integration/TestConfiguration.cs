namespace Data.Tests.Integration
{
    internal static class TestConfiguration
    {
        public static string ConnectionString => "server=localhost;database={0};integrated security=true";
    }
}
