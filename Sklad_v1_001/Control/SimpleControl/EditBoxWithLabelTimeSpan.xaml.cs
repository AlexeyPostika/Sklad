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
    /// Логика взаимодействия для EditBoxWithLabelTimeSpan.xaml
    /// </summary>
    public partial class EditBoxWithLabelTimeSpan : UserControl, INotifyPropertyChanged
    {
        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(String),
                        typeof(EditBoxWithLabelTimeSpan));
        public String LabelText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty VisibilitySpanProperty = DependencyProperty.Register(
                        "VisibilitySpan",
                        typeof(Visibility),
                        typeof(EditBoxWithLabelTimeSpan), new UIPropertyMetadata(Visibility.Visible));
        public Visibility VisibilitySpan
        {
            get
            {
                return (Visibility)GetValue(VisibilitySpanProperty);
            }
            set
            {
                SetValue(VisibilitySpanProperty, value);
                OnPropertyChanged("VisibilitySpan");
                Column1.Width = value == Visibility.Visible ? new GridLength(18) : new GridLength(0);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
                        "LabelWidth",
                        typeof(double),
                        typeof(EditBoxWithLabelTimeSpan), new UIPropertyMetadata());
        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsErrorProperty = DependencyProperty.Register(
                        "IsError",
                        typeof(Boolean),
                        typeof(EditBoxWithLabelTimeSpan), new UIPropertyMetadata());
        public Boolean IsError
        {
            get { return (Boolean)GetValue(IsErrorProperty); }
            set { SetValue(IsErrorProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty MaskProperty = DependencyProperty.Register(
                        "Mask",
                        typeof(String),
                        typeof(EditBoxWithLabelTimeSpan));
        public String Mask
        {
            get { return (String)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty TimeStringProperty = DependencyProperty.Register(
                        "TimeString",
                        typeof(String),
                        typeof(EditBoxWithLabelTimeSpan));
        public String TimeString
        {
            get { return (String)GetValue(TimeStringProperty); }
            set { SetValue(TimeStringProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty ResultTimeStringProperty = DependencyProperty.Register(
                        "ResultTimeString",
                        typeof(String),
                        typeof(EditBoxWithLabelTimeSpan));
        public String ResultTimeString
        {
            get { return (String)GetValue(ResultTimeStringProperty); }
            set { SetValue(ResultTimeStringProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditBoxWithLabelTimeSpan()
        {
            InitializeComponent();
        }

        private void Button_Click_Up(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Down(object sender, RoutedEventArgs e)
        {

        }
    }
}
