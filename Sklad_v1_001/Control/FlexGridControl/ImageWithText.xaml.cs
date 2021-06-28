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

namespace Sklad_v1_001.Control.FlexGridControl
{
    /// <summary>
    /// Логика взаимодействия для ButtonAdd.xaml
    /// </summary>
    public partial class ImageWithText : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                        "ImageSource",
                        typeof(ImageSource),
                        typeof(ImageWithText));


        // Обычное свойство .NET  - обертка над свойством зависимостей
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);                
            }
        }      

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(string),
                        typeof(ImageWithText), new UIPropertyMetadata(String.Empty));


        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string LabelText
        {
            get
            {
                return (string)GetValue(LabelTextProperty);
            }
            set
            {
                SetValue(LabelTextProperty, value);                
            }
        }

        public ImageWithText()
        {
            InitializeComponent();        
        }
    }
}
