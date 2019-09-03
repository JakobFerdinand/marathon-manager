using System;
using System.Windows.Controls;

namespace UI.RunnerManagement.Views
{
    /// <summary>
    /// Interaction logic for AddAndChangeCategoriesView.xaml
    /// </summary>
    public partial class AddAndChangeCategoriesView : UserControl
    {
        public AddAndChangeCategoriesView() => InitializeComponent();

        private void categoriesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nameTextBox.Focus();
            if (!nameTextBox.Text.IsNullOrEmpty())
                nameTextBox.SelectAll();
        }
    }
}
