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
            this.unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            RefreshServersCommand = new Command(RefreshServers);
        }

        public ObservableCollection<string> AvailableServers { get; } = new ObservableCollection<string>();

        private string server;
        public string Server
        {
            get => server;
            set => Set(ref server, value);
        }

        public ICommand RefreshServersCommand { get; }
        private void RefreshServers()
        {
            AvailableServers.Clear();
            var servers = unitOfWork.Database.GetAvailableServers();
            AvailableServers.AddRange(servers);
        }
    }
}
