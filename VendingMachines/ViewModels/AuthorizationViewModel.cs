using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VendingMachines.Service;

namespace VendingMachines.ViewModels
{
    internal class AuthorizationViewModel : ViewModelBase
    {

        private static View.Windows.Authorization _authWindow ;

        /// <summary>текущее окно входа</summary>
        public View.Windows.Authorization authWindow
        {
            get => _authWindow;
            set => Set(ref _authWindow, value);
        }

        #region Текст пароля

        private string _PswText = "";

        /// <summary>Текст пароля</summary>
        public string PswText
        {
            get => _PswText;
            set => Set(ref _PswText, value);
        }

        #endregion


        private string loginText;
        private const string adminLogin = "Admin";
        private const string adminPassword = "1";

        private const string managerLogin = "Manager";
        private const string managerPassword = "123";

        int count = 0;


        private void Button_Click(object sender)
        {
            Authenticate(_PswText);

            if (count == 3)
                LockWindow(15);
        }

        private void Authenticate(string passwordText)
        {
            switch (loginText)
            {
                case adminLogin:
                    CheckPassword(passwordText, adminPassword);
                    break;
                case managerLogin:
                    CheckPassword(passwordText, managerPassword);
                    break;
                case null:
                    MessageBox.Show("Не введён пользователь!");
                    break;
                case "":
                    MessageBox.Show("Не введён пользователь!");
                    break;
                default:
                    MessageBox.Show("Данный пользователь не существует или введён неверно!");
                    count += 1;
                    break;
            }
        }

        private void CheckPassword(string passwordText, string correctPsw)
        {
            if (passwordText == correctPsw)
            {
                MessageBox.Show("Авторизация прошла успешно!");
                NavigationManager.ChangeWindow("Main", _authWindow);
            }
            else if (passwordText == null || passwordText == "")
                MessageBox.Show("Не введён пароль!");
            else
            {
                MessageBox.Show("Не верный пароль!");
                count += 1;
            }
        }

        private void LockWindow(int secconds)
        {
            _authWindow.IsEnabled = false;

            MessageBox.Show("Вы использовали три попытки входа в систему.Через 15 секунд у Вас будет возможность повторить попытку входа в систему!");
            System.Threading.Thread.Sleep(secconds * 1000);
            _authWindow.IsEnabled = true;
            count = 0;
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    loginText = login.Text;

        //    if (loginText != null && loginText != "")
        //    {
        //        password.IsEnabled = true;
        //        toggle.IsEnabled = true;
        //    }
        //    else
        //    {
        //        password.IsEnabled = false;
        //        toggle.IsEnabled = false;
        //    }
        //}

        //private void ToggleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if ((bool)toggle.IsChecked)
        //    {
        //        passwordTxt.Text = password.Password;
        //        toggle.Content = "Скрыть";
        //    }
        //    else
        //    {
        //        password.Password = passwordTxt.Text;
        //        toggle.Content = "Показать";
        //    }
        //    passwordTxt.Visibility = (toggle.IsChecked == true) ? Visibility.Visible : Visibility.Hidden;
        //    password.Visibility = (toggle.IsChecked == true) ? Visibility.Hidden : Visibility.Visible;
        //}

    }
}
