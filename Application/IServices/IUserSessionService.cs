using VendingMachines.Domain.Models;

namespace VendingMachines.Application.IServices
{
    public interface IUserSessionService
    {
        User CurrentUser { get; set; }
        void StartSession(User user);
        void CreateGuestSession(); 
    }
}
