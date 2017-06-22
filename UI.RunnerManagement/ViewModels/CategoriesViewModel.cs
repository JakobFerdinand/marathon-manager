using Core;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace UI.RunnerManagement.ViewModels
{
    internal class CategoriesViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Category> _categories;
        private ICommand _initializeCommand;

        public CategoriesViewModel(IUnitOfWork unitOfWork)
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

        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new RelayCommand(LoadCategories));

        internal void LoadCategories()
        {
            Categories = _unitOfWork.Categories.GetAll();
        }
    }
}
