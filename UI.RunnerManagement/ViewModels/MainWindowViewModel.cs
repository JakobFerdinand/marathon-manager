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
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IUnitOfWork unitOfWork, INotificationService notificationService, IDialogService dialogService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            InitializeCommand = new Command(Initialize);
        }

        public ICommand InitializeCommand { get; }
        public event Action<bool> CouldConnectToDatabaseEvent;

        private async void Initialize()
        {
            const string errorMessage = "Die Verbindung zur Datenbank konnte nicht hergestellt werden. Bitte überprüfen Sie die Verbindungseinstellungen und erstellen sie gegebenfalls eine Datenbank über die Administrationsoberfläche.";
            const string errorTitle = "Verbindung zur Datenbank fehlgeschlagen.";

            var canConnect = await unitOfWork.Database.CanConnectAsync();
            if (!canConnect)
                notificationService.ShowNotification(errorMessage
                    , errorTitle
                    , NotificationType.Error
                    , expirationTime: 5.Seconds()
                    , () => dialogService.ShowOkMessageBox(errorMessage, errorTitle));

            CouldConnectToDatabaseEvent?.Invoke(canConnect);
        }
    }
}
