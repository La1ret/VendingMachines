using VendingMachines.WPF.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.WPF.Services
{
    public class NavigationService : INavigationService
    {
        #region Объявление сервисов и фреймов

        private readonly IServiceProvider _serviceProvider; 
        private Frame _frame;
        #endregion

        #region Инициализатор

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        public void SetFrame(Frame frame) => _frame = frame;

        public void NavigateToPage<NextPage>() 
            where NextPage : Page
        {
            var page = _serviceProvider.GetRequiredService<NextPage>();
            _frame?.Navigate(page);
        }

        public void CloseWindow(Window windowToClose)
        {
            if (windowToClose != null)
            {
                windowToClose.Close();
            }
        }

        public void ChangeWindowTo<NextWindow>()
             where NextWindow : Window
        {
            var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

            var newWindow = _serviceProvider.GetRequiredService<NextWindow>();
                
            currentWindow.Close();
            newWindow.Show();
        }
    }
}
