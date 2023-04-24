using System.Windows;
using System.Windows.Controls;

namespace ReactiveUI.Samples.Wpf.Extensions
{
    internal class PasswordExtension
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
           "Password",
           typeof(string),
           typeof(PasswordExtension),
           new PropertyMetadata(string.Empty, OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged -= PasswordChanged;
            if (!GetIsUpdating(passwordBox))
            {
                /*从Password往控件方向更新绑定值*/
                passwordBox.Password = e.NewValue.ToString();
            }

            passwordBox.PasswordChanged += PasswordChanged;
        }

        public static void SetPassword(DependencyObject element, string value)
        {
            element.SetValue(PasswordProperty, value);
        }

        public static string GetPassword(DependencyObject element)
        {
            return (string)element.GetValue(PasswordProperty);
        }

        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached(
            "Attach",
            typeof(bool),
            typeof(PasswordExtension),
            new PropertyMetadata(false, Attach));

        private static void Attach(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PasswordBox passwordBox))
            {
                return;
            }

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                /*当控件的值发生变化的时候，更新Password的值*/
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            /*IsUpdating的作用类似一把互斥锁,因涉及到双向绑定更新*/
            SetIsUpdating(passwordBox, true);
            SetPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
        }

        public static void SetAttach(DependencyObject element, bool value)
        {
            element.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject element)
        {
            return (bool)element.GetValue(AttachProperty);
        }

        public static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached(
            "IsUpdating",
            typeof(bool),
            typeof(PasswordExtension),
            new PropertyMetadata(default(bool)));

        public static void SetIsUpdating(DependencyObject element, bool value)
        {
            element.SetValue(IsUpdatingProperty, value);
        }

        public static bool GetIsUpdating(DependencyObject element)
        {
            return (bool)element.GetValue(IsUpdatingProperty);
        }
    }
}