using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VendingMachines.WPF.Common.Base;
using VendingMachines.Views.Pages;
using VendingMachines.WPF.Services.IServices;
using VendingMachines.Views.Windows;
using VendingMachines.WPF.Services;

namespace VendingMachines.ViewModels
{
    internal class PegistrationViewModel : ViewModelBase
    {
        #region Объявление сервисов

        private readonly INavigationService _navigationService;
        private readonly IApiAuthService _authService;
        #endregion

        #region Объявление команд

        public ICommand BackCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ChangePasswordVisibility { get; }
        #endregion

        #region Объявление полей

        private string _registrationStatusMessage = "";
        private string _fullName = "";
        private string _email = "";
        private string _username = "";
        private string _password = null; 
        private bool _familiarWithPolicy = false;
        private Visibility _isPasswordBoxVisible = Visibility.Visible;
        private Visibility _isPasswordTextBoxVisible = Visibility.Hidden;
        private ImageSource _visibilityToggleImage = (ImageSource)IMG_HIDDEN;

        ///<summary> CONST значений изображения переключателя </summary>
        private static object IMG_VISIBLE = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Visible.png", UriKind.Absolute));
        private static object IMG_HIDDEN = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Hidden.png", UriKind.Absolute));
        #endregion

        #region Инициализация

        public PegistrationViewModel(INavigationService navigationService, IApiAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;

            BackCommand = new RelayCommand(OnBackCommandExecute);
            SignUpCommand = new RelayCommand(OnSignUpCommandExecuteAsync, CanSignUpCommandExecute);
            ChangePasswordVisibility = new RelayCommand(OnChangePasswordVisibilityExecute);
            _authService = authService;
        }
        #endregion

        #region Свойства для биндинга в View

        /// <summary>Значение сообщения о статусе или результате регистрации пользователя</summary>
        public string RegistrationStatusMessage
        {
            get => _registrationStatusMessage;
            set => Set(ref _registrationStatusMessage, value);
        }

        /// <summary>Значение никнейма (логина) пользователя</summary>
        public string FullName
        {
            get => _fullName;
            set => Set(ref _fullName, value);
        }

        /// <summary>Значение почты пользователя</summary>
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        /// <summary>Значение имени пользователя</summary>
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        /// <summary>Значение для закрытого и открытого текста пароля</summary>
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        /// <summary>Значение подтверждения политики</summary>
        public bool FamiliarWithPolicy
        {
            get => _familiarWithPolicy;
            set => Set(ref _familiarWithPolicy, value);
        }

        /// <summary>Видимость поля пароля (с сокрытием)</summary>
        public Visibility IsPasswordBoxVisible
        {
            get => _isPasswordBoxVisible;
            set => Set(ref _isPasswordBoxVisible, value);
        }

        /// <summary>Видимость текстогого поля пароля (без сокрытия)</summary>
        public Visibility IsPasswordTextBoxVisible
        {
            get => _isPasswordTextBoxVisible;
            set => Set(ref _isPasswordTextBoxVisible, value);
        }

        ///<summary> изображение переключателя</summary>
        public ImageSource VisibilityToggleImage
        {
            get => _visibilityToggleImage;
            set => Set(ref _visibilityToggleImage, value);
        }
        #endregion

        #region Команда назад

        private void OnBackCommandExecute(object p)
        {
            _navigationService.NavigateToPage<AuthPage>();
        }
        #endregion

        #region Команда регистрации

        private async void OnSignUpCommandExecuteAsync(object p)
        {
            RegistrationStatusMessage = "";
            await Task.Delay(150); //Для заметности отображения смены сообщения

            var result = await _authService.RegisterAsync(_fullName, _email, _username, _password);
            if (result.IsSuccess)
            {
                RegistrationStatusMessage = "Производится вход в систему...";
                _navigationService.ChangeWindowTo<MainWindow>();
            }
            else 
            {
                RegistrationStatusMessage = result.Message;
            }
            
            await Task.Delay(15000); //Затирка сообщения через 15 секунд
            RegistrationStatusMessage = "";
        }

        private bool CanSignUpCommandExecute(object p)
        {
            return !string.IsNullOrEmpty(_username) 
                && !string.IsNullOrEmpty(_password) 
                && !string.IsNullOrEmpty(_username) 
                && !string.IsNullOrEmpty(_password) 
                && _familiarWithPolicy;//Дописать
        }
        #endregion

        #region Команда смены видимости паролей

        private void OnChangePasswordVisibilityExecute(object isChecked)
        {
            bool isPasswordVisible = (bool)isChecked;

            ChangeToggleImage(isPasswordVisible);
            ChangePasswordInputControl(isPasswordVisible);
        }
        #endregion

        #region Методы команды смены видимости паролей

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
    }
}
