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
    /// Логика взаимодействия для EditBoxWithLabel.xaml
    /// </summary>
    public partial class EditBoxWithLabel : UserControl, INotifyPropertyChanged
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
                        typeof(EditBoxWithLabel), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxWithLabel), new UIPropertyMetadata(Visibility.Collapsed));

        // свойство зависимостей
        public static readonly DependencyProperty HorizontalTextAlignmentProperty = DependencyProperty.Register(
                        "HorizontalTextAlignment",
                        typeof(HorizontalAlignment),
                        typeof(EditBoxWithLabel), new UIPropertyMetadata(HorizontalAlignment.Left));

        // свойство зависимостей
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(
                        "AcceptReturn",
                        typeof(Boolean),
                        typeof(EditBoxWithLabel), new UIPropertyMetadata(false));

        // свойство зависимостей
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                        "MaxLength",
                        typeof(Int32),
                        typeof(EditBoxWithLabel), new UIPropertyMetadata(50));

        public EditBoxWithLabel()
        {
            InitializeComponent();
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

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public HorizontalAlignment HorizontalTextAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(HorizontalTextAlignmentProperty);
            }
            set
            {
                SetValue(HorizontalTextAlignmentProperty, value);
                OnPropertyChanged("HorizontalTextAlignment");
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Boolean AcceptsReturn
        {
            get { return (Boolean)GetValue(AcceptsReturnProperty); }
            set
            {
                SetValue(AcceptsReturnProperty, value);
            }
        }

        public event Action ButtonClick;

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

        public Boolean IsReadOnly
        {
            get
            {
                return this.TextBox.IsReadOnly;
            }

            set
            {
                this.TextBox.IsReadOnly = value;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonClick?.Invoke();
        }

    }
}
