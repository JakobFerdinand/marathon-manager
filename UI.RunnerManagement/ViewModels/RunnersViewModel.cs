using Core;
using Core.Extensions;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.ViewModels
{
    public class RunnersViewModel : ViewModelBase
    {
        private readonly Func<IUnitOfWork> _getNewUnitOfWork;
        private readonly IDialogService _dialogService;
        private IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private IEnumerable<Runner> _runners;
        private Runner _selectedRunner;
        private ICommand _editCommand;
        private ICommand _currentCellChangedCommand;
        private ICommand _initializeCommand;
        private ICommand _reloadCommand;
        private ICommand _removeRunnerCommand;
        private ICommand _saveCommand;
        private bool _areStartnumbersUnic = true;
        private bool _areChipIdsUnic = true;

        public RunnersViewModel(Func<IUnitOfWork> getNewUnitOfWork, IDialogService dialogService)
        {
            _getNewUnitOfWork = getNewUnitOfWork ?? throw new ArgumentNullException(nameof(getNewUnitOfWork), $"{nameof(getNewUnitOfWork)} must not be null.");
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService), $"{nameof(dialogService)} must not be null.");
            _unitOfWork = _getNewUnitOfWork();
        }

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
            set
            {
                _selectedRunner = value;
                if (_selectedRunner != null)
                {
                    _unitOfWork.Attach(_selectedRunner);
                    _selectedRunner.Category = Categories.Single(c => c.Id == _selectedRunner.Category.Id);
                }
                RaisePropertyChanged();
            }
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
        public ICommand ReloadCommand => _reloadCommand ?? (_reloadCommand = new Command(Reload));
        public ICommand RemoveRunnerCommand => _removeRunnerCommand ?? (_removeRunnerCommand = new Command(RemoveRunner));
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(
            () => SaveRunners(),
            () => AreStartnumbersUnic &&
                  AreChipIdsUnic));

        internal void Reload()
        {
            var messageBoxResult = MessageBoxResult.No;
            if (_unitOfWork.HasChanges())
                messageBoxResult = _dialogService.ShowYesNoMessageBox("Sie haben ungespeicherte Änderungen. wollen sie diese speichern?", "Ungespeicherte Änderungen");
            if (messageBoxResult is MessageBoxResult.Yes)
                _unitOfWork.Complete();

            _unitOfWork.Dispose();
            _unitOfWork = _getNewUnitOfWork();

            Runners = null;
            Categories = null;
            SelectedRunner = null;
            LoadData();
        }
        internal void LoadData()
        {
            Runners = _unitOfWork.Runners.GetAllWithCategories();
            Categories = _unitOfWork.Categories.GetAll(asNoTracking: false);
        }
        internal void LoadRunners()
        {
            Runners = _unitOfWork.Runners.GetAllWithCategories();
            ValidateChipIds();
            NotifySportsClubAndCitiesAndInvalidRunners();
        }
        internal void LoadCategories() => Categories = _unitOfWork.Categories.GetAll(asNoTracking: false);

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
            ValidateChipIds();

            NotifySportsClubAndCitiesAndInvalidRunners();
        }
        internal void NotifySportsClubAndCitiesAndInvalidRunners()
        {
            RaisePropertyChanged(nameof(SportClubs));
            RaisePropertyChanged(nameof(Cities));
            RaisePropertyChanged(nameof(InvalidRunners));
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
