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
                PasswordTextBox.Password = TextPassword;

                PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
                TextBox.Visibility = System.Windows.Visibility.Collapsed;
                image.Source = ImageHelper.GenerateImage("IconLoopPassword_X16.png");
                IsShowPassword = false;
                PasswordTextBox.Focus();

            }
            else
            {
                TextPassword = PasswordTextBox.Password;
                PasswordTextBox.Visibility = System.Windows.Visibility.Collapsed;
                TextBox.Visibility = System.Windows.Visibility.Visible;

                image.Source = ImageHelper.GenerateImage("IconPassword_X16.png");
                IsShowPassword = true;
                TextBox.Focus();
            }
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextPassword = PasswordTextBox.Password;
            ButtonTextChangedClick?.Invoke();
        }
    }
}
