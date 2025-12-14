using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Data.Interfaces;
using VendingMachines.Models;
using VendingMachines.Common;

namespace VendingMachines.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VendingMachinesDbContext _context;

        public UserRepository(VendingMachinesDbContext context)
        {
            _context = context;
        }

        // Включаем навигационное свойство Role при запросе пользователей
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Метод для создания предопределенных ролей при первом запуске приложения/миграции.
        /// </summary>
        //public void InitializePredefinedUsers()
        //{
        //    if (!_context.Users.Any())
        //    {
        //        var users = new List<User>
        //        {
        //            new User { Username = "Admin",
        //                       PasswordHash = PasswordHasher.HashPassword("1"),
        //                       FullName = "Майская Мирослава Андреевна",
        //                       Email = "may@mail.ru",
        //                       RoleId = 1},

        //            new User { Username = "Manager",
        //                       PasswordHash = PasswordHasher.HashPassword("123"),
        //                       FullName = "Пахомов Ярослав Константинович",
        //                       Email = "Pahomov@yandex.ru",
        //                       RoleId = 2},

        //            new User { Username = "User",
        //                       PasswordHash = PasswordHasher.HashPassword("123"),
        //                       FullName = "Чундышко Адам Юнусович",
        //                       Email = "CHAU@google.com",
        //                       RoleId = 3},

        //            new User { Username = "guest",
        //                       PasswordHash = PasswordHasher.HashPassword("123"),
        //                       FullName = "Леонова Дарья Дмитриевна",
        //                       Email = "Lion@ya.ru",
        //                       RoleId = 4}
        //        };

        //        _context.Users.AddRange(users);
        //        _context.SaveChanges();
        //    }
        //}

        public async Task<User> AuthenticateAsync(string username, string providedPassword)
        {
            var user = await _context.Users
                                     .Include(u => u.Role)
                                     .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = PasswordHasher.VerifyPassword(providedPassword, user.PasswordHash);

            if (isPasswordValid)
            {
                return user; 
            }
            else
            {
                return null; 
            }
        }
    }
}
