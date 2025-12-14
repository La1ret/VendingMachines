using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Models;

namespace VendingMachines.Data
{
    public class VendingMachinesDbContext : DbContext
    {
        // Свойства DbSet делаем публичными для доступа из других частей приложения
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Конструктор, который использует программно созданное соединение
        public VendingMachinesDbContext() : base(GetConnection(), true)
        {
        }

        private static SQLiteConnection GetConnection()
        {
            // Строка подключения из вашего App.config
            string connectionString = "Data Source=|DataDirectory|\\VendingMachines.db; Pooling=true; Max Pool Size=100; FailIfMissing=False";

            var connection = new SQLiteConnection(connectionString);
            return connection;
        }
    }
}
