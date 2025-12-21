using VendingMachines.WPF.Common.Base;
using VendingMachines.WPF.Services.IServices;
using VendingMachines.WPF.Services;
using VendingMachines.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VendingMachines.ViewModels
{
    public class PasswordRecoveryViewModel : ViewModelBase
    {
        #region Объявление сервисов

        private readonly INavigationService _navigationService;
        private readonly IApiAuthService _authService;
        #endregion

        #region Объявление команд

        public ICommand BackCommand { get; }
        public ICommand RecoveryPasswordCommand { get; }
        //public ICommand ChangePasswordVisibility { get; }
        #endregion

        #region Поля

        private string _username = "";
        private string _email = "";
        private string _recoveryStatusMessage = "";
        #endregion

        #region Инициализация

        public PasswordRecoveryViewModel(INavigationService navigationService, IApiAuthService authService/*, IUserService userService, */)
        {
            _navigationService = navigationService;
            _authService = authService;

            BackCommand = new RelayCommand(OnBackCommandExecute);
            RecoveryPasswordCommand = new RelayCommand(OnRecoveryPasswordCommandExecute, CanRecoveryPasswordCommandExecute);
            //ChangePasswordVisibility = new RelayCommand(OnChangePasswordVisibilityExecute, CanChangePasswordVisibilityExecute);
        }
        #endregion

        #region Свойства

        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public string RecoveryStatusMessage
        {
            get => _recoveryStatusMessage;
            set => Set(ref _recoveryStatusMessage, value);
        }
        #endregion

        #region Команда сброса пароля

        private async void OnRecoveryPasswordCommandExecute(object p)
        {
            RecoveryStatusMessage = "";
            await Task.Delay(150); //Для заметности отображения смены сообщения

            var result = await _authService.RequestPasswordRecoveryAsync(_username, _email);
            if (result.IsSuccess)
            {
                RecoveryStatusMessage = "Письмо отправлено ожидайте (повторная отправка только через 1 минуту).";
                await Task.Delay(60 * 1000);
                RecoveryStatusMessage = "Возможна повторная отправка.";
            }
            else
            {
                RecoveryStatusMessage = result.Message;
            }

            await Task.Delay(15 * 1000); //Затирка сообщения через 15 cекунд
            RecoveryStatusMessage = "";
        }

        private bool CanRecoveryPasswordCommandExecute(object p)
        {
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_email) 
                && RecoveryStatusMessage != "Письмо отправлено ожидайте (повторная отправка только через 1 минуту).";
        }
        #endregion

        #region Команда назад

        private void OnBackCommandExecute(object p)
        {
            _navigationService.NavigateToPage<AuthPage>();
        }
        #endregion
    }
}
