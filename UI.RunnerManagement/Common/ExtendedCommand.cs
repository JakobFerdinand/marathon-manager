using System;

namespace UI.RunnerManagement.Common
{
    public class ExtendedCommand : Command
    {
        private readonly Func<string> getTooltip;
        private readonly Func<string> getCannotExecuteTooltip;

        public ExtendedCommand(Action execute, Func<string> getTooltip)
            : base(execute)
            => this.getTooltip = getTooltip ?? throw new ArgumentNullException(nameof(getTooltip));

        public ExtendedCommand(Action execute, Func<string> getTooltip, Func<bool> canExecute, Func<string> getCannotExecuteTooltip) : base(execute, canExecute)
        {
            this.getTooltip = getTooltip ?? throw new ArgumentNullException(nameof(getTooltip));
            this.getCannotExecuteTooltip = getCannotExecuteTooltip ?? throw new ArgumentNullException(nameof(getCannotExecuteTooltip));
        }

        public string Tooltip => CanExecute(null)
            ? getTooltip()
            : getCannotExecuteTooltip?.Invoke() ?? getTooltip();
    }
}
