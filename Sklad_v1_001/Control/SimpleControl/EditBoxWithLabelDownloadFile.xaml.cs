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
        //SourceLoop
        public static readonly DependencyProperty SourceLoopProperty = DependencyProperty.Register(
                        "SourceLoop",
                        typeof(ImageSource),
                       typeof(EditBoxWithLabelDownloadFile));

        //
        public static readonly DependencyProperty SourceCleareProperty = DependencyProperty.Register(
                       "SourceCleare",
                       typeof(ImageSource),
                      typeof(EditBoxWithLabelDownloadFile));

        public static readonly DependencyProperty VisibilityCleareProperty = DependencyProperty.Register(
                       "VisibilityCleare",
                       typeof(Visibility),
                      typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty VisibilityLoopProperty = DependencyProperty.Register(
                      "VisibilityLoop",
                      typeof(Visibility),
                     typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));
      
        public static readonly DependencyProperty VisibilityAddProperty = DependencyProperty.Register(
                     "VisibilityAdd",
                     typeof(Visibility),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty IsEnableCleareProperty = DependencyProperty.Register(
                       "IsEnableCleare",
                       typeof(Boolean),
                      typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableLoopProperty = DependencyProperty.Register(
                      "IsEnableLoop",
                      typeof(Boolean),
                     typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
                     "IsEnableAdd",
                     typeof(Boolean),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(true));
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

        public ImageSource SourceLoop
        {
            get
            {
                return (ImageSource)this.buttonLoop.Image.Source;
            }
            set
            {
                this.buttonLoop.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceLoop");
            }
        }
        public ImageSource SourceCleare
        {
            get
            {
                return (ImageSource)this.buttonClear.Image.Source;
            }
            set
            {
                this.buttonClear.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceCleare");
            }
        }

        public Visibility VisibilityCleare
        {
            get { return (Visibility)GetValue(VisibilityCleareProperty); }
            set { SetValue(VisibilityCleareProperty, value); }         
            
        }
        public Visibility VisibilityLoop
        {
            get { return (Visibility)GetValue(VisibilityLoopProperty); }
            set { SetValue(VisibilityLoopProperty, value); }               
        }
        //VisibilityAdd
        public Visibility VisibilityAdd
        {
            get { return (Visibility)GetValue(VisibilityAddProperty); }
            set { SetValue(VisibilityAddProperty, value); }
        }

        public Boolean IsEnableCleare
        {
            get { return (Boolean)GetValue(IsEnableCleareProperty); }
            set { SetValue(IsEnableCleareProperty, value); }

        }
        public Boolean IsEnableLoop
        {
            get { return (Boolean)GetValue(IsEnableLoopProperty); }
            set { SetValue(IsEnableLoopProperty, value); }
        }
        //VisibilityAdd
        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
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
        public event Action ButtonLoopClick;
        public event Action ButtonClearClick;
        public EditBoxWithLabelDownloadFile()
        {
            InitializeComponent();
            this.button.Image.Source= ImageHelper.GenerateImage("IconAddProduct.png");
            this.buttonLoop.Image.Source = ImageHelper.GenerateImage("IconSearchPage.png");
            this.buttonClear.Image.Source = ImageHelper.GenerateImage("IconEraseFaile.png");
        }
        private void button_ButtonClick()
        {
            ButtonAddClick?.Invoke();
        }

        private void buttonLoop_ButtonClick()
        {
            ButtonLoopClick?.Invoke();
        }  

        private void buttonClear_ButtonClick()
        {
            ButtonClearClick?.Invoke();
        }
    }
}
