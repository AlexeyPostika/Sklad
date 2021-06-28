using Sklad_v1_001.GlobalVariable;
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

namespace Sklad_v1_001.Control.FlexFilter
{
    /// <summary>
    /// Логика взаимодействия для EditBoxDelete.xaml
    /// </summary>
    public partial class FlexGridFilteDay : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(FlexGridFilteDay), new UIPropertyMetadata(0));


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
                        typeof(FlexGridFilteDay), new UIPropertyMetadata(Properties.Resources.CreatedDate));

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

        //public string FilterDescription
        //{
        //    get
        //    {
        //        //WeekdayList comboboxSelectedItem = (WeekdayList)FilterID.SelectedItem;
        //        return comboboxSelectedItem.Description.ToString();                
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

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

        public FlexGridFilteDay()
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
                            filterImage.Source = ImageHelper.GenerateImage("IconClearFilter.png");
                        }

                        if (Value == 0)
                        {
                            filterImage.Source = ImageHelper.GenerateImage("IconFilter.png");
                            ButtonFilterSelected?.Invoke();
                        }
                    }
                }               
            }
        }

        private void FilterID_Loaded(object sender, RoutedEventArgs e)
        {
            //WeekdayListF filter = new WeekdayListF();
            //FilterID.ItemsSource = filter.innerList;
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
