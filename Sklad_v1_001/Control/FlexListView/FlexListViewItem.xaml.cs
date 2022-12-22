using Sklad_v1_001.GlobalList;
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

namespace Sklad_v1_001.Control.FlexListView
{
    public static class Extensions
    {
        public static string Filter(this string str)
        {
            List<char> charsToRemove = new List<char>() { ',', '.', ' ', '-', '@', ':', ';', '_', '&', '#', '$' };           
            Boolean isActive = false;

            foreach (char c in charsToRemove)
            {
                if (c.GetHashCode() == ' '.GetHashCode() && !isActive)
                {
                    str = str.Replace(c.ToString(), "+").Replace(",", "+").Replace("++", "+");
                    isActive = true;
                }
                if (c.GetHashCode() == ','.GetHashCode() && !isActive)
                {
                    str = str.Replace(c.ToString(), "+").Replace(" ", "+").Replace("++", "+");
                    isActive = true;
                }
                if (isActive)
                {
                    str = str.Replace(c.ToString(), String.Empty);
                }
            }

            return str;
        }
    }   
    /// <summary>
    /// Логика взаимодействия для FlexListViewItem.xaml
    /// </summary>
    public partial class FlexListViewItem : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Text
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(FlexListViewItem), new UIPropertyMetadata(String.Empty));
        //Text
        // свойство зависимостей
        public static readonly DependencyProperty TextSearchProperty = DependencyProperty.Register(
                        "TextSearch",
                        typeof(string),
                        typeof(FlexListViewItem), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(string),
                        typeof(FlexListViewItem), new UIPropertyMetadata(String.Empty));
        //
        // свойство зависимостей
        public static readonly DependencyProperty MarginListViewProperty = DependencyProperty.Register(
                        "MarginListView",
                        typeof(Thickness),
                        typeof(FlexListViewItem), new UIPropertyMetadata(new Thickness(10,0,0,5)));
        // свойство зависимостей
        public static readonly DependencyProperty ListDataProperty = DependencyProperty.Register(
                        "ListData",
                        typeof(List<RowListView>),
                        typeof(FlexListViewItem), new UIPropertyMetadata(new List<RowListView>()));
        // свойство зависимостей
        public static readonly DependencyProperty IsOpenPopupProperty = DependencyProperty.Register(
                        "IsOpenPopup",
                        typeof(Boolean),
                        typeof(FlexListViewItem), new UIPropertyMetadata(false));
        
        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(FlexListViewItem), new UIPropertyMetadata(Visibility.Collapsed));

        // свойство зависимостей
        public static readonly DependencyProperty HorizontalTextAlignmentProperty = DependencyProperty.Register(
                        "HorizontalTextAlignment",
                        typeof(HorizontalAlignment),
                        typeof(FlexListViewItem), new UIPropertyMetadata(HorizontalAlignment.Left));

        // свойство зависимостей
        public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(
                        "AcceptReturn",
                        typeof(Boolean),
                        typeof(FlexListViewItem), new UIPropertyMetadata(false));

        // свойство зависимостей
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                        "MaxLength",
                        typeof(Int32),
                        typeof(FlexListViewItem), new UIPropertyMetadata(50));
        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string TextSearch
        {
            get { return (string)GetValue(TextSearchProperty); }
            set { SetValue(TextSearchProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value);}
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public Thickness MarginListView
        {
            get { return (Thickness)GetValue(MarginListViewProperty); }
            set { SetValue(MarginListViewProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public List<RowListView> ListData
        {
            get { return (List<RowListView>)GetValue(ListDataProperty); }
            set { SetValue(ListDataProperty, value); }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей     
        public Boolean IsOpenPopup
        {
            get { return (Boolean)GetValue(IsOpenPopupProperty); }
            set { SetValue(IsOpenPopupProperty, value); }
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
        public event Action ButtonSearch;
        public delegate void ButtonSelectClickHandler(Int64 text);
        public event ButtonSelectClickHandler ButtonSelectClick;

        public FlexListViewItem()
        {
            InitializeComponent();
            this.list.ItemsSource = ListData;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MarginListView = new Thickness(5 + this.wrapPanel.ActualWidth, 0, 5, 5);
            this.DataContext = this;
        }

        private void list_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RowListView row = list.SelectedItem as RowListView;
            if (row != null)
                ButtonSelectClick?.Invoke(row.ID);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextSearch = Text.Filter();
            ButtonSearch?.Invoke();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextSearch = Text.Filter() + "+";
                ButtonSearch?.Invoke();
            }
        }
    }
}
