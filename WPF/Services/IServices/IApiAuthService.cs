using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Shared;
using VendingMachines.Shared.DTOs.User;

namespace VendingMachines.WPF.Services.IServices
{
    public interface IApiAuthService : INotifyPropertyChanged
    {
        bool IsLocked { get; }
        Task<OperationResult<UserAuthResponse>> AuthenticateAsync(UserLoginRequest request);
        Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email);
        Task<OperationResult> RegisterAsync(string fullName, string email, string username, string password);
        Task<OperationResult> CreateGuestTokenAsync();
    }
}
