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

namespace Sklad_v1_001.Control.FlexCheckBox
{
    /// <summary>
    /// Логика взаимодействия для SwitchWithLabel.xaml
    /// </summary>
    public partial class SwitchWithLabel : UserControl, INotifyPropertyChanged
    {
        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(SwitchWithLabel), new UIPropertyMetadata(""));

        // свойство зависимостей
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
                        "IsChecked",
                        typeof(Boolean),
                        typeof(SwitchWithLabel), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
                OnPropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Key { get; set; }

        public event Action Checked;
        public event Action UnChecked;

        public SwitchWithLabel()
        {
            InitializeComponent();
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            IsChecked = true;
            Checked?.Invoke();
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            IsChecked = false;
            UnChecked?.Invoke();
        }

        private void toggleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            toggleButton.Tag = "true";
        }
    }
}
