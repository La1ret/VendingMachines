using VendingMachines.WPF.Common.Base;
using VendingMachines.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VendingMachines.ViewModels
{
    internal class AuthorizationViewModel
    {
        //ЗДЕСЬ ВООБЩЕ ПУСТО ДОЛЖНО БЫТЬ НО НА ВСЯКИЙ
        #region Подключение сервисов
        
       // private readonly INavigationService _navigationService;
        #endregion

        #region Определение команд

        public ICommand ShowRegisterationCommand { get; }
        public ICommand ShowAuthorizationCommand { get; }
        public ICommand ShowForgotPasswordCommand { get; }
        #endregion

        #region Инициализация

        public AuthorizationViewModel(/*INavigationService navigationService*/)
        {
            //_navigationService = navigationService;
        }
        #endregion

        #region Команда перехода на страницу восстановления пароля

        private void ResizeFrameForCurrentPage(object p)
        {
            //_navigationService.NavigateToPage<PasswordRecovery>();
        }
        #endregion
    }
}
