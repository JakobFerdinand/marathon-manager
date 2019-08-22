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
        private readonly IUnitOfWork unitOfWork;
        private readonly IConnectionstringService connectionstringService;
        private readonly IDialogService dialogService;
        private readonly INotificationService notificationService;

        public CreateRestoreDatabaseViewModel(
            IUnitOfWork unitOfWork
            , IConnectionstringService connectionstringService
            , IDialogService dialogService
            , INotificationService notificationService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.connectionstringService = connectionstringService ?? throw new ArgumentNullException(nameof(connectionstringService));
            this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
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
                    CanConnectToServer = unitOfWork.Database.IsServerOnline(Server);
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
            => connectionstringService.SaveConnectionDetails((Server, Database));
        private bool CanSaveConnectionDetails()
            => false
            && !Server.IsNullOrEmpty()
            && !Database.IsNullOrEmpty();

        public ICommand RefreshServersCommand { get; }
        private void RefreshServers()
        {
            AvailableServers.Clear();
            AvailableDatabases.Clear();
            var servers = unitOfWork.Database.GetAvailableServers();
            AvailableServers.AddRange(servers);
        }

        public ICommand RefreshDatabasesCommand { get; }
        private void RefreshDatabases()
        {
            AvailableDatabases.Clear();
            var databases = unitOfWork.Database.GetAllDatabases(Server);
            AvailableDatabases.AddRange(databases);
        }
        private bool CanRefreshDatabases()
            => !Server.IsNullOrEmpty();

        public ICommand RecreateDatabaseCommand { get; }
        private void RecreateDatabase()
        {
            if (unitOfWork.Database.GetAllDatabases(Server).Any(d => d == Database))
                if(dialogService.ShowYesNoMessageBox("Es ist bereits eine Datenbank vorhanden. Wollen Sie diese ersetzten?", "Datenbank vorhanden") is MessageBoxResult.Yes)
                    return;

            unitOfWork.Database.EnsureDeleted();
            unitOfWork.Database.EnsureCreated();
            notificationService.ShowNotification("Die Datenbank wurde neu erstellt.", "Datenbank erstellt", NotificationType.Success);

        }
        private bool CanRecreateDatabase()
            => CanConnectToServer;
    }
}
