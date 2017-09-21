using System;
using System.Windows.Input;

namespace UI.RunnerManagement.Common
{
    public class RelayCommand : ICommand
    {
        private Action executeHandler { get; }
        private Func<bool> canExecuteHandler { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute) => executeHandler = execute ?? throw new ArgumentException("Execute must not be null!");
        public RelayCommand(Action execute, Func<bool> canExecute)
            : this(execute) => canExecuteHandler = canExecute;

        public bool CanExecute(object parameter) => canExecuteHandler == null ? true : canExecuteHandler();
        public void Execute(object parameter) => executeHandler();
    }
    public class RelayCommand<T> : ICommand
    {
        private Action<T> executeHandler { get; }
        private Func<T, bool> canExecuteHandler { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute) => executeHandler = execute ?? throw new ArgumentException("Execute must not be null!");
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
            : this(execute) => canExecuteHandler = canExecute;

        public bool CanExecute(object parameter) => canExecuteHandler == null ? true : canExecuteHandler((T)parameter);
        public void Execute(object parameter) => executeHandler((T)parameter);
    }
}
