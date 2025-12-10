using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VendingMachines.Helpers
{
    internal class PswBoxHelper
    {
        // 1. Присоединенное свойство, которое хранит булево значение (true, если пароль пуст)
        public static readonly DependencyProperty IsPasswordEmptyProperty =
            DependencyProperty.RegisterAttached("IsPasswordEmpty", typeof(bool), typeof(PswBoxHelper), new PropertyMetadata(true));

        public static bool GetIsPasswordEmpty(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordEmptyProperty);
        }

        public static void SetIsPasswordEmpty(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordEmptyProperty, value);
        }

        // 2. Присоединенное свойство, которое мы используем ТОЛЬКО для подписки на событие PasswordChanged
        public static readonly DependencyProperty ObservePasswordProperty =
            DependencyProperty.RegisterAttached("ObservePassword", typeof(bool), typeof(PswBoxHelper), new PropertyMetadata(false, OnObservePasswordChanged));

        public static bool GetObservePassword(DependencyObject obj)
        {
            return (bool)obj.GetValue(ObservePasswordProperty);
        }

        public static void SetObservePassword(DependencyObject obj, bool value)
        {
            obj.SetValue(ObservePasswordProperty, value);
        }

        // Обработчик события, который обновляет IsPasswordEmpty
        private static void OnObservePasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            if (dp is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
                    // Устанавливаем начальное значение при первой подписке
                    SetIsPasswordEmpty(passwordBox, string.IsNullOrEmpty(passwordBox.Password));
                }
                else
                {
                    passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                }
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // Обновляем IsPasswordEmpty каждый раз при изменении пароля
                SetIsPasswordEmpty(passwordBox, string.IsNullOrEmpty(passwordBox.Password));
            }
        }
    }
}
