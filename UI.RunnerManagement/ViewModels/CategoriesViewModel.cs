using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using UI.RunnerManagement.Common;

namespace UI.RunnerManagement.ViewModels
{
    internal class CategoriesViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private ICommand _initializeCommand;

        private Timer _timer;

        public CategoriesViewModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), $"{nameof(unitOfWork)} must not be null.");

        public IEnumerable<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }

        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new RelayCommand(
            () =>
            {
                LoadCategories();
                InitializeTimer();
            }));

        internal void LoadCategories() => Categories = _unitOfWork.Categories.GetAll(asNotTracking: true);
        internal void InitializeTimer() => _timer = new Timer(_ => LoadCategories(), null, dueTime: 0, period: 10000);
    }
}
