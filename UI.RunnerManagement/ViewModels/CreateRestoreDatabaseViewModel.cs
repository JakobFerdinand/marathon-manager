using Core;
using Logic.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.ViewModels
{
    public class CreateRestoreDatabaseViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionstringService _connectionstringService;
        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;

        public CreateRestoreDatabaseViewModel(
            IUnitOfWork unitOfWork
            , IConnectionstringService connectionstringService
            , IDialogService dialogService
            , INotificationService notificationService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _connectionstringService = connectionstringService ?? throw new ArgumentNullException(nameof(connectionstringService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            (Server, Database) = connectionstringService.GetConnectionDetails();

            SaveConnectionDetailsCommand = new ExtendedCommand(
                SaveConnectionDetails,
                () => "Änderungen speichern",
                CanSaveConnectionDetails,
                () => "Diese Funktion wird noch nicht unterstützt.");
            RefreshServersCommand = new Command(RefreshServers);
            RefreshDatabasesCommand = new Command(RefreshDatabases, CanRefreshDatabases);
            RecreateDatabaseCommand = new Command(RecreateDatabase, CanRecreateDatabase);
        }

        public ObservableCollection<string> AvailableServers { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableDatabases { get; } = new ObservableCollection<string>();

        private string server;
        public string Server
        {
            get => server;
            set
            {
                if (Set(ref server, value) && !Server.IsNullOrEmpty())
                    CanConnectToServer = _unitOfWork.Database.IsServerOnline(Server);
            }
        }

        private string database;
        public string Database
        {
            get => database;
            set => Set(ref database, value);
        }

        private bool canConnectToServer;
        public bool CanConnectToServer
        {
            get => canConnectToServer;
            set => Set(ref canConnectToServer, value);
        }

        public ICommand SaveConnectionDetailsCommand { get; }
        private void SaveConnectionDetails()
            => _connectionstringService.SaveConnectionDetails((Server, Database));
        private bool CanSaveConnectionDetails()
            => false
            && !Server.IsNullOrEmpty()
            && !Database.IsNullOrEmpty();

        public ICommand RefreshServersCommand { get; }
        private void RefreshServers()
        {
            AvailableServers.Clear();
            AvailableDatabases.Clear();
            var servers = _unitOfWork.Database.GetAvailableServers();
            AvailableServers.AddRange(servers);
        }

        public ICommand RefreshDatabasesCommand { get; }
        private void RefreshDatabases()
        {
            AvailableDatabases.Clear();
            var databases = _unitOfWork.Database.GetAllDatabases(Server);
            AvailableDatabases.AddRange(databases);
        }
        private bool CanRefreshDatabases()
            => !Server.IsNullOrEmpty();

        public ICommand RecreateDatabaseCommand { get; }
        private void RecreateDatabase()
        {
            if (_unitOfWork.Database.GetAllDatabases(Server).Any(d => d == Database))
                if(_dialogService.ShowYesNoMessageBox("Es ist bereits eine Datenbank vorhanden. Wollen Sie diese ersetzten?", "Datenbank vorhanden") is MessageBoxResult.Yes)
                    return;

            _unitOfWork.Database.EnsureDeleted();
            _unitOfWork.Database.EnsureCreated();
            _notificationService.ShowNotification("Die Datenbank wurde neu erstellt.", "Datenbank erstellt", NotificationType.Success);

        }
        private bool CanRecreateDatabase()
            => CanConnectToServer;
    }
}
