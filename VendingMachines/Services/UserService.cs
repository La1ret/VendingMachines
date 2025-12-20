using VendingMachines.Data.Interfaces;
using VendingMachines.Services.Interfaces;

namespace VendingMachines.Services
{
    public class UserService : IUserService
    {
        #region Объявление сервисов и контекста
        
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly INavigationService _navigationService;
        private readonly IUserSessionService _userSessionService;
        #endregion

        #region Инициализатор

        public UserService( IUserRepository userRepository, 
                            IRoleRepository roleRepository, 
                            INavigationService navigationService, 
                            IUserSessionService userSessionService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _navigationService = navigationService;
            _userSessionService = userSessionService;
        }
        #endregion

       
    }
}
