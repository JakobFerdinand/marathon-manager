using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using UI.RunnerManagement.Validation;

namespace UI.RunnerManagement.ViewModels
{
    public class RunnersViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private IEnumerable<Runner> _runners;
        private ICommand _editCommand;
        private ICommand _initializeCommand;
        private ICommand _saveCommand;

        public RunnersViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), $"{nameof(unitOfWork)} must not be null.");
        }

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

        public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand<Runner>(EditRunner));
        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new RelayCommand(() =>
        {
            LoadRunners();
            LoadCategories();
        }));
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<DependencyObject>(
            _ =>SaveRunners(),
            grid => Validator.IsValid(grid)));

        internal void LoadRunners()
        {
            Runners = _unitOfWork.Runners.GetAll();
        }
        internal void LoadCategories()
        {
            Categories = _unitOfWork.Categories.GetAll();
        }
        internal void SaveRunners()
        {
            _unitOfWork.Complete();
        }
        internal void EditRunner(Runner selectedRunner)
        {
            if (selectedRunner.Id == 0)
                _unitOfWork.Runners.Add(selectedRunner);
        }
    }
}
