using Core;
using Core.Models;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using System.Collections.Immutable;

namespace UI.RunnerManagement.ViewModels
{
    public class RunnersViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private IEnumerable<Runner> _runners;
        private Runner _selectedRunner;
        private ICommand _editCommand;
        private ICommand _currentCellChangedCommand;
        private ICommand _initializeCommand;
        private ICommand _removeRunnerCommand;
        private ICommand _saveCommand;
        private bool _areStartnumbersUnic = true;
        private bool _areChipIdsUnic = true;

        public RunnersViewModel(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), $"{nameof(unitOfWork)} must not be null.");

        public IEnumerable<Category> Categories
        {
            get => _categories;
            set => Set(ref _categories, value);
        }
        public IEnumerable<Runner> Runners
        {
            get => _runners;
            set => Set(ref _runners, value);
        }
        public Runner SelectedRunner
        {
            get => _selectedRunner;
            set => Set(ref _selectedRunner, value);
        }
        public ImmutableList<string> SportClubs =>
            Runners?.Where(r => r.SportsClub != null)
                    .Select(r => r.SportsClub)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToImmutableList()
            ?? ImmutableList<string>.Empty;

        public ImmutableList<string> Cities => 
            Runners?.Where(r => r.City != null)
                    .Select(r => r.City)
                    .Distinct()
                    .OrderBy(r => r)
                    .ToImmutableList()
            ?? ImmutableList<string>.Empty;
        public bool AreStartnumbersUnic
        {
            get => _areStartnumbersUnic;
            set => Set(ref _areStartnumbersUnic, value);
        }
        public bool AreChipIdsUnic
        {
            get => _areChipIdsUnic;
            set => Set(ref _areChipIdsUnic, value);
        }
        public ImmutableList<Runner> InvalidRunners => 
            Runners?.Where(r => string.IsNullOrWhiteSpace(r.Firstname) 
                || string.IsNullOrWhiteSpace(r.Lastname)
                || (r.CategoryId == 0 && r.Category == null)).ToImmutableList()
            ?? ImmutableList<Runner>.Empty;

        public ICommand EditCommand => _editCommand ?? (_editCommand = new Command<Runner>(EditRunner));
        public ICommand CurrentCellChangedCommand => _currentCellChangedCommand ?? (_currentCellChangedCommand = new Command(CurrentCellChanged));
        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new Command(() =>
        {
            LoadCategories();
            LoadRunners();
        }));
        public ICommand RemoveRunnerCommand => _removeRunnerCommand ?? (_removeRunnerCommand = new Command(RemoveRunner));
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(
            () => SaveRunners(),
            () => AreStartnumbersUnic && 
                  AreChipIdsUnic &&
                  !InvalidRunners.Any()));

        internal void LoadRunners()
        {
            Runners = _unitOfWork.Runners.GetAll();
            //ValidateStartnumbers();
            ValidateChipIds();
        }
        internal void LoadCategories() => Categories = _unitOfWork.Categories.GetAll(asNotTracking: false);
        internal void SaveRunners() => _unitOfWork.Complete();
        internal void EditRunner(Runner selectedRunner)
        {
            if (selectedRunner.Id == 0)
                _unitOfWork.Runners.Add(selectedRunner);

            //ValidateStartnumbers();
            ValidateChipIds();
        }
        internal void RemoveRunner()
        {
            _unitOfWork.Runners.Remove(SelectedRunner);
            SelectedRunner = null;
            Runners = _unitOfWork.Runners.GetAll();
        }
        internal void CurrentCellChanged()
        {
            //ValidateStartnumbers();
            ValidateChipIds();

            NotifySportsClubAndCitiesAndInvalidRunners();
        }
        internal void NotifySportsClubAndCitiesAndInvalidRunners()
        {
            RaisePropertyChanged(nameof(SportClubs));
            RaisePropertyChanged(nameof(Cities));
            RaisePropertyChanged(nameof(InvalidRunners));
        }
        internal void ValidateStartnumbers()
        {
            if (Runners is null)
                return;

            var startNumbers = Runners
                .Where(r => r.Startnumber.HasValue && r.Startnumber != 0)
                .Select(r => r.Startnumber.Value);

            AreStartnumbersUnic = !startNumbers.ConaintsEqual();
        }
        internal void ValidateChipIds()
        {
            if (Runners is null)
                return;

            var chipIds = Runners
                .Where(r => r.ChipId != null)
                .Select(r => r.ChipId);

            AreChipIdsUnic = !chipIds.ConaintsEqual();
        }
    }
}
