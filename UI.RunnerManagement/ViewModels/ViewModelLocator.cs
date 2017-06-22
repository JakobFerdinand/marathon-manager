using Logic.DIConfiguration;

namespace UI.RunnerManagement.ViewModels
{
    internal class ViewModelLocator
    {
        private readonly IContainer _container;

        public CategoriesViewModel CategoriesViewModel => _container.GetInstance<CategoriesViewModel>();
        public MainWindowViewModel MainWindowViewModel => _container.GetInstance<MainWindowViewModel>();
        public RunnersViewModel RunnersViewModel => _container.GetInstance<RunnersViewModel>();

        public ViewModelLocator()
        {
            _container = new Container();
            _container.RegisterConcreteTypeAsSingelton<MainWindowViewModel>();
            _container.RegisterConcreteTypeAsSingelton<RunnersViewModel>();
        }
    }
}
