using VendingMachines.Data;
using VendingMachines.Data.Interfaces;
using VendingMachines.Data.Repositories;
using VendingMachines.Services;
using VendingMachines.Services.Interfaces;
using VendingMachines.ViewModels;
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

namespace VendingMachines
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly ServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
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

            services.AddTransient<IAuthentificationService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IUserSessionService, UserSessionService>();

            //Views
            services.AddTransient<Views.Windows.Authorization>();
            services.AddTransient<Views.Pages.AuthPage>();
            services.AddTransient<Views.Pages.PasswordRecovery>();
            services.AddTransient<Views.Pages.Registration>();

            services.AddTransient<Views.Windows.MainWindow>();

            //ViewModels
            services.AddTransient<AuthPageViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<PasswordRecoveryViewModel>();
            services.AddTransient<PegistrationViewModel>();

            services.AddTransient<MainWindowViewModel>();
        }
    } 
}
