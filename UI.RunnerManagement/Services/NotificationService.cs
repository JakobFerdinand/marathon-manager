using Notifications.Wpf;
using System;

namespace UI.RunnerManagement.Services
{
    public enum NotificationType
    {
        Information,
        Success,
        Warning,
        Error
    }

    public interface INotificationService
    {
        void ShowNotification(string message, string title = null, NotificationType type = NotificationType.Information, TimeSpan? expirationTime = null, Action onClick = null);
        void ShowWindowsNotification(string message, string title = null, NotificationType type = NotificationType.Information);
    }

    internal class NotificationService : INotificationService
    {
        private readonly INotificationManager notificationManager;

        public NotificationService(INotificationManager notificationManager)
            => this.notificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));

        public void ShowNotification(string message, string title = null, NotificationType type = NotificationType.Information, TimeSpan? expirationTime = null, Action onClick = null)
            => notificationManager.Show(
                new NotificationContent { Title = title, Message = message, Type = (Notifications.Wpf.NotificationType)(int)type }
                , areaName: "NotificationArea"
                , expirationTime
                , onClick);

        public void ShowWindowsNotification(string message, string title = null, NotificationType type = NotificationType.Information)
            => notificationManager.Show(new NotificationContent { Title = title, Message = message, Type = (Notifications.Wpf.NotificationType)(int)type });
    }
}
