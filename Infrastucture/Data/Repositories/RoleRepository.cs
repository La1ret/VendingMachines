using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain.IRepository;
using VendingMachines.Domain.Models;
using VendingMachines.Infrastructure.Data;

namespace VendingMachines.Infrastructure.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly VendingMachinesDbContext _context;

        public RoleRepository(VendingMachinesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> GetRoleBySystemNameAsync(string systemName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.SystemName == systemName);
        }

        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Метод для создания предопределенных ролей при первом запуске приложения/миграции.
        ///// </summary>
        public void InitializePredefinedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                    new Role { SystemName = "Admin",
                               DisplayName = "Администратор",
                               Description = "Полный доступ ко всем функциям системы." },

                    new Role { SystemName = "Operator",
                        DisplayName = "Оператор",
                        Description = "Доступ к основным рабочим функциям (обработке заказов/данных)." },

                    new Role { SystemName = "User",
                        DisplayName = "Пользователь",
                        Description = "Стандартный пользователь системы." },

                    new Role { SystemName = "Guest",
                        DisplayName = "Гость",
                        Description = "Минимальные права (просмотр публичной информации)." }
                );
            
        }
    }
}
