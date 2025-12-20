using VendingMachines.Application.IServices;

namespace VendingMachines.Application.Services
{
    public class UserService : IUserService
    {
        #region Объявление сервисов и контекста
        
        private readonly INavigationService _navigationService;
        private readonly IUserSessionService _userSessionService;
        #endregion

        #region Инициализатор

        public UserService( INavigationService navigationService, 
                            IUserSessionService userSessionService)
        {
            _navigationService = navigationService;
            _userSessionService = userSessionService;
        }
        #endregion

       
    }
}
