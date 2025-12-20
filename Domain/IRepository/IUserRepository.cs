using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain.Models;

namespace VendingMachines.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);

        // Метод для проверки учетных данных
        Task<User> AuthenticateAsync(string username, string passwordHash);
        //void InitializePredefinedUsers(ModelBuilder modelBuilder);
    }
}
