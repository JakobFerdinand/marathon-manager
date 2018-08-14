using System;
using System.Windows.Input;

namespace UI.RunnerManagement.Common
{
    public class Command : ICommand
    {
        private Action executeHandler { get; }
        private Func<bool> canExecuteHandler { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action execute) => executeHandler = execute ?? throw new ArgumentNullException(nameof(execute), "Execute must not be null!");
        public Command(Action execute, Func<bool> canExecute)
            : this(execute) => canExecuteHandler = canExecute;

        public bool CanExecute(object parameter) => canExecuteHandler == null ? true : canExecuteHandler();
        public void Execute(object parameter) => executeHandler();
    }
    public class Command<T> : ICommand
    {
        private Action<T> executeHandler { get; }
        private Func<T, bool> canExecuteHandler { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action<T> execute) => executeHandler = execute ?? throw new ArgumentNullException(nameof(execute), "Execute must not be null!");
        public Command(Action<T> execute, Func<T, bool> canExecute)
            : this(execute) => canExecuteHandler = canExecute;

        public bool CanExecute(object parameter) => canExecuteHandler == null ? true : canExecuteHandler((T)parameter);
        public void Execute(object parameter) => executeHandler((T)parameter);
    }
}
