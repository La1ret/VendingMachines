using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.WPF.Common.Helpers
{
    public static class PasswordBoxHelper
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
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

                if (!GetIsUpdating(passwordBox))
                {
                    passwordBox.Password = (string)e.NewValue;
                }

                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }
        #endregion

        #region Свойство IsPasswordEmpty

        //  Свойство IsPasswordEmpty (для биндинга состояния пустоты в ViewModel/XAML и стиле)
        public static readonly DependencyProperty IsPasswordEmptyProperty =
            DependencyProperty.RegisterAttached("IsPasswordEmpty", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(true));

        internal static bool GetIsPasswordEmpty(DependencyObject dp) => (bool)dp.GetValue(IsPasswordEmptyProperty);
        internal static void SetIsPasswordEmpty(DependencyObject dp, bool value) => dp.SetValue(IsPasswordEmptyProperty, value);
        #endregion

        #region Cвойство IsUpdating

        // Вспомогательное свойство для предотвращения рекурсии
        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false));

        private static bool GetIsUpdating(DependencyObject dp) => (bool)dp.GetValue(IsUpdatingProperty);
        private static void SetIsUpdating(DependencyObject dp, bool value) => dp.SetValue(IsUpdatingProperty, value);
        #endregion

        #region Обработчик события PasswordChanged

        // Единый обработчик события PasswordChanged
        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null) return;

            bool isEmpty = string.IsNullOrEmpty(passwordBox.Password);
            SetIsPasswordEmpty(passwordBox, isEmpty);

            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }
        #endregion
    }
}

