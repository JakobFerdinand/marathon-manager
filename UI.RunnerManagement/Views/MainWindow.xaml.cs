using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using UI.RunnerManagement.ViewModels;

namespace UI.RunnerManagement.Views
{
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel.CouldConnectToDatabaseEvent += couldConnect => tabControl.ItemsSource = GetVisibleTabItems(couldConnect).ToList();
        }

        private IEnumerable<UserControl> GetVisibleTabItems(bool couldConnnectToDatabase)
        {
            if (couldConnnectToDatabase)
            {
                yield return new RunnersView();
                yield return new CategoriesView();
            }
            yield return new AdministrationMainView();
        }
    }
}
