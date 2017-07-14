using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UI.Shared
{
    internal static class SimpleCommandManager
    {
        private static List<Action> _raiseCanExecuteChangedActions = new List<Action>();

        public static void AddRaiseAcanExecuteChangedAction(ref Action raiseCanExecuteChanged)
        {
            _raiseCanExecuteChangedActions.Add(raiseCanExecuteChanged);
        }

        public static void RemoveRaiseAcanExecuteChangedAction(Action raiseCanExecuteChanged)
        {
            _raiseCanExecuteChangedActions.Add(raiseCanExecuteChanged);
        }

        public static void AssignOnPropertyChanged(ref PropertyChangedEventHandler propertyEventHandler)
        {
            propertyEventHandler += OnPropertyChanged;
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // this if clause is to prevent an infinity loop
            if (e.PropertyName != "CanExecute")
                RefreshCommandStates();
        }

        public static void RefreshCommandStates()
        {
            foreach (var raiseCanExecuteChangedAction in _raiseCanExecuteChangedActions)
                raiseCanExecuteChangedAction?.Invoke();
        }
    }
}
