using System.ComponentModel;
using System.Runtime.CompilerServices;
using VendingMachines.Application.IServices;
using VendingMachines.Domain.Models;

namespace VendingMachines.Application.Services
{
    public class UserSessionService :  IUserSessionService
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

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Set
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
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
