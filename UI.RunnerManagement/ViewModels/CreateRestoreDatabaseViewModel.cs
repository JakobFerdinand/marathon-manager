using Core;

namespace UI.RunnerManagement.ViewModels
{
    public class CreateRestoreDatabaseViewModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateRestoreDatabaseViewModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }
    }
}
