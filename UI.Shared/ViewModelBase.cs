using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;

namespace UI.Shared
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private List<RelayCommand> _commands = new List<RelayCommand>();

        public ViewModelBase()
        {
            DispatcherObject = CoreWindow.GetForCurrentThread().Dispatcher;
            SimpleCommandManager.AssignOnPropertyChanged(ref PropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual CoreDispatcher DispatcherObject { get; protected set; }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected RelayCommand CreateCommand(Action execute) => CreateCommand(execute, null);

        protected RelayCommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            var command = new RelayCommand(execute, canExecute);
            if (_commands.Contains(command))
                return _commands[_commands.IndexOf(command)];

            _commands.Add(command);
            return command;
        }

        public void RemoveCommands()
        {

        }
    }
}
