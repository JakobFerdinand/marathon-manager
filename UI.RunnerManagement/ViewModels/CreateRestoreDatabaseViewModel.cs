using Core;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.RunnerManagement.Common;

namespace UI.RunnerManagement.ViewModels
{
    public class CreateRestoreDatabaseViewModel : ViewModelBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateRestoreDatabaseViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            RefreshServersCommand = new Command(RefreshServers);
            RefreshDatabasesCommand = new Command(RefreshDatabases, CanRefreshDatabases);
        }

        public ObservableCollection<string> AvailableServers { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> AvailableDatabases { get; } = new ObservableCollection<string>();

        private string server;
        public string Server
        {
            get => server;
            set => Set(ref server, value);
        }

        private string database;
        public string Database
        {
            get => database;
            set => Set(ref database, value);
        }

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
