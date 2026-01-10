using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Domain;
using VendingMachines.Domain.IRepository;
using VendingMachines.Domain.Models;
using VendingMachines.Domain.Security;
using VendingMachines.Infrastructure.Authentication;
using VendingMachines.Infrastructure.Data.Repositories;

namespace VendingMachines.Infrastructure.Data
{
    public class VendingMachinesDbContext : DbContext
    {
        private readonly IPasswordHasher _passwordHasher;

        // Свойства DbSet для доступа из других частей приложения
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Конструктор, который использует программно созданное соединение
        public VendingMachinesDbContext(
            DbContextOptions<VendingMachinesDbContext> options,
            IPasswordHasher passwordHasher = null) //для поддержки Design-time (миграций)
            : base(options)
        {
            _passwordHasher = passwordHasher;
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
            const int adminRoleId = 1;
            const int operatorRoleId = 2;
            const int userRoleId = 3;
            const int guestRoleId = 4;
            
            const int AdminId = 1;
            const int OperatorId = 2;

            if (_passwordHasher != null)
            {
                modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        RoleId = adminRoleId,
                        SystemName = "Admin",
                        DisplayName = "Администратор",
                        Description = "Полный доступ ко всем функциям системы."
                    },

                    new Role
                    {
                        RoleId = operatorRoleId,
                        SystemName = "Operator",
                        DisplayName = "Оператор",
                        Description = "Доступ к основным рабочим функциям (обработке заказов/данных)."
                    },

                    new Role
                    {
                        RoleId = userRoleId,
                        SystemName = "User",
                        DisplayName = "Пользователь",
                        Description = "Стандартный пользователь системы."
                    },

                    new Role
                    {
                        RoleId = guestRoleId,
                        SystemName = "Guest",
                        DisplayName = "Гость",
                        Description = "Минимальные права (просмотр публичной информации)."
                    }
                );

                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        UserId = AdminId,
                        Username = "Admin",
                        PasswordHash = _passwordHasher.HashPassword("Administrator"),
                        FullName = "Администратор",
                        Email = "Admin@mail.ru",
                        RoleId = adminRoleId,
                        FailedLoginAttempts = 0
                    },

                    new User
                    {
                        UserId = OperatorId,
                        Username = "Operator",
                        PasswordHash = _passwordHasher.HashPassword("Operator"),
                        FullName = "Опреатор",
                        Email = "Operator@yandex.ru",
                        RoleId = operatorRoleId,
                        FailedLoginAttempts = 0
                    }
                );
            }
        }
    }
}