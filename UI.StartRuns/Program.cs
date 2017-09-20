using Data;
using Logic.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System.IO;
using System.Reflection;
using UI.StartRuns.Registries;

namespace UI.StartRuns
{
    class Program
    {
        private static IContainer _container;
        private static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            _container = new Container();
            Startup();
            ConfigureServices();


            var starter = _container.GetInstance<RunStarter>();
            starter.Run();
        }

        public static void Startup()
        {
            var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static void ConfigureServices()
        {
            _container.AddDbContext<RunnerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            _container.Configure(c =>
            {
                c.AddRegistry(new CommonRegistry());
                c.AddRegistry(new LoggingRegistry(Configuration));
                c.AddRegistry(new DataRegistry());
            });
        }
    }
}