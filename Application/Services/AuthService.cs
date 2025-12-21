using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Application.IServices;
using VendingMachines.Domain.IRepository;
using VendingMachines.Domain.Models;
using VendingMachines.Shared;
using VendingMachines.Shared.DTOs.User;
using VendingMachines.Application.Common;

namespace VendingMachines.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Объявление сервисов и контекста

        private readonly IUserRepository _userRepository;

        private readonly IUserSessionService _userSessionService;

        private readonly IRoleRepository _roleRepository;
        #endregion

        // HERE
        #region Инициализатор

        public AuthService( IUserRepository userRepository, //THIS
                                        IUserSessionService userSessionService,
                                        IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userSessionService = userSessionService;
            _roleRepository = roleRepository;
        }
        #endregion

        public async Task<OperationResult<UserAuthResponse>> AuthenticateAsync(UserLoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null) return OperationResult<UserAuthResponse>.Failure("Пользователь не найден");

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                return OperationResult<UserAuthResponse>.Failure($"Аккаунт заблокирован. Попробуйте через {Math.Ceiling((user.LockoutEnd - DateTime.UtcNow).Value.TotalMinutes)} сек.");
            }

            bool isPasswordValid = PasswordHasher.VerifyPassword(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 3)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddSeconds(15); 
                    user.FailedLoginAttempts = 0;
                }

                await _userRepository.UpdateUserAsync(user);
                return OperationResult<UserAuthResponse>.Failure("Неверный пароль");
            }

            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            await _userRepository.UpdateUserAsync(user);

            //Переписать
            return OperationResult<UserAuthResponse>.Success(new UserAuthResponse  
            {
                Token = "FAKE_TOKEN",// ВОТ ЭТО
                Username = user.FullName
            }); 
        }

        //HERE
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

            var passwordHash = PasswordHasher.HashPassword(password); //THIS

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
