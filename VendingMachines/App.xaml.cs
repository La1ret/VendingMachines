using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Data;
using VendingMachines.Data.Interfaces;
using VendingMachines.Data.Repositories;
using VendingMachines.Services;
using VendingMachines.Services.Interfaces;
using VendingMachines.ViewModels;

namespace VendingMachines
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private ServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            InitializeComponent();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Запуск окна входа
            var authorizationWindow = serviceProvider.GetRequiredService<Views.Windows.Authorization>();
            authorizationWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация контекста БД как Transient с использованием фабрики создания
            services.AddTransient<VendingMachinesDbContext>(_ => new VendingMachinesDbContext());

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<_JwtService>();
            services.AddTransient<AuthentificationService>();
            services.AddTransient<UserService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<Views.Windows.Authorization>();
            services.AddTransient<Views.Windows.MainWindow>();
        }

        //private void InitializeDatabaseSync()
        //{
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var dbContext = services.GetRequiredService<VendingMachinesDbContext>();

                
        //        dbContext.Database.CreateIfNotExists();


        //        var roleRepo = services.GetRequiredService<IRoleRepository>();
        //        roleRepo.InitializePredefinedRoles(); 

        //        var userRepo = services.GetRequiredService<IUserRepository>();
        //        userRepo.InitializePredefinedUsers();
        //    }
        //}
    } 
}
