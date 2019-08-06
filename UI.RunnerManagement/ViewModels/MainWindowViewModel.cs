using Core;
using System;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly INotificationService notificationService;

        public MainWindowViewModel(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));

            InitializeCommand = new Command(Initialize);
        }

        public ICommand InitializeCommand { get; }
        public event Action<bool> CouldConnectToDatabaseEvent;

        private void Initialize()
        {
            var canConnect = unitOfWork.Database.CanConnect();
            if (!canConnect)
                notificationService.ShowNotification("Die Verbindung zur Datenbank konnte nicht hergestellt werden. Bitte überprüfen Sie die Verbindungseinstellungen und erstellen sie gegebenfalls eine Datenbank über die Administrationsoberfläche."
                    , "Verbindung zur Datenbank fehlgeschlagen."
                    , NotificationType.Error);

            CouldConnectToDatabaseEvent?.Invoke(canConnect);
        }
    }
}
