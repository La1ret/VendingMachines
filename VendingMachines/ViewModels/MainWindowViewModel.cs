using VendingMachines.Common;
using VendingMachines.Data.Interfaces;
using VendingMachines.Data.Repositories;
using VendingMachines.Services.Interfaces;
using VendingMachines.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VendingMachines.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;
        private readonly INavigationService _navigationService;

        public ICommand BackToAuthCommand { get; }

        public MainWindowViewModel(IUserRepository userRepository, INavigationService navigationService)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;

            GetUsernameList();
            BackToAuthCommand = new RelayCommand(OnBackToAuthCommandExecute);
        }
        private async void GetUsernameList() 
        {
            var userLists = await _userRepository.GetAllUsersAsync();
            
            foreach (var user in userLists)
            {
                usernameList.AppendLine(user.Username);
            }
        }

        private StringBuilder usernameList = new StringBuilder();

        public string UserList
        {
            get => usernameList.ToString();
            set => usernameList.ToString();
        }

        private void OnBackToAuthCommandExecute(object p) 
        {
            _navigationService.ChangeWindowTo<Authorization>();
        }
    }
}
