using Core;
using System;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;

        public MainWindowViewModel(IUnitOfWork unitOfWork, INotificationService notificationService, IDialogService dialogService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            InitializeCommand = new Command(Initialize);
        }

        public ICommand InitializeCommand { get; }
        public event Action<bool> CouldConnectToDatabaseEvent;

        private async void Initialize()
        {
            const string errorMessage = "Die Verbindung zur Datenbank konnte nicht hergestellt werden. Bitte überprüfen Sie die Verbindungseinstellungen und erstellen sie gegebenfalls eine Datenbank über die Administrationsoberfläche.";
            const string errorTitle = "Verbindung zur Datenbank fehlgeschlagen.";

            var canConnect = await _unitOfWork.Database.CanConnectAsync();
            if (!canConnect)
                _notificationService.ShowNotification(errorMessage
                    , errorTitle
                    , NotificationType.Error
                    , expirationTime: 5.Seconds()
                    , () => _dialogService.ShowOkMessageBox(errorMessage, errorTitle));

            CouldConnectToDatabaseEvent?.Invoke(canConnect);
        }
    }
}
