using VendingMachines.Application.IServices;

namespace VendingMachines.Application.Services
{
    public class UserService : IUserService
    {
        #region Объявление сервисов и контекста

        private readonly IUserSessionService _userSessionService;
        #endregion

        #region Инициализатор

        public UserService( IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        #endregion

       
    }
}
