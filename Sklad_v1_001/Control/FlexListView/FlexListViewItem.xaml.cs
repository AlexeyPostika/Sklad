using System;
using System.Collections.Generic;
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
    public class RowListView
    {
        public Int64 ID { get; set; }
        public String Description { get; set; }
        public ImageSource Icon { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для FlexListViewItem.xaml
    /// </summary>
    public partial class FlexListViewItem : UserControl
    {
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
            MarginListView = new Thickness(5 + this.search.wrapPanel.ActualWidth, 0, 5, 5);
            this.DataContext = this;
        }

        private void search_ButtonClick()
        {            
            TextSearch = Text.Filter();
            ButtonSearch?.Invoke();
        }

        private void list_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RowListView row = list.SelectedItem as RowListView;
            if (row != null)
                ButtonSelectClick?.Invoke(row.ID);
        }

    }
}
