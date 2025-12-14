using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VendingMachines.Models;
using VendingMachines.Services;
using VendingMachines.Services.Interfaces;

namespace VendingMachines.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        
        public AuthorizationViewModel(INavigationService navigationService, AuthentificationService authService /*, ...*/)
        {
            _navigationService = navigationService;
            // ... инициализация других сервисов
        }

        // ПЕРЕПИСАТЬ
        #region работа с окном

        private static Views.Windows.Authorization _authWindow ;

        /// <summary>текущее окно входа</summary>
        public Views.Windows.Authorization authWindow
        {
            get => _authWindow;
            set => Set(ref _authWindow, value);
        }

        private void LockWindow(int secconds)
        {
            _authWindow.IsEnabled = false;

            MessageBox.Show("Вы использовали три попытки входа в систему.Через 15 секунд у Вас будет возможность повторить попытку входа в систему!");
            System.Threading.Thread.Sleep(secconds * 1000);
            _authWindow.IsEnabled = true;
            _failedLoginAttempts = 0;
        }

        internal void Change() 
        {
            _navigationService.NavigateToMain();
        }
        #endregion


        #region работа с пользователями

        private string _userName = "";

        /// <summary>Текст пароля</summary>
        public string UserName
        {
            get => _userName;
            set 
            {
                Set(ref _userName, value);
                UpdateCanExecute();
            }
        }
        #endregion


        #region работа с паролями

        /// <summary>Текст пароля</summary>
        private string _password = null;
        public string Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                UpdateCanExecute();
            }
        }
        #endregion


        #region работа с видимостью

        /// <summary>Видимость поля пароля</summary>
        private Visibility _isPasswordBoxVisible = Visibility.Visible;
        public Visibility IsPasswordBoxVisible
        {
            get => _isPasswordBoxVisible;
            set => Set(ref _isPasswordBoxVisible, value);
        }


        /// <summary>Видимость текстогого поля пароля (без шифра)</summary>
        private Visibility _isPasswordTextBoxVisible = Visibility.Hidden;
        public Visibility IsPasswordTextBoxVisible
        {
            get => _isPasswordTextBoxVisible;
            set => Set(ref _isPasswordTextBoxVisible, value);
        }


        /// <summary>Видимость пароля (переключатель)</summary>
        private bool _isPasswordVisible = false;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                Set(ref _isPasswordVisible, value);
                ChangeToggleImage(_isPasswordVisible);
                ChangePasswordInputControl(_isPasswordVisible);
            }
        }

        ///<summary> CONST изображений переключателя </summary>
        private static object IMG_VISIBLE = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Visible.png", UriKind.Absolute));
        private static object IMG_HIDDEN = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Hidden.png", UriKind.Absolute));

        ///<summary> изображение переключателя</summary>
        private ImageSource _visibilityToggleImage = (ImageSource)IMG_HIDDEN;
        public ImageSource VisibilityToggleImage
        {
            get => _visibilityToggleImage;
            set => Set(ref _visibilityToggleImage, value);
        }

        /// <summary> Смена изображений переключателя </summary>
        /// <param name="isPasswordVisible"> должен ли быть виден пароль</param>
        private void ChangeToggleImage(bool isPasswordVisible) 
        {
            if (isPasswordVisible)
            {
                VisibilityToggleImage = (ImageSource)IMG_VISIBLE;
            }
            else if (!isPasswordVisible)
            {
                VisibilityToggleImage = (ImageSource)IMG_HIDDEN;
            }
        }

        /// <summary> Изменение поля ввода пароля на TextBox и обратно на PasswordBox</summary>
        /// <param name="isPasswordVisible">должен ли быть виден пароль</param>
        private void ChangePasswordInputControl(bool isPasswordVisible)
        {
            if (isPasswordVisible)
            {
                IsPasswordBoxVisible = Visibility.Hidden;
                IsPasswordTextBoxVisible = Visibility.Visible;
            }
            else if (!isPasswordVisible)
            {
                IsPasswordBoxVisible = Visibility.Visible;
                IsPasswordTextBoxVisible = Visibility.Hidden;
            }
        }
        #endregion


        // ДОПИСАТЬ вызов по кнопке 
        #region Логика аутентификации

        private int _failedLoginAttempts = 0;


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
        //        default :
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
        #endregion


        #region Логика состояния блокировки полей

        private bool _isSignInButtonEnabled;
        public bool IsSignInButtonEnabled
        {
            get => _isSignInButtonEnabled;
            set => Set(ref _isSignInButtonEnabled, value);
        }


        private bool _isPasswordFieldsEnabled;
        public bool IsPasswordFieldsEnabled
        {
            get => _isPasswordFieldsEnabled;
            set => Set(ref _isPasswordFieldsEnabled, value);
        }

        private void UpdateCanExecute()
        {
            IsPasswordFieldsEnabled = !string.IsNullOrEmpty(UserName);
            IsSignInButtonEnabled = !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        }
        #endregion
    }
}
