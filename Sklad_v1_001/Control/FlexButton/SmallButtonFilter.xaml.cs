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

namespace Sklad_v1_001.Control.FlexButton
{
    /// <summary>
    /// Логика взаимодействия для SmallButtonFilter.xaml
    /// </summary>
    public partial class SmallButtonFilter : UserControl
    {
        public SmallButtonFilter()
        {
           InitializeComponent();
        }

        public event Action ButtonClick;
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                        "ImageSource",
                        typeof(ImageSource),
                       typeof(SmallButtonFilter));

        // Обычное свойство .NET  - обертка над свойством зависимостей

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)this.Image.Source;
            }
            set
            {
                this.Image.Source = value as ImageSource;
                OnPropertyChanged("ImageSource");
            }
        }

        public static readonly DependencyProperty IsImageEnableProperty = DependencyProperty.Register(
                       "IsImageEnable",
                       typeof(Boolean),
                       typeof(SmallButtonFilter), new PropertyMetadata(true));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsImageEnable
        {
            get
            {
                return (Boolean)GetValue(IsImageEnableProperty);
            }
            set
            {
                SetValue(IsImageEnableProperty, value);
                OnPropertyChanged("IsImageEnable");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)

            {
                ButtonClick();
            }
        }
    }
}
