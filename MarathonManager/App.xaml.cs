using Microsoft.Extensions.Configuration;
using System;
using System.Windows;
using UI.RunnerManagement;
using UI.RunnerManagement.Views;

namespace MarathonManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IConfigurationRoot configuration;

        public App()
            => throw new InvalidOperationException("This constructor can´t be used.");

        public App(IConfigurationRoot configuration)
        {
            InitializeComponent();
            this.configuration = configuration;
            var locator = new ViewModelLocator(configuration);
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                { "Locator", locator }
            });
        }

        protected override void OnStartup(StartupEventArgs e) => new MainWindow().Show();
    }
}
