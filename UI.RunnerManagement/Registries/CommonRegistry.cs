using Core.EventAggregation;
using Logic.Common.Interfaces;
using Logic.Common.Services;
using Notifications.Wpf;
using StructureMap;
using System.Windows;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.Registries
{
    internal class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            For<IReader>()
                .Use<ConsoleReader>()
                .Singleton();

            For<IDateTimeManager>()
                .Use<DateTimeManager>()
                .Singleton();

            For<IDialogService>()
                .Use<DialogService>()
                .Singleton();

            For<INotificationService>()
                .Use(c => new NotificationService(new NotificationManager(null)))
                .Singleton();

            For<IEventAggregator>()
                .Use<EventAggregator>()
                .Singleton();
        }
    }
}
