using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Shared;
using VendingMachines.WPF.Services.IServices;

namespace VendingMachines.WPF.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private bool _isLocked;

        public ApiAuthService() { }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Свойство блокировки

        public bool IsLocked
        {
            get => _isLocked;
            private set { _isLocked = value; OnPropertyChanged(); }
        }
        #endregion

        public async Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email)
        {
            return OperationResult.Failure("Пока не подключен API");
        }

        public async Task<OperationResult> AuthenticateAsync(string username, string password)
        {
            return OperationResult.Failure("Пока не подключен API");
        }

        public async Task<OperationResult> RegisterAsync( string fullName, string email, string username, string password) 
        {
            return OperationResult.Failure("Пока не подключен API");
        }

        public async Task<OperationResult> CreateGuestTokenAsync()
        {
            return OperationResult.Failure("Пока не подключен API");
        }
    }
}
