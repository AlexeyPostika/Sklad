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

namespace Sklad_v1_001.Control.ToolBar
{
    /// <summary>
    /// Логика взаимодействия для ToolBarNextToBack.xaml
    /// </summary>
    public partial class ToolBarNextToBack : UserControl
    {
        public static readonly DependencyProperty WidthOnWhatPageProperty = DependencyProperty.Register(
          "WidthOnWhatPage",
          typeof(Int32),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(50));

        public Int32 WidthOnWhatPage
        {
            get { return (Int32)GetValue(WidthOnWhatPageProperty); }
            set { SetValue(WidthOnWhatPageProperty, value); }
        }

        public static readonly DependencyProperty TextOnWhatPageProperty = DependencyProperty.Register(
          "TextOnWhatPage",
          typeof(String),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(String.Empty));

        public String TextOnWhatPage
        {
            get { return (String)GetValue(TextOnWhatPageProperty); }
            set { SetValue(TextOnWhatPageProperty, value); }
        }

        public static readonly DependencyProperty IsEnableNextProperty = DependencyProperty.Register(
          "IsEnableNext",
          typeof(Boolean),
          typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        public Boolean IsEnableNext
        {
            get { return (Boolean)GetValue(IsEnableNextProperty); }
            set { SetValue(IsEnableNextProperty, value); }
        }

        public static readonly DependencyProperty IsEnableNextEndProperty = DependencyProperty.Register(
        "IsEnableNextEnd",
        typeof(Boolean),
        typeof(ToolBarNextToBack), new UIPropertyMetadata(true));

        public Boolean IsEnableNextEnd
        {
            get { return (Boolean)GetValue(IsEnableNextEndProperty); }
            set { SetValue(IsEnableNextEndProperty, value); }
        }

        public static readonly DependencyProperty IsEnableBackProperty = DependencyProperty.Register(
         "IsEnableBack",
         typeof(Boolean),
         typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        public Boolean IsEnableBack
        {
            get { return (Boolean)GetValue(IsEnableBackProperty); }
            set { SetValue(IsEnableBackProperty, value); }
        }

       public static readonly DependencyProperty IsEnableBackInProperty = DependencyProperty.Register(
       "IsEnableBackIn",
       typeof(Boolean),
       typeof(ToolBarNextToBack), new UIPropertyMetadata(false));

        public Boolean IsEnableBackIn
        {
            get { return (Boolean)GetValue(IsEnableBackInProperty); }
            set { SetValue(IsEnableBackInProperty, value); }
        }

        public ToolBarNextToBack()
        {
            InitializeComponent();
        }

        public event Action ButtonNext;
        public event Action ButtonNextEnd;
        public event Action ButtonBack;
        public event Action ButtonBackIn;
        //кнопка назад
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonBack?.Invoke();
        }

        private void NextButoon_Click(object sender, RoutedEventArgs e)
        {
            ButtonNext?.Invoke();
        }

        private void BackInButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonBackIn?.Invoke();
        }
        
        private void NextEndButoon_Click(object sender, RoutedEventArgs e)
        {
            ButtonNextEnd?.Invoke();
        }
    }
}
