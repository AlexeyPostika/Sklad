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

namespace Sklad_v1_001.Control.Zona.Green
{
    /// <summary>
    /// Логика взаимодействия для SelectCategoryTable.xaml
    /// </summary>
    public partial class SelectCategoryGreenTable : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty LabelNameTextProperty = DependencyProperty.Register(
                        "LabelNameText",
                        typeof(String),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelNameText
        {
            get
            {
                return (String)GetValue(LabelNameTextProperty);
            }
            set
            {
                SetValue(LabelNameTextProperty, value);
            }
        }
        //Value
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                       "Value",
                       typeof(String),
                       typeof(SelectCategoryGreenTable), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Value
        {
            get
            {
                return (String)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty WidthLabelProperty = DependencyProperty.Register(
                        "WidthLabel",
                        typeof(Int32),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthLabel
        {
            get
            {
                return (Int32)GetValue(WidthLabelProperty);
            }
            set
            {
                SetValue(WidthLabelProperty, value);
            }
        }
        // свойство зависимостей
        public static readonly DependencyProperty WidthTextBoxProperty = DependencyProperty.Register(
                        "WidthTextBox",
                        typeof(Int32),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthTextBox
        {
            get
            {
                return (Int32)GetValue(WidthTextBoxProperty);
            }
            set
            {
                SetValue(WidthTextBoxProperty, value);
            }
        }


        // свойство зависимостей
        public static readonly DependencyProperty LabelNameTextDescriptionProperty = DependencyProperty.Register(
                        "LabelNameTextDescription",
                        typeof(String),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelNameTextDescription
        {
            get
            {
                return (String)GetValue(LabelNameTextDescriptionProperty);
            }
            set
            {
                SetValue(LabelNameTextDescriptionProperty, value);
            }
        }
        //Value
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
                       "Description",
                       typeof(String),
                       typeof(SelectCategoryGreenTable), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String Description
        {
            get
            {
                return (String)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty WidthLabelDescriptionProperty = DependencyProperty.Register(
                        "WidthLabelDescription",
                        typeof(Int32),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthLabelDescription
        {
            get
            {
                return (Int32)GetValue(WidthLabelDescriptionProperty);
            }
            set
            {
                SetValue(WidthLabelDescriptionProperty, value);
            }
        }

        public static readonly DependencyProperty HeightLabelDescriptionProperty = DependencyProperty.Register(
                        "HeightLabelDescription",
                        typeof(Int32),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(20));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 HeightLabelDescription
        {
            get
            {
                return (Int32)GetValue(HeightLabelDescriptionProperty);
            }
            set
            {
                SetValue(HeightLabelDescriptionProperty, value);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty WidthTextBoxDescriptionProperty = DependencyProperty.Register(
                        "WidthTextBoxDescription",
                        typeof(Int32),
                        typeof(SelectCategoryGreenTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthTextBoxDescription
        {
            get
            {
                return (Int32)GetValue(WidthTextBoxDescriptionProperty);
            }
            set
            {
                SetValue(WidthTextBoxDescriptionProperty, value);
            }
        }

        public SelectCategoryGreenTable()
        {
            InitializeComponent();
        }
        public event Action ButtonSelectChanged;
        public event Action ButtonInputTextBox;
        public event Action ButtonInputTextBoxDescription;
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSelectChanged?.Invoke();
        }

        private void textBox_TextInput(object sender, TextCompositionEventArgs e)
        {
           
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = textBox.Text;
            ButtonInputTextBox?.Invoke();
        }

        private void textBoxDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            Description = textBoxDescription.Text;
            ButtonInputTextBoxDescription?.Invoke();
        }

        private void textBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
