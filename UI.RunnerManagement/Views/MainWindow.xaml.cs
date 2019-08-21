using MahApps.Metro.Controls;
using System;
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
            ViewModel.CouldConnectToDatabaseEvent += couldConnect => tabControl
                .Items
                .AddRange(GetVisibleTabItems(couldConnect).ToTabItems());
        }

        private IEnumerable<(UserControl control, string header)> GetVisibleTabItems(bool couldConnnectToDatabase)
        {
            if (couldConnnectToDatabase)
            {
                yield return (new RunnersView(), "Läufer");
                yield return (new CategoriesView(), "Kategorien");
            }
            yield return (new AdministrationMainView(), "Administration");
        }
    }

    internal static partial class Extensions
    {
        public static IEnumerable<TabItem> ToTabItems(this IEnumerable<(UserControl control, string header)> @this)
            => @this
            .Select(i => new TabItem { Header = i.header, Content = i.control });
    }
}
