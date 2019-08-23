using Core;
using Core.EventAggregation;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Events;

namespace UI.RunnerManagement.ViewModels
{
    internal class CategoriesViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IEnumerable<Category> _categories;
        private ICommand _initializeCommand;

        private readonly Timer _timer = new Timer(10000);

        public CategoriesViewModel(IUnitOfWork unitOfWork, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), $"{nameof(unitOfWork)} must not be null.");

            _timer.Elapsed += (s, e) => LoadCategories();

            eventAggregator.GetEvent<EnsureDatabaseDeletingEvent>()
                .Subscribe(() => _timer.Stop());
            eventAggregator.GetEvent<EnsureDatabaseCreatedEvent>()
                .Subscribe(() => _timer.Start());
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

        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new Command(
            () =>
            {
                LoadCategories();
                InitializeTimer();
            }));

        internal void LoadCategories() => Categories = _unitOfWork.Categories.GetAll(asNoTracking: true);
        internal void InitializeTimer() => _timer.Start();
    }
}
