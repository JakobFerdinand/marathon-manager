using System.Windows;

namespace UI.RunnerManagement.Services
{
    public interface IDialogService
    {
        MessageBoxResult ShowYesNoMessageBox(string message, string caption);
    }

    public class DialogService : IDialogService
    {
        public MessageBoxResult ShowYesNoMessageBox(string message, string caption)
            => MessageBox.Show(message, caption, MessageBoxButton.YesNo);
    }
}
