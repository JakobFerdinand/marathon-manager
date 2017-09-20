using StructureMap;
using UI.Universal.Extensions;
using UI.Universal.Registries;
using UI.Universal.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UI.Universal.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Container _container;
        internal MainPageViewModel ViewModel => _container.GetInstance<MainPageViewModel>();

        public MainPage()
        {
            InitializeComponent();

            _container = new Container();
            ConfigureServices();
        }

        public void ConfigureServices()
        {
            _container.Configure(c =>
            {
                c.AddRegistry(new CommonRegistry());
            });
            _container.RegisterConcreteTypeAsSingelton<MainPageViewModel>();
        }
    }
}
