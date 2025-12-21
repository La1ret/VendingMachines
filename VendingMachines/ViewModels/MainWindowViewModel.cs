using VendingMachines.WPF.Common.Base;
using VendingMachines.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VendingMachines.WPF.Services;
using VendingMachines.WPF.Services.IServices;

namespace VendingMachines.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ICommand BackToAuthCommand { get; }

        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //GetUsernameList();
            BackToAuthCommand = new RelayCommand(OnBackToAuthCommandExecute);
        }
       
        //private async void GetUsernameList() 
        //{
        //    var userLists = await _userRepository.GetAllUsersAsync();
            
        //    foreach (var user in userLists)
        //    {
        //        usernameList.AppendLine(user.Username);
        //    }
        //}

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
