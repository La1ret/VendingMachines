using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Common;
using VendingMachines.Data;
using VendingMachines.Data.Interfaces;
using VendingMachines.Models;


namespace VendingMachines.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VendingMachinesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(VendingMachines.Data.VendingMachinesDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                     r => r.SystemName,
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

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var adminRoleId = context.Roles.Single(r => r.SystemName == "Admin").RoleId;
                var operatorRoleId = context.Roles.Single(r => r.SystemName == "Operator").RoleId;
                var guestRoleId = context.Roles.Single(r => r.SystemName == "Guest").RoleId;

                context.Users.AddOrUpdate(
                    u => u.Username,
                    new User { Username = "Admin",
                               PasswordHash = PasswordHasher.HashPassword("1"),
                               FullName = "Майская Мирослава Андреевна",
                               Email = "may@mail.ru",
                               RoleId = adminRoleId},

                    new User { Username = "Manager",
                               PasswordHash = PasswordHasher.HashPassword("123"),
                               FullName = "Чундышко Адам Юнусович",
                               Email = "CHAU@google.com",
                               RoleId = operatorRoleId}//,
                    //new User
                    //{
                    //    Username = "Guest",
                    //    PasswordHash = PasswordHasher.HashPassword("123"),
                    //    FullName = "Гость",
                    //    Email = "нет@google.com",
                    //    RoleId = guestRoleId
                    //}
                );

                context.SaveChanges();
            }
        }
    }
}
