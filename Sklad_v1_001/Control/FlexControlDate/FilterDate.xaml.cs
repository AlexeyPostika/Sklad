using Sklad_v1_001.GlobalList;
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
namespace Sklad_v1_001.Control.FlexControlDate
{
    /// <summary>
    /// Interaction logic for FilterDate.xaml
    /// </summary>
    public partial class FilterDate : UserControl
    {
        // свойство зависимостей
         public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                         "Value",
                         typeof(Int32),
                         typeof(FilterDate), new UIPropertyMetadata(0));
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
             }
         }
         // свойство зависимостей
         // заголовок фильтра
         public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                         "LabelText",
                         typeof(string),
                         typeof(FilterDate), new UIPropertyMetadata(Properties.Resources.CreatedDate));
         // Обычное свойство .NET  - обертка над свойством зависимостей
         // заголовок фильтра
         public string LabelText
         {
             get
             {
                 return (string)GetValue(LabelTextProperty);
             }
             set
             {
                 SetValue(LabelTextProperty, value);
             }
         }
         public string FilterText
         {
             get
             {
                 return LabelText;
             }
             set
             {
                 throw new NotImplementedException();
             }
         }
         public string FilterValue
         {
             get
             {
                 return FilterID.SelectedValue.ToString();
             }
             set
             {
                 throw new NotImplementedException();
             }
         }
         public string FilterDescription
         {
             get
             {
                 DateTimeItem comboboxSelectedItem = (DateTimeItem)FilterID.SelectedItem;
                 return comboboxSelectedItem.Description.ToString();
             }
             set
             {
                 throw new NotImplementedException();
             }
         }
         public Boolean IsChecked
         {
             get
             {
                 throw new NotImplementedException();
             }
             set
             {
                 throw new NotImplementedException();
             }
         }
         public delegate void ButtonClickHandler();
         public event ButtonClickHandler ButtonFilterSelected;
        public FilterDate()
        {
            InitializeComponent();
        }
         private void FilterID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox rolefilter = sender as ComboBox;
             if (rolefilter != null)
             {
                 ControlTemplate test = rolefilter.Template as ControlTemplate;
                 Grid MainGrid = test.FindName("MainGrid", rolefilter) as Grid;
                 if (MainGrid != null)
                 {
                     System.Windows.Controls.Primitives.ToggleButton button = MainGrid.Children[1] as System.Windows.Controls.Primitives.ToggleButton;
                     ControlTemplate buttonTemplate = button.Template as ControlTemplate;
                     Image filterImage = buttonTemplate.FindName("FilterImage", button) as Image;
                     if (rolefilter != null && rolefilter.SelectedValue != null)
                     {
                         if (Value != 0)
                         {
                            //@"Icone\Controllu\clear_filters_30px.png";
                            filterImage.Source = new BitmapImage(new Uri(@"..\Icone\Controllu\clear_filters_30px.png", UriKind.Relative));
                        }
                         if (Value == 0)
                         {
                             filterImage.Source = new BitmapImage(new Uri(@"..\Icone\Controllu\funnel_30px.png", UriKind.Relative));
                            ButtonFilterSelected?.Invoke();
                         }
                     }
                 }
             }
        }
         private void FilterID_Loaded(object sender, RoutedEventArgs e)
        {
             DateTimeListFilter filter = new DateTimeListFilter();//создать лист
             FilterID.ItemsSource = filter.innerList;
        }
         private void FilterID_DropDownClosed(object sender, EventArgs e)
        {
            if (Value != 0)
             {
                 ButtonFilterSelected?.Invoke();
             }
        }
    }
}
