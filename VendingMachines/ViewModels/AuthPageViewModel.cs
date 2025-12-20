using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VendingMachines.Common;
using VendingMachines.Models;
using VendingMachines.Services;
using VendingMachines.Services.Interfaces;
using VendingMachines.Views.Pages;
using VendingMachines.Views.Windows;

namespace VendingMachines.ViewModels
{
    public class AuthPageViewModel : ViewModelBase
    {
        #region Объявление сервисов

        private readonly INavigationService _navigationService;

        private readonly IAuthentificationService _authService;
        #endregion

        #region Объявление команд

        public ICommand ChangePasswordVisibility { get; }
        public ICommand ShowForgotPasswordCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand EnterLikeGuestCommand { get; }
        #endregion

        #region Объявление полей и контант

        private bool _windowIsEnabled;
        private string _authStatusMessage = "";
        private string _username = "";
        private string _password = null;
        private bool _isPasswordFieldsEnabled;
        private Visibility _isPasswordBoxVisible = Visibility.Visible;
        private Visibility _isPasswordTextBoxVisible = Visibility.Hidden;
        private ImageSource _visibilityToggleImage = (ImageSource)IMG_HIDDEN;

        ///<summary> CONST значений изображения переключателя </summary>
        private static readonly object IMG_VISIBLE = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Visible.png", UriKind.Absolute));
        private static readonly object IMG_HIDDEN = new BitmapImage(new Uri("pack://application:,,,/Resources/Assets/Hidden.png", UriKind.Absolute));
        #endregion

        #region Инициализация

        public AuthPageViewModel(INavigationService navigationService, IAuthentificationService authService /*, ...*/)
        {
            _navigationService = navigationService;
            _authService = authService;

            _windowIsEnabled = !_authService.IsLocked;

            //Подписка на изменение блокировки окна авторизации
            _authService.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(IAuthentificationService.IsLocked))
                {
                    AuthStatusMessage = "Превышено количество попыток входа! Система заблокирована на 15 секунд.";
                    WindowIsEnabled = !_authService.IsLocked;
                }
            };


            SignInCommand = new RelayCommand(OnSignInCommandExecuteAsync, CanSignInCommandExecute);
            SignUpCommand = new RelayCommand(OnSignUpCommandExecute);
            ChangePasswordVisibility = new RelayCommand(OnChangePasswordVisibilityExecute, CanChangePasswordVisibilityExecute);
            ShowForgotPasswordCommand = new RelayCommand(OnShowForgotPasswordCommandExecute);
            EnterLikeGuestCommand = new RelayCommand(OnEnterLikeGuestCommandExecute);
        }
        #endregion

        #region Свойства для биндинга в View

        /// <summary>Значение доступности окна</summary>
        public bool WindowIsEnabled
        {
            get => _windowIsEnabled;
            set => Set(ref _windowIsEnabled, value);
        }

        /// <summary>Значение сообщения о статусе или результате аунтетификации пользователя</summary>
        public string AuthStatusMessage
        {
            get => _authStatusMessage;
            set => Set(ref _authStatusMessage, value);
        }

        /// <summary>Значение никнейма (логина) пользователя</summary>
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

        /// <summary>Видимость поля пароля</summary>
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

        /// <summary> Общее значение доступности поля закрытого (Pwd) и открытого (Txt) пароля и кнопки переключающей его видимость</summary>
        public bool IsPasswordFieldsEnabled
        {
            get => _isPasswordFieldsEnabled;
            set => Set(ref _isPasswordFieldsEnabled, value);
        }

        ///<summary> изображение переключателя</summary>
        public ImageSource VisibilityToggleImage
        {
            get => _visibilityToggleImage;
            set => Set(ref _visibilityToggleImage, value);
        }
        #endregion


        #region Команда смены видимости паролей

        private void OnChangePasswordVisibilityExecute(object isChecked)
        {
            bool isPasswordVisible = (bool)isChecked;

            ChangeToggleImage(isPasswordVisible);
            ChangePasswordInputControl(isPasswordVisible);
        }

        private bool CanChangePasswordVisibilityExecute(object p)
        {
            bool IsEnabled = !string.IsNullOrEmpty(_username);
            IsPasswordFieldsEnabled = IsEnabled;

            return IsEnabled;
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

        #region Команда входа

        private async void OnSignInCommandExecuteAsync(object p)
        {
            AuthStatusMessage = "";
            await Task.Delay(150); //Для того чтоб пользователь видел что сообщение сменилось (если на такое же)

            var result = await _authService.AuthenticateAsync(_username, _password);
            if (result.IsSuccess)
            {
                _navigationService.ChangeWindowTo<MainWindow>();
            }
            else 
            {
                AuthStatusMessage = result.Message;
            }
            
            //Затирание текста через 15 сек
            await Task.Delay(15000);//15 сек
            AuthStatusMessage = "";
        }

        private bool CanSignInCommandExecute(object p)
        {
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password);
        }
        #endregion

        #region Команда перехода на страницу регистрации

        private void OnSignUpCommandExecute(object p)
        {
            _navigationService.NavigateToPage<Registration>();
        }
        #endregion

        #region Команда перехода на страницу восстановления пароля

        private void OnShowForgotPasswordCommandExecute(object p)
        {
            _navigationService.NavigateToPage<PasswordRecovery>();
        }
        #endregion

        #region Команда входа без аутентификации

        private void OnEnterLikeGuestCommandExecute(object p)
        {
            VendingMachines.Services.Class1.Button_Click();
        }
        #endregion
    }
}
