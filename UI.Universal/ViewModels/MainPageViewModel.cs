using System.Collections.Generic;
using System.Collections.ObjectModel;
using UI.Universal.Interfaces;
using UI.Universal.Models;

namespace UI.Universal.ViewModels
{
    internal class MainPageViewModel  : ViewModelBase
    {
        private readonly IRunnerService _runnerService;

        public MainPageViewModel(IRunnerService runnerService)
        {
            _runnerService = runnerService;
        }

        public ICollection<Runner> Runners { get; } = new ObservableCollection<Runner>();

        public void LoadRunners()
        {
            var runners = _runnerService.GetAll().Result;
            foreach (var r in runners)
                Runners.Add(r);
        }
    }
}
