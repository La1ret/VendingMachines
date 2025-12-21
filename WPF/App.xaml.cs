using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.ViewModels;
using VendingMachines.WPF.Services;
using VendingMachines.WPF.Services.IServices;

namespace VendingMachines
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
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
            //Service (для навигации между окнами и страницами и вызова АПИ поэтому не в Application)
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<IApiAuthService, ApiAuthService>();

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
