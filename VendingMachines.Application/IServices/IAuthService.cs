using System.ComponentModel;
using System.Threading.Tasks;
using VendingMachines.Application.Common;

namespace VendingMachines.Application.IServices
{
    public interface IAuthService
    {
        Task<OperationResult> AuthenticateAsync(string username, string password); 
        Task<OperationResult> RegisterAsync(string fullName, string email, string username, string password);
        Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email);
    }
}
