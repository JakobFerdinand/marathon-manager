using Data;
using Data.Logging;
using Logic.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;
using UI.RunnerManagement.Registries;

namespace UI.RunnerManagement.ViewModels
{
    internal class ViewModelLocator
    {
        private readonly IContainer _container;
        private IConfigurationRoot Configuration { get; set; }

        public CategoriesViewModel CategoriesViewModel => _container.GetInstance<CategoriesViewModel>();
        public MainWindowViewModel MainWindowViewModel => _container.GetInstance<MainWindowViewModel>();
        public RunnersViewModel RunnersViewModel => _container.GetInstance<RunnersViewModel>();

        public ViewModelLocator()
        {
            _container = new Container();

            Startup();
            ConfigureServices();
        }

        public void Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices()
        {
            _container.AddDbContext<RunnerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            _container.AddDbContext<LoggingDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Logging")));
            _container.Configure(c =>
            {
                c.AddRegistry(new CommonRegistry());
                c.AddRegistry(new LoggingRegistry(Configuration));
                c.AddRegistry(new DataRegistry());
            });
            _container.RegisterConcreteTypeAsSingelton<MainWindowViewModel>();
            _container.RegisterConcreteTypeAsSingelton<RunnersViewModel>();
        }
    }
}
