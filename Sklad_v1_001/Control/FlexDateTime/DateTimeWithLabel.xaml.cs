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

namespace Sklad_v1_001.Control.FlexDateTime
{
    /// <summary>
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class DateTimeWithLabel : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(DateTimeWithLabel), new UIPropertyMetadata(Visibility.Collapsed));


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
        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(
                        "SelectedDate",
                        typeof(DateTime),
                        typeof(DateTimeWithLabel), new UIPropertyMetadata(DateTime.Now.AddYears(-1)));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }
        public DateTimeWithLabel()
        {
            InitializeComponent();            
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

        private void CurrentDayButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedDate = DateTime.Now;
        }
    }
}
