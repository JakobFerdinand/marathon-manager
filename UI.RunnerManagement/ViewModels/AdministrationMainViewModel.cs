using System.Windows.Input;
using UI.RunnerManagement.Common;

namespace UI.RunnerManagement.ViewModels
{
    public class AdministrationMainViewModel : ViewModelBase
    {
        private readonly string _administrationPassword;
        public AdministrationMainViewModel(string administrationPassword)
        {
            _administrationPassword = administrationPassword;

            LoginCommand = new Command(
                execute: () =>
                {
                    IsLoggedin = true;
                    Password = string.Empty;
                },
                canExecute: () => Password == administrationPassword && !IsLoggedin);
            LogoutCommand = new Command(
                execute: () => IsLoggedin = false,
                canExecute: () => IsLoggedin);
        }

        private bool isLoggedin;
        public bool IsLoggedin
        {
            get => isLoggedin;
            set => Set(ref isLoggedin, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }
    }
}
