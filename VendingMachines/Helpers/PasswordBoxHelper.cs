using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.Helpers
{
    internal static class PasswordBoxHelper
    {
        
        #region Свойство Password

        // Свойство Password (для передачи данных в ViewModel как string)
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject dp) => (string)dp.GetValue(PasswordProperty);
        public static void SetPassword(DependencyObject dp, string value) => dp.SetValue(PasswordProperty, value);

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Отписываемся, чтобы избежать рекурсии при обновлении из ViewModel
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                // Обновляем UI значением из ViewModel
                if (!GetIsUpdating(passwordBox))
                {
                    passwordBox.Password = (string)e.NewValue;
                }

                // Подписываемся снова.
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }
        #endregion


        #region Свойство IsPasswordEmpty

        //  Свойство IsPasswordEmpty (для биндинга состояния пустоты в ViewModel/XAML)
        public static readonly DependencyProperty IsPasswordEmptyProperty =
            DependencyProperty.RegisterAttached("IsPasswordEmpty", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(true));

        internal static bool GetIsPasswordEmpty(DependencyObject dp) => (bool)dp.GetValue(IsPasswordEmptyProperty);
        internal static void SetIsPasswordEmpty(DependencyObject dp, bool value) => dp.SetValue(IsPasswordEmptyProperty, value);
        #endregion


        #region Cвойство IsUpdating

        // Вспомогательное свойство IsUpdating (для предотвращения рекурсии)
        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false));

        private static bool GetIsUpdating(DependencyObject dp) => (bool)dp.GetValue(IsUpdatingProperty);
        private static void SetIsUpdating(DependencyObject dp, bool value) => dp.SetValue(IsUpdatingProperty, value);
        #endregion


        #region Обработчик события PasswordChanged

        // 4. Единый обработчик события PasswordChanged (Вся логика здесь)
        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null) return;

            // Обновляем свойство IsPasswordEmpty при каждом изменении
            bool isEmpty = string.IsNullOrEmpty(passwordBox.Password);
            SetIsPasswordEmpty(passwordBox, isEmpty);

            // Обновляем основное свойство Password во ViewModel (ВСЕГДА)
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
        #endregion



        // Новая версия кривая но попроще (не помогло)

        //// Свойство Password (с Callback, который гарантирует подписку)
        //public static readonly DependencyProperty PasswordProperty =
        //    DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper),
        //        new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        //public static string GetPassword(DependencyObject dp) => (string)dp.GetValue(PasswordProperty);
        //public static void SetPassword(DependencyObject dp, string value) => dp.SetValue(PasswordProperty, value);

        //private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var passwordBox = sender as PasswordBox;
        //    if (passwordBox != null)
        //    {
        //        // Убеждаемся, что мы подписаны на событие при любом изменении
        //        passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
        //        passwordBox.PasswordChanged += PasswordBox_PasswordChanged;

        //        // Обновляем UI из ViewModel
        //        passwordBox.Password = (string)e.NewValue;
        //    }
        //}

        //// Свойство IsPasswordEmpty (для триггера плейсхолдера)
        //public static readonly DependencyProperty IsPasswordEmptyProperty =
        //    DependencyProperty.RegisterAttached("IsPasswordEmpty", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(true));

        //public static bool GetIsPasswordEmpty(DependencyObject dp) => (bool)dp.GetValue(IsPasswordEmptyProperty);
        //public static void SetIsPasswordEmpty(DependencyObject dp, bool value) => dp.SetValue(IsPasswordEmptyProperty, value);


        //// Общий обработчик события PasswordChanged
        //private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    var passwordBox = sender as PasswordBox;
        //    if (passwordBox == null) return;

        //    // Обновляем свойство IsPasswordEmpty (для триггера)
        //    bool isEmpty = string.IsNullOrEmpty(passwordBox.Password);
        //    SetIsPasswordEmpty(passwordBox, isEmpty);

        //    // Обновляем ViewModel
        //    // SetPassword() вызывает OnPasswordPropertyChanged, 
        //    // который снова подписывает нас на событие, но это безопасно.
        //    SetPassword(passwordBox, passwordBox.Password);
        //}
    }
}
