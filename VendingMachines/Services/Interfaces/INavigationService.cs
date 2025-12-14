using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VendingMachines.Services.Interfaces
{
    public interface INavigationService
    {
        void NavigateToMain();
        void NavigateToAuthorization();
        void CloseWindow(Window windowToClose);
    }
}
