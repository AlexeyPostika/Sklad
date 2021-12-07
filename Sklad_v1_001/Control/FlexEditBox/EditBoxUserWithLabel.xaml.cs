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

namespace Sklad_v1_001.Control.FlexEditBox
{
    /// <summary>
    /// Логика взаимодействия для EditBoxUserWithLabel.xaml
    /// </summary>
    public partial class EditBoxUserWithLabel : UserControl, INotifyPropertyChanged
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
                        typeof(EditBoxUserWithLabel), new UIPropertyMetadata(String.Empty));
        //VisisbilityImageSim
        public static readonly DependencyProperty VisisbilityImageSimProperty = DependencyProperty.Register(
                       "VisisbilityImageSim",
                       typeof(Visibility),
                       typeof(EditBoxUserWithLabel), new UIPropertyMetadata(Visibility.Visible));
        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxUserWithLabel), new UIPropertyMetadata(Visibility.Collapsed));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Visibility VisisbilityImageSim
        {
            get { return (Visibility)GetValue(VisisbilityImageSimProperty); }
            set { SetValue(VisisbilityImageSimProperty, value); }
        }
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
        public EditBoxUserWithLabel()
        {
            InitializeComponent();
        }

        public event Action ButtonClearClick;
        public event Action ButtonTextChangedClick;

        public Boolean IsReadOnly
        {
            get
            {
                return this.EditBoxUser.TextField.IsReadOnly;
            }

            set
            {
                this.EditBoxUser.TextField.IsReadOnly = value;
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

        private void EditBoxUser_ButtonClearClick()
        {
            ButtonClearClick?.Invoke();
        }

        private void EditBoxUser_ButtonTextChangedClick()
        {
            ButtonTextChangedClick?.Invoke();
        }
    }
}
