using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.Application.IServices
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
