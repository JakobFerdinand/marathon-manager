using Core;
using Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.RunnerManagement.Common;

namespace UI.RunnerManagement.ViewModels
{
    public class AddAndChangeCategoriesViewModel : ViewModelBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AddAndChangeCategoriesViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            ReloadCategoriesCommand = new Command(ReloadCategoriesCommandHandler);
        }

        public ICommand ReloadCategoriesCommand { get; }
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                if (selectedCategory != null)
                    unitOfWork.Attach(selectedCategory);
                RaisePropertyChanged();
            }
        }

        private void ReloadCategoriesCommandHandler()
        {
            SelectedCategory = null;
            var categories = unitOfWork.Categories.GetAll(asNoTracking: true);
            Categories.Clear();
            Categories.AddRange(categories);
        }
    }
}
