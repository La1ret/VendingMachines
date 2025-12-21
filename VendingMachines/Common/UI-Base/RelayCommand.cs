using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VendingMachines.WPF.Common.Base
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Метод чтобы покрасить кнопку серой или активной.
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Метод вызывается в момент клика.
        public void Execute(object parameter) => _execute(parameter);
    }
}
