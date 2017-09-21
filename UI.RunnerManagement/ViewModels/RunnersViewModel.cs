using Core;
using Core.Models;
using Logic.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace UI.RunnerManagement.ViewModels
{
    public class RunnersViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private IEnumerable<Runner> _runners;
        private ICommand _editCommand;
        private ICommand _currentCellChangedCommand;
        private ICommand _initializeCommand;
        private ICommand _saveCommand;
        private bool _areStartnumbersUnic = true;
        private bool _areChipIdsUnic = true;

        public RunnersViewModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), $"{nameof(unitOfWork)} must not be null.");

        public IEnumerable<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Runner> Runners
        {
            get => _runners;
            set
            {
                _runners = value;
                RaisePropertyChanged();
            }
        }
        public List<string> SportClubs => 
            Runners
            .Where(r => r.SportsClub != null)
            .Select(r => r.SportsClub)
            .Distinct()
            .OrderBy(s => s)
            .ToList();
        public List<string> Cities => 
            Runners
            .Where(r => r.City != null)
            .Select(r => r.City)
            .Distinct()
            .OrderBy(r => r)
            .ToList();
        public bool AreStartnumbersUnic
        {
            get => _areStartnumbersUnic;
            set
            {
                _areStartnumbersUnic = value;
                RaisePropertyChanged();
            }
        }
        public bool AreChipIdsUnic
        {
            get => _areChipIdsUnic;
            set
            {
                _areChipIdsUnic = value;
                RaisePropertyChanged();
            }
        }

        public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand<Runner>(EditRunner));
        public ICommand CurrentCellChangedCommand => _currentCellChangedCommand ?? (_currentCellChangedCommand = new RelayCommand(CurrentCellChanged));
        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new RelayCommand(() =>
        {
            LoadCategories();
            LoadRunners();
        }));
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(
            () => SaveRunners(),
            () => AreStartnumbersUnic && AreChipIdsUnic));

        internal void LoadRunners()
        {
            Runners = _unitOfWork.Runners.GetAll();
            ValidateStartnumbers();
            ValidateChipIds();
        }
        internal void LoadCategories() => Categories = _unitOfWork.Categories.GetAll(asNotTracking: false);
        internal void SaveRunners() => _unitOfWork.Complete();
        internal void EditRunner(Runner selectedRunner)
        {
            if (selectedRunner.Id == 0)
                _unitOfWork.Runners.Add(selectedRunner);

            ValidateStartnumbers();
            ValidateChipIds();
        }
        internal void CurrentCellChanged()
        {
            ValidateStartnumbers();
            ValidateChipIds();

            NotifySportsClubAndCities();
        }
        internal void NotifySportsClubAndCities()
        {
            RaisePropertyChanged(nameof(SportClubs));
            RaisePropertyChanged(nameof(Cities));
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
