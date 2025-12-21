using System.ComponentModel;
using System.Threading.Tasks;
using VendingMachines.Shared;
using VendingMachines.Shared.DTOs.User;

namespace VendingMachines.Application.IServices
{
    public interface IAuthService
    {
        Task<OperationResult<UserAuthResponse>> AuthenticateAsync(UserLoginRequest request);
        Task<OperationResult> RegisterAsync(string fullName, string email, string username, string password);
        Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email);
    }
}
