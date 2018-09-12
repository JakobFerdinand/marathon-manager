using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace UI.RunnerManagement.Behaviors
{
    public class ScrollToSelectedItemBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
            => AssociatedObject.SelectionChanged += SelectionChangedHandler;

        protected override void OnDetaching()
            => AssociatedObject.SelectionChanged -= SelectionChangedHandler;

        private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
                dataGrid.Dispatcher.Invoke(() =>
                {
                    dataGrid.UpdateLayout();
                    dataGrid.ScrollIntoView(dataGrid.SelectedItem);
                });
        }
    }
}
