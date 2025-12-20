using VendingMachines.Common;
using VendingMachines.Data.Interfaces;
using VendingMachines.Data.Repositories;
using VendingMachines.Models;
using VendingMachines.Services.Interfaces;
using VendingMachines.Views.Pages;
using VendingMachines.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VendingMachines.Services
{
    public class AuthService : IAuthentificationService
    {
        #region Объявление сервисов и контекста

        private readonly INavigationService _navigationService;

        private readonly IUserRepository _userRepository;

        private readonly IUserSessionService _userSessionService;

        private readonly IRoleRepository _roleRepository;
        #endregion

        #region Инициализатор

        public AuthService( IUserRepository userRepository,
                                        INavigationService navigationService, 
                                        IUserSessionService userSessionService,
                                        IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
            _userSessionService = userSessionService;
            _roleRepository = roleRepository;
        }
        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Переменные

        private bool isWillLocked;
        private int _failedLoginAttempts = 0;
        #endregion

        #region Свойство блокировки

        private bool _isLocked = false;

        public bool IsLocked
        {
            get => _isLocked;
            private set { _isLocked = value; OnPropertyChanged(); }
        }
        #endregion

        #region Методы блокировки окна

        private async Task LockWindowAsync(int seconds)
        {
            IsLocked = true;
            await Task.Delay(seconds * 1000);
            IsLocked = false;
            _failedLoginAttempts = 0;
        }

        private void IncrementFailedAttempts()
        {
            _failedLoginAttempts++;

            if (_failedLoginAttempts >= 3)
            {
                _ = LockWindowAsync(15);
            }
        }
        #endregion 

        public async Task<OperationResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.AuthenticateAsync(username, password);
            if (user == null)
            {
                IncrementFailedAttempts();
                return OperationResult.Failure("Неверный логин или пароль.");
            }

            _userSessionService.CurrentUser = user;
            _navigationService.ChangeWindowTo<MainWindow>();
            return OperationResult.Success();
        }

        public async Task<OperationResult> RegisterAsync(string fullName, string email, string username, string password)
        {
            var nameParts = fullName.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            if (nameParts < 2 || nameParts > 3)
            {
                return OperationResult.Failure("Вы некорректно ввели ФИО. Регистрация не возможна.");
            }

            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return OperationResult.Failure("Пользователь с таким логином уже существует. Выберите другой.");
            }

            var role = await _roleRepository.GetRoleBySystemNameAsync("User");
            if (role == null)
            {
                return OperationResult.Failure("Ошибка системы: роль 'Пользователь' не найдена. Обратитесь к администратору.");
            }

            var passwordHash = PasswordHasher.HashPassword(password);

            User user = new User
            {
                FullName = fullName,
                Email = email,
                Username = username,
                PasswordHash = passwordHash,
                RoleId = role.RoleId
            };

            await _userRepository.AddUserAsync(user);

            _userSessionService.CurrentUser = user;
            _navigationService.ChangeWindowTo<MainWindow>();

            return OperationResult.Success();
        }

        public async Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email) 
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            
            if (user == null)
            { 
                return OperationResult.Failure("Данный пользователь не зарегистрирован в системе.");
            }

            if (user.Email != email) 
            {
                return OperationResult.Failure("Данный пользователь зарегистрирован на другую почту.");
            }


            return OperationResult.Success();
        }
    }
}
