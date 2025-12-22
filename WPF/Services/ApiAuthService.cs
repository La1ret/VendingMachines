using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VendingMachines.Shared;
using VendingMachines.Shared.DTOs.User;
using VendingMachines.WPF.Services.IServices;
using System.Net.Http.Json;

namespace VendingMachines.WPF.Services
{
    public class ApiAuthService : IApiAuthService
    {
        private readonly HttpClient _httpClient;
      
        public ApiAuthService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Поля

        private bool _isLocked;
        private string _messageIsLocked;
        #endregion

        #region Свойства блокировки

        public bool IsLocked
        {
            get => _isLocked;
            private set { _isLocked = value; OnPropertyChanged(); }
        }

        public string MessageIsLocked
        {
            get => _messageIsLocked;
            private set { _messageIsLocked = value; OnPropertyChanged(); }
        }
        #endregion


        public async Task<OperationResult> RegisterAsync( string fullName, string email, string username, string password) 
        {
            return OperationResult.Failure("Пока не подключен API");
        }

        public async Task<OperationResult> CreateGuestTokenAsync()
        {
            return OperationResult.Failure("Пока не подключен API");
        }

        public async Task<OperationResult<UserAuthResponse>> AuthenticateAsync(UserLoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/Auth/Login", request);

            var result = await response.Content.ReadFromJsonAsync<OperationResult<UserAuthResponse>>();

            if (result.Message.Contains("Вход в аккаунт временно заблокирован."))
            {
                var messageArray = result.Message.Split();

                if (short.TryParse(messageArray[messageArray.Length - 2], out short timeInSeconds))
                {
                    IsLocked = true;
                    MessageIsLocked = result.Message;
                    await Task.Delay(timeInSeconds * 1000);
                    result.Message = "";//Потому что уже отображено
                    IsLocked = false;
                }
            }
            
            return result;
        }

        public async Task<OperationResult> RequestPasswordRecoveryAsync(string username, string email)
        {
            return OperationResult.Failure("Пока не подключен API");
        }
    }
}
