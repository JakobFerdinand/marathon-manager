using Core;
using Logic.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.RunnerManagement.Common;

namespace UI.RunnerManagement.ViewModels
{
    public class CreateRestoreDatabaseViewModel : ViewModelBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConnectionstringService connectionstringService;

        public CreateRestoreDatabaseViewModel(IUnitOfWork unitOfWork, IConnectionstringService connectionstringService)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.connectionstringService = connectionstringService ?? throw new ArgumentNullException(nameof(connectionstringService));

            (Server, Database) = connectionstringService.GetConnectionDetails();

            SaveConnectionDetailsCommand = new Command(SaveConnectionDetails, CanSaveConnectionDetails);
            RefreshServersCommand = new Command(RefreshServers);
            RefreshDatabasesCommand = new Command(RefreshDatabases, CanRefreshDatabases);
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
            => !Server.IsNullOrEmpty()
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
    }
}
