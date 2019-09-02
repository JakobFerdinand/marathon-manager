using Core;
using Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using UI.RunnerManagement.Common;
using UI.RunnerManagement.Services;

namespace UI.RunnerManagement.ViewModels
{
    public class AddAndChangeCategoriesViewModel : ViewModelBase
    {
        private readonly Func<IUnitOfWork> _getNewUnitOfWork;
        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;
        private IUnitOfWork _unitOfWork;

        public AddAndChangeCategoriesViewModel(Func<IUnitOfWork> getNewUnitOfWork, IUnitOfWork unitOfWork, IDialogService dialogService, INotificationService notificationService)
        {
            _getNewUnitOfWork = getNewUnitOfWork ?? throw new ArgumentNullException(nameof(getNewUnitOfWork));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));

            InitializeCommand = new Command(LoadData);
            ReloadCategoriesCommand = new Command(ReloadCategoriesCommandHandler);
            NewCategoryCommand = new Command(NewCategoryCommandHandler);
            SaveCommand = new Command(SaveCommandHandler);
            RemoveRunnerCommand = new Command(RemoveRunnerCommandHandler);
        }

        public ICommand InitializeCommand { get; }
        public ICommand ReloadCategoriesCommand { get; }
        public ICommand NewCategoryCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand RemoveRunnerCommand { get; }
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                if (selectedCategory != null && selectedCategory.Id > 0)
                    _unitOfWork.Attach(selectedCategory);
                RaisePropertyChanged();
            }
        }

        private void LoadData()
        {
            try
            {
                SelectedCategory = null;
                var categories = _unitOfWork.Categories.GetAll(asNoTracking: true);
                Categories.Clear();
                Categories.AddRange(categories);
            }
            catch
            { }
        }

        private void ReloadCategoriesCommandHandler()
        {
            var messageBoxResult = MessageBoxResult.No;
            if (_unitOfWork.HasChanges())
                messageBoxResult = _dialogService.ShowYesNoMessageBox("Sie haben ungespeicherte Änderungen. wollen sie diese speichern?", "Ungespeicherte Änderungen");
            if (messageBoxResult is MessageBoxResult.Yes)
                _unitOfWork.Complete();

            _unitOfWork.Dispose();
            _unitOfWork = _getNewUnitOfWork();

            LoadData();
        }

        private void NewCategoryCommandHandler()
        {
            var category = new Category();
            _unitOfWork.Categories.Add(category);
            Categories.Add(category);
            SelectedCategory = category;
        }

        private void SaveCommandHandler()
        {
            SaveCategories();
            _notificationService.ShowNotification("Es wurde erfolgreich gespeichert.", "Gespeichert", NotificationType.Success);
        }

        private void SaveCategories()
            => _unitOfWork.Complete();

        private void RemoveRunnerCommandHandler()
        {
            SaveCategories();

            var result = _dialogService.ShowYesNoMessageBox($"Wollen sie die Kategorie {selectedCategory.Name} wirklich löschen?", "Kategorie löschen");

            switch (result)
            {
                case MessageBoxResult.No: return;

                case MessageBoxResult.Yes:
                    _unitOfWork.Categories.Remove(selectedCategory);
                    selectedCategory = null;
                    SaveCategories();
                    ReloadCategoriesCommandHandler();
                    break;
            }
        }
    }
}
