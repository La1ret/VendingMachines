using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VendingMachines.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            //password.IsEnabled = false;
            //toggle.IsEnabled = false;
            PswText.Visibility = Visibility.Hidden;
            
            //ViewModels.AuthorizationViewModel sd = this;

        }


        //string loginText;
        //const string adminLogin = "Admin";
        //const string adminPassword = "1";

        //const string managerLogin = "Manager";
        //const string managerPassword = "123";

        //int count = 0;


        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Authenticate(password.Password);

        //    if (count == 3)
        //        LockWindow(15);
        //}

        //private void Authenticate(string passwordText)
        //{
        //    switch (loginText)
        //    {
        //        case adminLogin:
        //            CheckPassword(passwordText, adminPassword);
        //            break;
        //        case managerLogin:
        //            CheckPassword(passwordText, managerPassword);
        //            break;
        //        case null:
        //            MessageBox.Show("Не введён пользователь!");
        //            break;
        //        case "":
        //            MessageBox.Show("Не введён пользователь!");
        //            break;
        //        default:
        //            MessageBox.Show("Данный пользователь не существует или введён неверно!");
        //            count += 1;
        //            break;
        //    }
        //}

        //private void CheckPassword(string passwordText, string correctPsw)
        //{
        //    if (passwordText == correctPsw)
        //    {
        //        MessageBox.Show("Авторизация прошла успешно!");
        //        NavigationManager.ChangeWindow("Main", this);
        //    }
        //    else if (passwordText == null || passwordText == "")
        //        MessageBox.Show("Не введён пароль!");
        //    else
        //    {
        //        MessageBox.Show("Не верный пароль!");
        //        count += 1;
        //    }
        //}

        //private void LockWindow(int secconds)
        //{
        //    this.IsEnabled = false;

        //    MessageBox.Show("Вы использовали три попытки входа в систему.Через 15 секунд у Вас будет возможность повторить попытку входа в систему!");
        //    System.Threading.Thread.Sleep(secconds * 1000);
        //    this.IsEnabled = true;
        //    count = 0;
        //}

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
