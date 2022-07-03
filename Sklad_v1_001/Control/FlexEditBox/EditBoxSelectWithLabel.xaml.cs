using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace POS.FlexControls.FlexEditBox
{
    /// <summary>
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class EditBoxSelectWithLabel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(EditBoxSelectWithLabel), new UIPropertyMetadata(0));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get { return (Int32)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register(
                        "DisplayValue",
                        typeof(string),
                        typeof(EditBoxSelectWithLabel), new UIPropertyMetadata(String.Empty));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string DisplayValue
        {
            get { return (string)GetValue(DisplayValueProperty); }
            set { SetValue(DisplayValueProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxSelectWithLabel), new UIPropertyMetadata(Visibility.Collapsed));

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

        public EditBoxSelectWithLabel()
        {
            InitializeComponent();
        }

        public event Action ButtonSelectClick;
        public event Action ButtonLostFocusSelectClick;


        public Boolean IsReadOnly
        {
            get
            {
                return EditBoxSelect.TextField.IsReadOnly;
            }

            set
            {
                EditBoxSelect.TextField.IsReadOnly = value;
            }
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

        private void EditBoxSelect_ButtonSelectClick()
        {
            if (ButtonSelectClick != null)
            {
                ButtonSelectClick();
            }
        }

        private void EditBoxSelect_ButtonLostFocusClick()
        {
            if (ButtonLostFocusSelectClick != null)
            {
                ButtonLostFocusSelectClick();
            }
        }
    }
}
