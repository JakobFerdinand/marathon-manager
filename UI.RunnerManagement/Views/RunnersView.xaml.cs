using System;
using System.Windows.Controls;

namespace UI.RunnerManagement.Views
{
    /// <summary>
    /// Interaction logic for RunnersView.xaml
    /// </summary>
    public partial class RunnersView : UserControl
    {
        public RunnersView() => InitializeComponent();

        private void runnerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startnumberTextBox.Focus();
            if (!startnumberTextBox.Text.IsNullOrEmpty())
                startnumberTextBox.SelectAll();
        }
    }
}
