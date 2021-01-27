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

namespace Sklad_v1_001.Control.FlexControlDescription
{
    /// <summary>
    /// Логика взаимодействия для Description4TextBlock.xaml
    /// </summary>
    public partial class Description4TextBlock : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueAdressProperty = DependencyProperty.Register(
                        "ValueAdress",
                        typeof(String),
                        typeof(Description4TextBlock), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String ValueAdress
        {
            get
            {
                return (String)GetValue(ValueAdressProperty);
            }
            set
            {
                SetValue(ValueAdressProperty, value);
            }
        }
        // свойство зависимостей
        public static readonly DependencyProperty WidthLabelProperty = DependencyProperty.Register(
                        "WidthLabel",
                        typeof(Int32),
                        typeof(Description4TextBlock), new UIPropertyMetadata(120));
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
        public static readonly DependencyProperty ValueTypeBeliveryProperty = DependencyProperty.Register(
                        "ValueTypeBelivery",
                        typeof(String),
                        typeof(Description4TextBlock), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String ValueTypeBelivery
        {
            get
            {
                return (String)GetValue(ValueTypeBeliveryProperty);
            }
            set
            {
                SetValue(ValueTypeBeliveryProperty, value);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty ValueNumvberPhonProperty = DependencyProperty.Register(
                        "ValueNumvberPhon",
                        typeof(String),
                        typeof(Description4TextBlock), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String ValueNumvberPhon
        {
            get
            {
                return (String)GetValue(ValueNumvberPhonProperty);
            }
            set
            {
                SetValue(ValueNumvberPhonProperty, value);
            }
        }
        // свойство зависимостей
        public static readonly DependencyProperty ValueNmaeProperty = DependencyProperty.Register(
                        "ValueNmae",
                        typeof(String),
                        typeof(Description4TextBlock), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String ValueNmae
        {
            get
            {
                return (String)GetValue(ValueNmaeProperty);
            }
            set
            {
                SetValue(ValueNmaeProperty, value);
            }
        }

        public Description4TextBlock()
        {
            InitializeComponent();
        }
    }
}
