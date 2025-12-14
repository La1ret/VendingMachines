using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Models;

namespace VendingMachines.Services
{
    public class AuthentificationService
    {
        //private int _failedLoginAttempts = 0;


        //private void Button_Click(object sender)
        //{
        //    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        //    {

        //    }
        //    User user = AccountFactory.FindAccountOnUserName(Username);
        //    Authenticate(user, Password);

        //    if (_failedLoginAttempts == 3)
        //        LockWindow(15);
        //}

        //private void Authenticate(User user, string currentPassword)
        //{
        //    switch (user)
        //    {
        //        case null:
        //            MessageBox.Show("Данный пользователь не существует или введён неверно!");
        //            _failedLoginAttempts += 1;
        //            break;
        //        default:
        //            CheckPassword(user, currentPassword);
        //            break;
        //    }
        //}

        //private void CheckPassword(User user, string currentPassword)
        //{
        //    if (currentPassword == user.Password)
        //    {
        //        MessageBox.Show("Авторизация прошла успешно!");
        //        Helpers.NavigationHelper.ChangeWindow("Main", _authWindow);
        //    }
        //    else if (currentPassword != user.Password)
        //    {
        //        MessageBox.Show("Не верный пароль!");
        //        _failedLoginAttempts += 1;
        //    }
        //}
    }
}
