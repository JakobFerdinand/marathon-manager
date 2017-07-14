using System;
using System.Windows.Input;

namespace UI.Shared
{
    public class RelayCommand : ICommand
    {
        private Action _executeHandler { get; }
        private Func<bool> _canExecuteHandler { get; }
        private readonly Action RaiseCanExecuteChangedAction;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute => _canExecuteHandler == null || _canExecuteHandler();

        public RelayCommand(Action execute)
        {
            _executeHandler = execute ?? throw new ArgumentException("Execute must not be null!");
            RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            SimpleCommandManager.AddRaiseAcanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }
        public RelayCommand(Action execute, Func<bool> canExecute)
            : this(execute)
        {
            _canExecuteHandler = canExecute;
        }

        public void RemoveCommand()
        {
            SimpleCommandManager.RemoveRaiseAcanExecuteChangedAction(RaiseCanExecuteChangedAction);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, new EventArgs());
        }

        ~RelayCommand() => RemoveCommand();

        bool ICommand.CanExecute(object parameter) => CanExecute;

        public void Execute(object parameter)
        {
            _executeHandler();
            SimpleCommandManager.RefreshCommandStates();
        }
    }
}
