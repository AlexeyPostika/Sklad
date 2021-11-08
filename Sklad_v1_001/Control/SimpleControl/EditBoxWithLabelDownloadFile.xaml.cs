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
    /// Логика взаимодействия для EditBoxWithLabelDownloadFile.xaml
    /// </summary>
    public partial class EditBoxWithLabelDownloadFile : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(Visibility.Collapsed));
        
        // свойство зависимостей
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                        "MaxLength",
                        typeof(Int32),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(50));
        
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                        "Source",
                        typeof(ImageSource),
                       typeof(EditBoxWithLabelDownloadFile));

        // Обычное свойство .NET  - обертка над свойством зависимостей

        public ImageSource Source
        {
            get
            {
                return (ImageSource)this.button.Image.Source;
            }
            set
            {
                this.button.Image.Source = value as ImageSource;
                OnPropertyChanged("Source");
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged("Text");
            }
        }

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
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 MaxLength
        {
            get
            {
                return (Int32)GetValue(MaxLengthProperty);
            }
            set
            {
                SetValue(MaxLengthProperty, value);
                OnPropertyChanged("MaxLength");
            }
        }
        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.wrapPanel.Width;
            }

            set
            {
                this.wrapPanel.Width = value;
            }
        }
        public String ImageSource
        {
            set
            {
                this.button.Image.Source = new BitmapImage(new Uri(value, UriKind.Relative));
            }
            get
            {
                return null;
            }
        }
        public event Action ButtonAddClick;
        public EditBoxWithLabelDownloadFile()
        {
            InitializeComponent();
            this.button.Image.Source= ImageHelper.GenerateImage("IconAddProduct.png");
        }
        private void button_ButtonClick()
        {
            ButtonAddClick?.Invoke();
        }
    }
}
