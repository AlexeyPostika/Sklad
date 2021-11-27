using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sklad_v1_001.Control.FlexComboBox
{
    /// <summary>
    /// Логика взаимодействия для ComboBoxWithLabel.xaml
    /// </summary>
    public partial class ComboBoxWithLabel : UserControl, INotifyPropertyChanged
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(ComboBoxWithLabel), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get
            {
                return (Int32)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
                OnPropertyChanged("Value");
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(ComboBoxWithLabel), new UIPropertyMetadata(Visibility.Collapsed));

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
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
                        "Item",
                        typeof(object),
                        typeof(ComboBoxWithLabel));
        //Item

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public object Item
        {
            get
            {
                return (object)GetValue(ItemProperty);
            }
            set
            {
                SetValue(ItemProperty, value);
                OnPropertyChanged("Item");
            }
        }


        // свойство зависимостей
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
                        "Description",
                        typeof(String),
                        typeof(ComboBoxWithLabel), new UIPropertyMetadata("Description"));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Description
        {
            get
            {
                return (String)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
                OnPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ComboBoxWithLabel()
        {
            InitializeComponent();
        }

        public event Action SelectTextInput;
        public event Action DropDownClosed;
        public delegate void SelectionChangedHandler(object sender = null, RoutedEventArgs e = null);
        public event SelectionChangedHandler SelectionChanged;

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

        private void ComboBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(sender, e);
        }

        private void ComboBoxElement_TextInput(object sender, TextCompositionEventArgs e)
        {
            SelectTextInput?.Invoke();
        }

        private void ComboBoxElement_DropDownClosed(object sender, EventArgs e)
        {
            DropDownClosed?.Invoke();
        }
    }
}
