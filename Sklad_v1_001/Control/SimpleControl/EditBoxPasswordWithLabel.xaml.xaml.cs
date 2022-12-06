using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.SimpleControl
{
    public class BoundPasswordBox
    {
        #region BoundPassword
        private static bool _updating = false;
        /// <summary>
        /// BoundPassword Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty BoundPasswordProperty =  DependencyProperty.RegisterAttached("BoundPassword",
        typeof(string),
        typeof(BoundPasswordBox), new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));
        /// <summary>
        /// Gets the BoundPassword property.
        /// </summary>
        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPasswordProperty);
        }
        /// <summary>
        /// Sets the BoundPassword property.
        /// </summary>
        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        /// <summary>
        /// Handles changes to the BoundPassword property.
        /// </summary>
        private static void OnBoundPasswordChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            if (password != null)
            {
                // Disconnect the handler while we're updating.
                //password.PasswordChanged -= PasswordChanged;
            }
            if (e.NewValue != null)
            {
                if (!_updating)
                {
                    password.Password = e.NewValue.ToString();
                }
            }
            else
            {
                password.Password = string.Empty;
            }
            // Now, reconnect the handler.
            //password.PasswordChanged += new RoutedEventHandler(PasswordChanged);
        }
        /// <summary>
        /// Handles the password change event.
        /// </summary>
        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            //PasswordBox password = sender as PasswordBox;
            //_updating = true;
            //SetBoundPassword(password, password.Password);
            //_updating = false;
            //password.Focus();
        }
        #endregion

    }
    /// <summary>
    /// Логика взаимодействия для EditBoxPasswordWithLabel.xaml
    /// </summary>
    public partial class EditBoxPasswordWithLabel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxPasswordWithLabel), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get
            {
                return (Visibility)GetValue(IsRequiredProperty);
            }
            set
            {
                SetValue(IsRequiredProperty, value);
                OnPropertyChanged("IsRequired");
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty TextPasswordProperty = DependencyProperty.Register(
                        "TextPassword",
                        typeof(string),
                        typeof(EditBoxPasswordWithLabel), new UIPropertyMetadata(String.Empty));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string TextPassword
        {
            get { return (string)GetValue(TextPasswordProperty); }
            set { SetValue(TextPasswordProperty, value); }
        }

        private Boolean isShowPassword;

        public event Action ButtonTextChangedClick;

        public EditBoxPasswordWithLabel()
        {
            InitializeComponent();

            image.Source = ImageHelper.GenerateImage("IconLoopPassword_X16.png");

            PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
            TextBox.Visibility = System.Windows.Visibility.Collapsed;
            IsShowPassword = false;
        }

        public string LabelText
        {
            get
            {
                return label.Content.ToString();
            }

            set
            {
                label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return wrapPanel.Width;
            }

            set
            {
                wrapPanel.Width = value;
            }
        }

        public Boolean IsShowPassword
        {
            get
            {
                return isShowPassword;
            }

            set
            {
                isShowPassword = value;
                if (value)
                {
                    PasswordTextBox.Visibility = System.Windows.Visibility.Collapsed;
                    TextBox.Visibility = System.Windows.Visibility.Visible;
                    image.Source = ImageHelper.GenerateImage("IconPassword_X16.png");
                }
                else
                {
                    PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
                    TextBox.Visibility = System.Windows.Visibility.Collapsed;
                    image.Source = ImageHelper.GenerateImage("IconLoopPassword_X16.png");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsShowPassword)
            {
                PasswordTextBox.Focus();
                PasswordTextBox.Password = TextPassword;
                PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
                TextBox.Visibility = System.Windows.Visibility.Collapsed;
                image.Source = ImageHelper.GenerateImage("IconLoopPassword_X16.png");
                IsShowPassword = false;
                

            }
            else
            {
                TextBox.Focus();
                TextPassword = PasswordTextBox.Password;
                PasswordTextBox.Visibility = System.Windows.Visibility.Collapsed;
                TextBox.Visibility = System.Windows.Visibility.Visible;
                image.Source = ImageHelper.GenerateImage("IconPassword_X16.png");
                IsShowPassword = true;
                
            }
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //TextPassword = PasswordTextBox.Password;
            ButtonTextChangedClick?.Invoke();
        }
    }
}
