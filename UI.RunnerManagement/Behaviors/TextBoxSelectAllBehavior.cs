using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;
using UI.RunnerManagement.Helpers;

namespace UI.RunnerManagement.Behaviors
{
    public class TextBoxSelectAllBehavior : Behavior<TextBoxBase>
    {
        internal static bool _preventSelectAll;

        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(FrameworkElement.PreviewMouseDownEvent, new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
            AssociatedObject.GotKeyboardFocus += SelectAllText;
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox
                && !textBox.IsKeyboardFocusWithin
                && e.OriginalSource.GetType().Name is "TextBoxView")
            {
                //Wir setzen hier das PreventSelectAll-Flag, sodass beim nächten FocusGotten-Handler dieses Flag true ist, u. dadurch das SelectAll verhindert wird.
                //Anschließend wird das Flag wieder zurückgesetzt -> Asynchron mit Priorität ContextIdle
                _preventSelectAll = true;
                textBox.Dispatcher.BeginInvoke((Action)(() => _preventSelectAll = false), DispatcherPriority.ContextIdle);
            }
        }

        private void SelectAllText(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;

            if (_forceSelectAll
                || (!_preventSelectAll
                    && e.OldFocus != null
                    && !ViewHelper.IsWithin(AssociatedObject, (DependencyObject)e.OldFocus)
                        || e.OldFocus is Window /*Ausnahme: wenn ein Fenster geöffnet wird */))
                textBox?.SelectAll();
        }

        internal static bool _forceSelectAll { get; private set; }
        internal static void ForceSelectAll(Action setFocus)
        {
            _forceSelectAll = true;
            setFocus();
            _forceSelectAll = false;
        }
    }
}
