using Sklad_v1_001;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
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

namespace Sklad_v1_001.Control.FlexButton
{
    /// <summary>
    /// Логика взаимодействия для ButtonAdd.xaml
    /// </summary>
    public partial class ToolBarButton : UserControl, IAbstractButton
    {
        // свойство зависимостей
        public static readonly DependencyProperty VisibilityButtonProperty = DependencyProperty.Register(
                        "VisibilityButton",
                        typeof(Visibility),
                        typeof(ToolBarButton), new UIPropertyMetadata(Visibility.Visible));
        //IsEnabledRule
        public static readonly DependencyProperty IsEnabledRuleProperty = DependencyProperty.Register(
                        "IsEnabledRule",
                        typeof(Boolean),
                        typeof(ToolBarButton), new UIPropertyMetadata(true));
        public ToolBarButton()
        {
            InitializeComponent();        
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilityButton
        {
            get { return (Visibility)GetValue(VisibilityButtonProperty); }
            set { SetValue(VisibilityButtonProperty, value); }
        }

        public Boolean IsEnabledRule
        {
            get { return (Boolean)GetValue(IsEnabledRuleProperty); }
            set { SetValue(IsEnabledRuleProperty, value); }
        }
        public event Action ButtonClick;       

        public string Text
        {
            get
            {
                return this.TextField.Content.ToString();
            }

            set
            {
                this.TextField.Content = value;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick();
            }
        }        
    }
}
