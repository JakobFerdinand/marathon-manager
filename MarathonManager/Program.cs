using Microsoft.Extensions.Configuration;
using Rollbar;
using System;
using UI.RunnerManagement;

namespace MarathonManager
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var app = new App(builder.Build());
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception e)
            {
                RollbarLocator.RollbarInstance.Error(e);
            }
            Console.ReadLine();
        }
    }
}
