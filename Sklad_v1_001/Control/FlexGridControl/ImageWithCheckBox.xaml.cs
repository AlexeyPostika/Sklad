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
    public partial class ImageWithCheckBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
                        "IsChecked",
                        typeof(Boolean),
                        typeof(ImageWithCheckBox), new UIPropertyMetadata(false, IsCheckedPropertyChanged));

        private static void IsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageWithCheckBox instance = (ImageWithCheckBox)d;
            instance.ImageSource = instance.IsChecked ? ImageHelper.GenerateImage("IconPaymentOK.png") : ImageHelper.GenerateImage("IconPaymentCancel.png");            
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsChecked
        {
            get
            {
                return (Boolean)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);                              
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsNeedImageProperty = DependencyProperty.Register(
                        "IsNeedImage",
                        typeof(Boolean),
                        typeof(ImageWithCheckBox), new UIPropertyMetadata(false, IsNeedImagePropertyChanged));

        private static void IsNeedImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageWithCheckBox instance = (ImageWithCheckBox)d;
            instance.ImageSource = instance.IsChecked ? ImageHelper.GenerateImage("IconPaymentOK.png") : ImageHelper.GenerateImage("IconPaymentCancel.png");
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean IsNeedImage
        {
            get
            {
                return (Boolean)GetValue(IsNeedImageProperty);
            }
            set
            {
                
                SetValue(IsNeedImageProperty, value);                
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
                        "ImageSource",
                        typeof(ImageSource),
                        typeof(ImageWithCheckBox));


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
                        typeof(ImageWithCheckBox), new UIPropertyMetadata(String.Empty));


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

        public ImageWithCheckBox()
        {
            InitializeComponent();            
        }
    }
}
