using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.Services.Interfaces
{
    public interface INavigationService
    {
        void SetFrame(Frame frame);
        void CloseWindow(Window windowToClose);
        void NavigateToPage<T>()
            where T : Page;
        void ChangeWindowTo<T>()
            where T : Window;

    }
}
