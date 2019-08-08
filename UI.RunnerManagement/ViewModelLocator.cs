using Data;
using Data.Logging;
using Logic.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StructureMap;
using System;
using UI.RunnerManagement.Registries;
using UI.RunnerManagement.ViewModels;

namespace UI.RunnerManagement
{
    internal class ViewModelLocator
    {
        private readonly IContainer _container;
        private IConfigurationRoot Configuration { get; set; }

        public AdministrationMainViewModel AdministrationMainViewModel => _container.GetInstance<AdministrationMainViewModel>();
        public CategoriesViewModel CategoriesViewModel => _container.GetInstance<CategoriesViewModel>();
        public CreateRestoreDatabaseViewModel CreateRestoreDatabaseViewModel => _container.GetInstance<CreateRestoreDatabaseViewModel>();
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
                c.AddRegistry(new DataRegistry(bool.Parse(Configuration.GetSection("UseSampleData").Value)));
            });
            _container.Configure(c => c.ForConcreteType<AdministrationMainViewModel>().Configure.Singleton().Ctor<string>().Is(Configuration.GetSection("AdministrationPassword").Value));
            _container.RegisterConcreteTypeAsSingelton<MainWindowViewModel>();
            _container.RegisterConcreteTypeAsSingelton<RunnersViewModel>();
        }
    }
}
