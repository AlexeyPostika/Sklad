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
    public partial class FlexGridFilterCheckbox : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(FlexGridFilterCheckbox), new UIPropertyMetadata(0));

        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(
                        "IsChecked",
                        typeof(Boolean),
                        typeof(FlexGridFilterCheckbox), new UIPropertyMetadata(false));

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

        public string IsChecked
        {
            get { return (string)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        
        public event Action ButtonFilterSelected;
        public event Action ButtonAllCheck;

        public FlexGridFilterCheckbox()
        {
            InitializeComponent();
        }

        private void FilterID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox rolefilter = sender as ComboBox;

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
                    }
                }
            }
            ButtonFilterSelected?.Invoke();
        }

        public string LabelText
        {
            get
            {
                return this.ControlLabel.Content.ToString();
            }

            set
            {
                this.ControlLabel.Content = value;
            }
        }

        public Visibility CheckBoxVisibility
        {
            get
            {
                return this.CheckAll.Visibility;
            }

            set
            {
                this.CheckAll.Visibility = value;
            }
        }

        public Boolean CheckBoxIsEnabled
        {
            get
            {
                return this.CheckAll.IsEnabled;
            }

            set
            {
                this.CheckAll.IsEnabled = value;
            }
        }

        private void CheckAll_Checked(object sender, RoutedEventArgs e)
        {
            ButtonAllCheck?.Invoke();
        }

        private void CheckAll_Unchecked(object sender, RoutedEventArgs e)
        {
            ButtonAllCheck?.Invoke();
        }

        private void FilterID_Loaded(object sender, RoutedEventArgs e)
        {
            //ActiveListFilter filter = new ActiveListFilter();
            //FilterID.ItemsSource = filter.innerList;
        }
    }
}
