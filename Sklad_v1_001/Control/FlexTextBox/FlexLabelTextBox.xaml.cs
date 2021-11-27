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

namespace Sklad_v1_001.Control.FlexTextBox
{
    /// <summary>
    /// Логика взаимодействия для FlexLabelTextBox.xaml
    /// </summary>
    public partial class FlexLabelTextBox : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
        "LabelText",
        typeof(String),
        typeof(FlexLabelTextBox), new UIPropertyMetadata(Properties.Resources.Name));
        public String LabelText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
       "Description",
       typeof(String),
       typeof(FlexLabelTextBox), new UIPropertyMetadata());
        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty EnableTextBoxProperty = DependencyProperty.Register(
       "EnableTextBox",
       typeof(Boolean),
       typeof(FlexLabelTextBox), new UIPropertyMetadata());
        public Boolean EnableTextBox
        {
            get { return (Boolean)GetValue(EnableTextBoxProperty); }
            set { SetValue(EnableTextBoxProperty, value); }
        }

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(FlexLabelTextBox), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get { return (Visibility)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }           
        }
        public FlexLabelTextBox()
        {
            InitializeComponent();
        }
    }
}
