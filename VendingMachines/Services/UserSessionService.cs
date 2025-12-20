using VendingMachines.Common;
using VendingMachines.Models;
using VendingMachines.Services.Interfaces;

namespace VendingMachines.Services
{
    public class UserSessionService : ViewModelBase, IUserSessionService
    {
        #region Объявление сервисов
        #endregion

        #region Инициализатор
        public UserSessionService() 
        {
        }
        #endregion

        #region Поля

        private User _currentUser = null;
        #endregion

        #region Свойства
        public User CurrentUser 
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }
        #endregion

        #region Основные методы
        public void StartSession(User user) 
        { 
        
        }

        public void CreateGuestSession() 
        {
        
        }
        #endregion

        #region Вспомогательные
        #endregion
    }
}
