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


namespace Sklad_v1_001.Control.Toolbar
{
    /// <summary>
    /// Логика взаимодействия для DataViewToolBar.xaml
    /// </summary>
    public partial class Close : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
                       "HeaderText",
                       typeof(String),
                       typeof(Close), new UIPropertyMetadata(""));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String HeaderText
        {
            get
            {
                return (String)GetValue(HeaderTextProperty);
            }
            set
            {

                SetValue(HeaderTextProperty, value);
                OnPropertyChanged("HeaderText");
            }
        }

        public static readonly DependencyProperty ButtonClearFiltersVisibleProperty = DependencyProperty.Register(
                   "ButtonClearFiltersVisible",
                   typeof(Visibility),
                   typeof(Close), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility ButtonClearFiltersVisible
        {
            get
            {
                return (Visibility)GetValue(ButtonClearFiltersVisibleProperty);
            }
            set
            {

                SetValue(ButtonClearFiltersVisibleProperty, value);
                OnPropertyChanged("ButtonClearFiltersVisible");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Close()
        {
            InitializeComponent();
            ButtonClearFilters.Image.Source = ImageHelper.GenerateImage("IconClearAllFilters.png");
            ButtonClose.Image.Source = ImageHelper.GenerateImage("IconClose.png");
        }

        public event Action ButtonClearFiltersClick;
        public event Action ButtonCloseClick;

        private void ButtonClearFilters_ButtonClick()
        {
            ButtonClearFiltersClick?.Invoke();
        }
        private void ButtonClose_ButtonClick()
        {
            ButtonCloseClick?.Invoke();
        }
    }
}
