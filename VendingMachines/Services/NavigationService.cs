using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Services.Interfaces;
using VendingMachines.Views.Windows;

namespace VendingMachines.Services
{
    internal class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateToMain()
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public void NavigateToAuthorization()
        {
            var authWindow = _serviceProvider.GetRequiredService<Authorization>();
            authWindow.Show();
        }

        public void CloseWindow(Window windowToClose)
        {
            if (windowToClose != null)
            {
                windowToClose.Close();
            }
        }
    }
}
