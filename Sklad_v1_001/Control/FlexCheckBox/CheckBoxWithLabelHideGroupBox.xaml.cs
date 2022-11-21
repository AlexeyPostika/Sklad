using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CheckBoxWithLabelHideGroupBox.xaml
    /// </summary>
    public partial class CheckBoxWithLabelHideGroupBox : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
                        "IsChecked",
                        typeof(Boolean),
                        typeof(CheckBoxWithLabelHideGroupBox), new UIPropertyMetadata(false));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public delegate void ButtonCheckedClickHandler(object sender, RoutedEventArgs e);
        public delegate void ButtonUnCheckedClickHandler(object sender, RoutedEventArgs e);
        public event ButtonCheckedClickHandler ButtonCheckedClick;
        public event ButtonUnCheckedClickHandler ButtonUnCheckedClick;
        public CheckBoxWithLabelHideGroupBox()
        {
            InitializeComponent();
        }

        private string key;

        public string LabelText
        {
            get
            {
                return CheckBoxLabel.Content.ToString();
            }

            set
            {
                CheckBoxLabel.Content = value;
            }
        }

        public Boolean CheckBoxIsEnabled
        {
            get
            {
                return CheckBox.IsEnabled;
            }

            set
            {
                CheckBox.IsEnabled = value;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox.IsChecked = !CheckBox.IsChecked;
        }

        private void CheckBox_Unchecked()
        {
            ButtonUnCheckedClick?.Invoke(this, new RoutedEventArgs());
        }

        private void CheckBox_Checked()
        {
            ButtonCheckedClick?.Invoke(this, new RoutedEventArgs());
        }
    }
}
