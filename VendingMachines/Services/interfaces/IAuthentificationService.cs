using VendingMachines.Common;
using VendingMachines.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace VendingMachines.Services.Interfaces
{
    public interface IAuthentificationService: INotifyPropertyChanged
    {
        bool IsLocked { get; }
        Task<OperationResult> AuthenticateAsync(string username, string password); 
        Task<OperationResult> RegisterAsync(string fullName, string email, string username, string password);
        Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email);
    }
}
