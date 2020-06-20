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
    /// Interaction logic for FlexGridFromTo.xaml
    /// </summary>
    public partial class FlexGridFromTo : UserControl
    {
        // заголовок фильтра
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
                        "LabelText",
                        typeof(string),
                        typeof(FlexGridFromTo), new UIPropertyMetadata(Properties.Resources.LableText));
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
        public FlexGridFromTo()
        {
            InitializeComponent();
        }
    }
}
