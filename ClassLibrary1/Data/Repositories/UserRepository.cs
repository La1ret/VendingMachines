using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain.IRepository;
using VendingMachines.Domain.Models;
using VendingMachines.Infrastructure.Authentication;
using VendingMachines.Infrastructure.Data;

namespace VendingMachines.Infrastructure.Repositories
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
