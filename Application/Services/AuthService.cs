using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Application.Common;
using VendingMachines.Application.IServices;
using VendingMachines.Domain.IRepository;
using VendingMachines.Domain.Models;
using VendingMachines.Domain.Security;
using VendingMachines.Shared;
using VendingMachines.Shared.DTOs.User;

namespace VendingMachines.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Объявление сервисов

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserSessionService _userSessionService;
        private readonly JwtOptions _options;
        #endregion

        #region Инициализатор

        public AuthService(IUserRepository userRepository,
                           IRoleRepository roleRepository, 
                           IPasswordHasher passwordHasher,
                           IUserSessionService userSessionService,
                           IJwtTokenGenerator jwtTokenGenerator,
                           IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _userSessionService = userSessionService;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _options = jwtOptions.Value;
        }
        #endregion

        public async Task<OperationResult<UserAuthResponse>> LoginAsync(UserLoginRequest request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null) return OperationResult<UserAuthResponse>.Failure("Пользователь не найден");

            if (user.LockoutEnd > DateTime.UtcNow)
            {
                return OperationResult<UserAuthResponse>.Failure($"Вход в аккаунт временно заблокирован. Попробуйте через {Math.Ceiling((user.LockoutEnd - DateTime.UtcNow).Value.TotalSeconds)} сек.");
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

            var expiryDate = DateTime.UtcNow.AddMinutes(_options.TokenLifetimeInMinutes);
            var token = _jwtTokenGenerator.GenerateToken(user, role.SystemName, expiryDate);
            
            return OperationResult<UserAuthResponse>.Success(new UserAuthResponse
            {
                Token = token,
                ExpiryDate = expiryDate,
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