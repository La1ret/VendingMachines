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
using VendingMachines.Domain.Security;

namespace VendingMachines.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Объявление сервисов и контекста

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserSessionService _userSessionService;
        #endregion

        #region Инициализатор

        public AuthService(IUserRepository userRepository,
                           IRoleRepository roleRepository, 
                           IPasswordHasher passwordHasher,
                           IUserSessionService userSessionService)
        {
            _userRepository = userRepository;
            _userSessionService = userSessionService;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
        }
        #endregion

        //HERE
        public async Task<OperationResult<UserAuthResponse>> AuthenticateAsync(UserSignInRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null) return OperationResult<UserAuthResponse>.Failure("Пользователь не найден");

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                return OperationResult<UserAuthResponse>.Failure($"Аккаунт заблокирован. Попробуйте через {Math.Ceiling((user.LockoutEnd - DateTime.UtcNow).Value.TotalMinutes)} сек.");
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);

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

            var role = await _roleRepository.GetRoleByIdAsync(user.RoleId);
            if (role == null) return OperationResult<UserAuthResponse>.Failure("Ошибка сервера: права доступа пользователя не найдены");

            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            await _userRepository.UpdateUserAsync(user);
            
            //Переписать
            return OperationResult<UserAuthResponse>.Success(new UserAuthResponse
            {
                Token = "FAKE_TOKEN",// ВОТ ЭТО
                FullName = user.FullName,
                RoleSystemName = role.SystemName
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

            var passwordHash = _passwordHasher.HashPassword(password); //THIS

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