using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain;
using VendingMachines.Domain.Models;
using VendingMachines.Infrastructure.Authentication;
using VendingMachines.Infrastructure.Data.Repositories;

namespace VendingMachines.Infrastructure.Data
{
    public class VendingMachinesDbContext : DbContext
    {
        // Свойства DbSet делаем публичными для доступа из других частей приложения
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Конструктор, который использует программно созданное соединение
        public VendingMachinesDbContext(DbContextOptions<VendingMachinesDbContext> options)
              : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=VendingMachines.db";
                optionsBuilder.UseSqlite(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitializePredefinedRoles(modelBuilder);
            InitializePredefinedUsers(modelBuilder);
        }

        public void InitializePredefinedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    SystemName = "Admin",
                    DisplayName = "Администратор",
                    Description = "Полный доступ ко всем функциям системы."
                },

                new Role
                {
                    SystemName = "Operator",
                    DisplayName = "Оператор",
                    Description = "Доступ к основным рабочим функциям (обработке заказов/данных)."
                },

                new Role
                {
                    SystemName = "User",
                    DisplayName = "Пользователь",
                    Description = "Стандартный пользователь системы."
                },

                new Role
                {
                    SystemName = "Guest",
                    DisplayName = "Гость",
                    Description = "Минимальные права (просмотр публичной информации)."
                }
            );

        }
        public void InitializePredefinedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new User
                {
                    Username = "Admin",
                    PasswordHash = PasswordHasher.HashPassword("1"),
                    FullName = "Майская Мирослава Андреевна",
                    Email = "may@mail.ru",
                    RoleId = 1
                },

                new User
                {
                    Username = "Manager",
                    PasswordHash = PasswordHasher.HashPassword("123"),
                    FullName = "Пахомов Ярослав Константинович",
                    Email = "Pahomov@yandex.ru",
                    RoleId = 2
                }
            );
        }
    }
}
